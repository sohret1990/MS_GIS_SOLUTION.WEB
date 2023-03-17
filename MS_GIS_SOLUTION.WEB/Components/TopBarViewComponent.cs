using Microsoft.AspNetCore.Mvc;

namespace MS_GIS_SOLUTION.WEB.Components
{
    public class TopBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
