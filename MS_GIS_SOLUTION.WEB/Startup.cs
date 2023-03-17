using Alachisoft.NCache.EntityFrameworkCore;
using AspNetCoreWizardCustomization;
using AspNetCoreWizardCustomization.Services;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using MS_GIS_SOLUTION.UnitOfWork;
using MS_GIS_SOLUTION.WEB.Reporting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sentry;
using Serilog;
using System.IO;


namespace MS_GIS_SOLUTION.WEB
{
    public class Startup
    {
        readonly CustomConfigurationProvider configurationProvider;
        IHostingEnvironment _hostingEnvironment;


        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            configurationProvider = new CustomConfigurationProvider(hostingEnvironment);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {

            });

            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                });

            services.AddCors(c => c.AddPolicy("Permissions", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<MvcOptions>(options =>
            {

            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            //
            // HTML Helper
            //
            services.TryAddTransient<IHtmlHelper, HtmlHelper>();
            services.TryAddTransient(typeof(IHtmlHelper<>), typeof(HtmlHelper<>));
            services.TryAddSingleton<IHtmlGenerator, DefaultHtmlGenerator>();
            services.TryAddSingleton<ModelExpressionProvider>();
            // ModelExpressionProvider caches results. Ensure that it's re-used when the requested type is IModelExpressionProvider.
            services.TryAddSingleton<IModelExpressionProvider>(s => s.GetRequiredService<ModelExpressionProvider>());
            services.TryAddSingleton<ValidationHtmlAttributeProvider, DefaultValidationHtmlAttributeProvider>();

            //services.TryAddSingleton<IJsonHelper, DefaultJsonHelper>();

            services.AddControllersWithViews();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDevExpressControls();

            services.AddSingleton<CustomConfigurationProvider, CustomConfigurationProvider>();
            services.AddSingleton<CustomConfigFileDataConnectionProviderService, CustomConfigFileDataConnectionProviderService>();

            services.AddSingleton<IConfiguration>(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.Configure<HtmlHelperOptions>(o => { o.ClientValidationEnabled = false; o.Html5DateRenderingMode = Microsoft.AspNetCore.Mvc.Rendering.Html5DateRenderingMode.CurrentCulture; });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(provider => new MsGisDbContext(new AEUnitOfWork<MsGisModel>()));

            services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                    designerConfigurator.RegisterDataSourceWizardConfigurationConnectionStringsProvider(connectionStringsSection: configurationProvider.GetSqlConnectionStrings(), jsonDataSourceConnectionStringsSection: configurationProvider.GetJsonConnectionStrings());
                    designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                    designerConfigurator.EnableCustomSql();
                });

                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.RegisterConnectionProviderFactory<CustomConnectionProviderFactory>();
                    viewerConfigurator.RegisterJsonDataConnectionProviderFactory<CustomConnectionProviderFactory>();
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });

            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, MsGisDbContext db)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseSession();
            app.UseHttpsRedirection();
            ////For DevExtreme components
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(_hostingEnvironment.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            app.UseStaticFiles();

            var reportDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, "Reports");
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(new MyReportStorageWebExtension(Configuration, db));
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Bindings;
            app.UseDevExpressControls();
            DefaultConnectionStringProvider.AssignConnectionStrings(configurationProvider.GetGlobalConnectionStrings());

            app.UseStatusCodePagesWithReExecute("/CustomError/Error/?code={0}");

            //app.Use(async (ctx, next) =>
            //{

            //    var items = ctx.Items;

            //    if (ctx.Request.Path == "/DXXRDV")
            //    {
            //        var form = ctx.Request.Form;
            //        var decodedURL = System.Net.WebUtility.UrlDecode(form["arg"].ToString());
            //        DxReportViewModel model = new DxReportViewModel();
            //        JsonConvert.PopulateObject(decodedURL, model);
            //    }

            //    await next();
            //});

            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseCors(
                options =>
                {
                    string frontendUrl = "https://localhost:44367/";
                    options.WithOrigins(frontendUrl);
                });

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            SentrySdk.CaptureException(exceptionHandlerPathFeature.Error);
                        }
                    });
                });

                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();
                        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                        {
                            SentrySdk.CaptureException(exceptionHandlerPathFeature.Error);
                        }
                    });
                });

                app.UseExceptionHandler("/CustomError");
                app.UseHsts();
            }

            UserContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            loggerFactory.AddSerilog();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalRHub>("/signalrhub");
            });

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //name: "areas",
                //template: "{area:exists}/{controller}/{action}/{id?}",
                //defaults: new { area = "ERP", controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseApplicationDbContextMiddleware();
        }
    }
}