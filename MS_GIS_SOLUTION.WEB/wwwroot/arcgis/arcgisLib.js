var view;
var map;
var layerTileRegion;
var filterExpression = '1=1';

require([
    "esri/views/MapView",
    "esri/layers/FeatureLayer",
    "esri/widgets/LayerList",
    "esri/Graphic",
    "esri/layers/TileLayer",
    "esri/geometry/geometryEngine",
    "esri/widgets/Bookmarks",
    "esri/widgets/Legend",
    "esri/WebMap",
    "esri/identity/IdentityManager",
    "esri/widgets/BasemapGallery",
    "esri/views/2d/draw/Draw",
    "esri/geometry/Polygon",
    "esri/geometry/Polyline",
    "esri/geometry/support/webMercatorUtils",
    "Scripts/ArcgisTools/CoordinateAndScale",
    "Scripts/ArcgisTools/IdentifyTaskLineTool",
    "esri/widgets/Print",
    "esri/popup/content/AttachmentsContent",
    "esri/popup/content/TextContent",
    "esri/layers/TileLayer",

    "dojo/domReady!"
], function (
    MapView,
    FeatureLayer,
    LayerList,
    Graphic,
    TileLayer,
    geometryEngine,
    Bookmarks,
    Legend,
    WebMap,
    IdentityManager,
    BasemapGallery,
    Draw,
    Polygon,
    Polyline,
    webMercatorUtils,
    CoordinateAndScale,
    IdentifyTaskLineTool,
    Print,
    AttachmentsContent,
    TextContent,
    TileLayer,
) {

    var regionsFilter = '';
    $(document).ready(function () {
        $('body').removeClass('enlarge-menu');
        $('body').addClass('enlarge-menu');
        $('.page-title-box').css('display', 'none');
        $('.page-wrapper').height(window.innerHeight);
        $('#map_canvas').height($('.page-wrapper').height() - (parseInt($('.topbar').height()) + parseInt($('.footer').outerHeight())));

        $.getJSON('/AdminUnit/LoadIdName', { loadOptions: {} }).then((d) => {
            regionsFilter = ' and fk_id_admin_unit in(' + d.data.map(m => m.Id).join(",") + ')';
            getLayerStore().then((d) => {
                for (let i = 0; i < d.length; i++) {
                    let { id, name } = d[i];
                    createLayer(id, name)
                }
            });
        });
    });

    map = new WebMap({
        basemap: 'satellite',
    });

    let viewConfig = {
        container: "map_canvas",
        map: map,
        zoom: 8,
        center: [48, 40.185132],
        constraints: {
            minZoom: 8,
            maxZoom: 18,
            rotationEnabled: true,
        },
    };

    view = new MapView(viewConfig);


    view.ui.move("zoom", "top-right");
    view.ui.add("calcite_shell_panel", "top-left")

    const basemaps = new BasemapGallery({
        view,
        container: "basemaps-container"
    });



    /////////////////////////////////////////////////////////////////////////Mesafe



    var draw = new Draw({
        view: view,
    });

    // Add event listener on keydown
    document.addEventListener(
        "keydown",
        (event) => {
            var name = event.key;
            var code = event.code;

            if (code == "Escape") {
                action = draw.reset();
                view.graphics.removeAll();
                view.popup.visible = false;
                document.querySelector(`[data-action-id=drawpolyline]`).active = false;
                document.querySelector(`[data-action-id=draw-polygon]`).active = false;
            }
        },
        false
    );


    document.querySelector(`[data-action-id=drawpolyline]`).addEventListener("click",
        function () {
            //return;
            view.graphics.removeAll();
            // create() will return a reference to an instance of PolygonDrawAction
            var action = draw.create("polyline");

            // focus the view to activate keyboard shortcuts for drawing polygons
            view.focus();

            // listen polygonDrawAction events to give immediate visual feedback
            // to users as the polygon is being drawn on the view.
            action.on("vertex-add", drawPolyline);
            action.on("cursor-update", drawPolyline);
            action.on("vertex-remove", drawPolyline);
            action.on("redo", drawPolyline);
            action.on("undo", drawPolyline);
            action.on("draw-complete", drawPolyline);
        });

    //document.getElementById("filter").addEventListener("click",
    //    function () {
    //        let popup = $('#filterPopup').dxPopup('instance');
    //        popup.option('width', '400px');
    //        popup.option('height', view.height + 'px');
    //        popup.show();
    //    });

    // this function is called from the polygon draw action events
    // to provide a visual feedback to users as they are drawing a polygon
    function drawPolyline(event) {
        var vertices = event.vertices;
        //remove existing graphic
        view.graphics.removeAll();
        view.popup.visible = false;

        // create a new polygon
        var polyLine = new Polyline({
            paths: vertices,
            spatialReference: view.spatialReference
        });

        // create a new graphic representing the polygon, add it to the view
        var graphic = new Graphic({
            geometry: polyLine,
            symbol: {
                type: "simple-line", // autocasts as new SimpleFillSymbol
                color: [255, 0, 0],
                width: 2,
                cap: "round",
                join: "round"
            }
        });

        view.graphics.add(graphic);

        var length = Math.round(geometryEngine.geodesicLength(polyLine, "meters") * 100) / 100 + " metr (Çıxmaq üçün 'ESC' basın)";
        if ((Math.round(geometryEngine.geodesicLength(polyLine, "meters") * 100) / 100) > 1000) {
            length = Math.round(geometryEngine.geodesicLength(polyLine, "kilometers") * 1000) / 1000 + " kilometr (Çıxmaq üçün 'ESC' basın)";
        }
        var normalizedVal = webMercatorUtils.xyToLngLat(vertices[vertices.length - 1][0], vertices[vertices.length - 1][1]);

        view.popup.dockEnabled = false;
        view.popup.dockOptions.buttonEnabled = false;
        view.popup.open({
            title: "Məsafə: ",
            content: length
        });
        view.popup.location = normalizedVal;

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////



    var drawPolygonButton = document.querySelector(`[data-action-id=draw-polygon]`);
    drawPolygonButton.addEventListener("click", function () {
        //return;
        view.graphics.removeAll();
        view.popup.visible = false;
        enableCreatePolygon(draw, view);
    });

    function enableCreatePolygon(draw, view) {
        // create() will return a reference to an instance of PolygonDrawAction
        var action = draw.create("polygon");

        // focus the view to activate keyboard shortcuts for drawing polygons
        view.focus();

        // listen to vertex-add event on the action
        action.on("vertex-add", drawPolygon);

        // listen to cursor-update event on the action
        action.on("cursor-update", drawPolygon);

        // listen to vertex-remove event on the action
        action.on("vertex-remove", drawPolygon);

        // *******************************************
        // listen to draw-complete event on the action
        // *******************************************
        action.on("draw-complete", drawPolygon);
    }

    // this function is called from the polygon draw action events
    // to provide a visual feedback to users as they are drawing a polygon
    function drawPolygon(event) {
        var vertices = event.vertices;


        //remove existing graphic
        view.graphics.removeAll();
        view.popup.visible = false;
        // create a new polygon
        polygon = new Polygon({
            rings: vertices,
            spatialReference: view.spatialReference,
        });
        // create a new graphic representing the polygon, add it to the view
        var graphic = new Graphic({
            geometry: polygon,
            symbol: {
                type: "simple-fill", // autocasts as SimpleFillSymbol
                color: [178, 102, 234, 0.8],
                style: "solid",
                outline: {
                    // autocasts as SimpleLineSymbol
                    color: [255, 255, 255],
                    width: 2,
                },
            },
        });
        view.graphics.add(graphic);

        // calculate the area of the polygon
        var area = geometryEngine.geodesicArea(polygon, "acres");
        if (area < 0) {
            // simplify the polygon if needed and calculate the area again
            var simplifiedPolygon = geometryEngine.simplify(polygon);
            if (simplifiedPolygon) {
                area = geometryEngine.geodesicArea(simplifiedPolygon, "acres");
            }
        }
        // start displaying the area of the polygon
        labelAreas(polygon, area);
    }
    function labelAreas(geom, area) {
        var graphic = new Graphic({
            geometry: geom.centroid,
            symbol: {
                type: "text",
                color: "white",
                haloColor: "black",
                haloSize: "1px",
                text: (area * 0.404686).toFixed(2) + " ha",
                xoffset: 3,
                yoffset: 3,
                //font: { // autocast as Font
                //    size: 14,
                //    //family: "sans-serif"
                //}
            }
        });
        view.graphics.add(graphic);
    }

    let labelClass = {
        // autocasts as new LabelClass()
        symbol: {
            type: "text", // autocasts as new TextSymbol()
            color: "white",
            font: { // autocast as new Font()
                //family: "Playfair Display",
                size: 12,
                //weight: "bold"
            }
        },
        //labelPlacement: "above-center",
        labelExpressionInfo: {
            expression: "$feature.name"
        }
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    const bookmarks = new Bookmarks({
        view,
        container: "bookmarks-container"
    });

    const layerList = new LayerList({
        view,
        selectionEnabled: true,
        container: "layers-container",
        listItemCreatedFunction: (event) => {
            const item = event.item;

            if (item.layer.id == "layer15000") {
                item.panel = {
                    content: "legend",
                    open: true
                };
            }
            else if (item.layer.type != "group") {
                // don't show legend twice
                item.panel = {
                    content: "legend",
                    open: true
                };
            }

        }
    });

    const legend = new Legend({
        view,
        container: "legend-container"
    });


    const print = new Print({
        view,
        container: "print-container",
        templateOptions: {
            title: "My Print",
            author: "AZERP",
            copyright: "AZERP",
            legendEnabled: false
        }
    });

    print.on("load", function (evn) {
        print.templateOptions.title = "MSCİS";

    });


    const button = document.querySelector("#search-panel-buttom");
    const panel = document.querySelector("#search-Panel");
    button.addEventListener("click", function () {

        panel.dismissed = false;
        panel.closed = false;
        button.active = false;
    });


    /*_____________________________VIEW CLICK EVENT_____________________________*/

    let highlightSelect = null;
    view.on("click", function (evnt) {
        view.hitTest(evnt).then(function (response) {

            let url = '';
            let id = null;

            if (response.results.length > 0) {

                let feature = response.results.filter(m => m.layer !== null)[0];

                let graphic = feature.graphic;
                let { name, OBJECTID } = graphic.attributes;
                let template = '';

                view.whenLayerView(response.results[0].graphic.layer).then((layerView) => {

                    if (highlightSelect) {
                        highlightSelect.remove();
                    }

                    highlightSelect = layerView.highlight(OBJECTID);
                });

                switch (feature.layer.layerId) {
                    case ARCGIS_SERVICES.NASOS_STANSIYALARI:
                        url = '/PumpStation/Get';
                        id = graphic.attributes["id_pump_station"];
                        template = popupTemplate.nasosStansiyasiTemplate;
                        break;
                    case ARCGIS_SERVICES.ACHIQ_SUVARMA_SHEBEKESI:
                        url = '/Channel/Get';
                        id = graphic.attributes["id_channel_admin_unit"];
                        template = popupTemplate.aciqSuvarmaTemplate;
                        break;
                    case ARCGIS_SERVICES.QAPALI_SUVARMA_SHEBEKESI:
                        url = '/ClosedChannel/Get';
                        id = graphic.attributes["id_closed_channel_admin_unit"];
                        template = popupTemplate.qapaliSuvarmaTemplate;
                        break;
                    case ARCGIS_SERVICES.KOLLEKTOR_DRENAJ_SHEBEKESI:

                        url = '/Collector/Get';
                        id = graphic.attributes["id_collector_admin_unit"];
                        template = popupTemplate.collectorTemplate;
                        break;
                    case ARCGIS_SERVICES.HIDROTEXNIKI_QURGULAR:
                        url = '/HydrotechnicalInstallation/Get';
                        id = graphic.attributes["id_hydrotechnical_installations"];
                        template = popupTemplate.hydroTechnicalTemplate;

                        break;
                    case ARCGIS_SERVICES.HIDROQOVSHAQ:
                        url = '/HydroJunction/Get';
                        id = graphic.attributes["id_hydro_junctions"];
                        template = popupTemplate.hydroJunctionTemplate;
                        break;
                    case ARCGIS_SERVICES.MUHAFIZE_BENDLERI:
                        url = '/ProtectionBarrier/Get';
                        id = graphic.attributes["id_protection_barriers"];
                        template = popupTemplate.protectionBarriersTemplate;
                        break;
                    case ARCGIS_SERVICES.ISTISMAR_YOLLARI:
                        url = '/ExploitationRoads/Get';
                        id = graphic.attributes["id_exploitation_roads"];
                        template = popupTemplate.exploitationRoadsTemplate;
                        break;
                    case ARCGIS_SERVICES.QISH_OTLAQLARIN_SU_TEMINATI_SISTEMLERI:
                        url = '/WinterWaterSupply/Get';
                        id = graphic.attributes["id_winter_water_supply_systems"];
                        template = popupTemplate.winterWaterSupplyTemplate;
                        break;
                    case ARCGIS_SERVICES.SU_TUTARLAR:
                        url = '/ReservoirSystem/Get';
                        id = graphic.attributes["id_reservoir_system"];
                        template = popupTemplate.reservoirSystemTemplate;
                        break;
                    case ARCGIS_SERVICES.BINALAR_VE_TIKILILER:
                        url = '/BuildingAndConstructions/Get';
                        id = graphic.attributes["id_buildings_constructions"];
                        template = popupTemplate.buildingsConstructionsTemplate;
                        break;
                    case ARCGIS_SERVICES.ARTEZIAN_VE_SUBARTEZIAN_QUYULARI:
                        url = '/ArtesianSubartesianWell/Get';
                        id = graphic.attributes["id_artesian_subartesian_well"];
                        template = popupTemplate.artesianSubartesianTemplate;
                        break;
                    case ARCGIS_SERVICES.CHAYLAR:
                        url = '/River/Get';
                        id = graphic.attributes["id_river"];
                        template = popupTemplate.riverTemplate;
                        break;
                    case ARCGIS_SERVICES.GOLLER:
                        url = '/Lake/Get';
                        id = graphic.attributes["id_lake"];
                        template = popupTemplate.lakeTemplate;
                        break;
                }

                if (id === null) return;

                $.post(url, { id: id }).then((d) => {
                    template = template.replace(/{key}/g, id);
                    Object.keys(d).map(m => {
                        template = template.replace('{' + m + '}', d[m]);
                    });

                    let layerTitle = feature.layer.title.replace('AzMTS v7 forServer v5 - ', '').replace(' AU', '').replace('Su tutarlar', 'Su Anbarları(Su Tutarları)');

                    $('#infoPopup').dxPopup({
                        title: layerTitle,
                        visible: true,
                        contentTemplate: function () {
                            let container = document.createElement("DIV");
                            container.classList.add("info-div");
                            container.style.height = "360px";
                            container.style.overflowY = "scroll";
                            container.scrollWidth = "1px solid #fd22df"
                            container.addEventListener('wheel', function (e) {
                                container.scrollTop = container.scrollTop + e.deltaY;
                            }, { passive: false })
                            $(container).html(template);
                            return container;
                        }
                    });

                    if (feature.layer.capabilities.attachment.supportsName === true) {
                        feature.layer.queryAttachments({
                            attachmentTypes: ["image/jpeg"],
                            objectIds: [OBJECTID]
                        }).then((ss) => {
                            Object.keys(ss).map(k => {
                                ss[k].map(im => {
                                    let image = document.createElement("img");
                                    image.style.width = "100%";
                                    image.src = im.url;
                                    let tr = document.createElement("tr");
                                    let td = document.createElement("td");
                                    td.colSpan = 2;
                                    td.appendChild(image);
                                    tr.appendChild(td);
                                    let table = document.getElementById("tblInfo-" + id);
                                    table.appendChild(tr);
                                });
                            });
                        });
                    }
                });
            }
        });

        var screenPoint = evnt.screenPoint;
        arrPoint = identifyTaskLineTool.runCommand(screenPoint);
    });

    /*_____________________________VIEW CLICK EVENT_____________________________*/



    // Function that creates two popup elements for the template: attachments and text
    function infoPopup(feature) {
        // 1. Set how the attachments should display within the popup
        const attachmentsElement = new AttachmentsContent({
            displayType: "auto"
        });

        const textElement = new TextContent();
        return [textElement, attachmentsElement];
    }

    let layer15000 = new TileLayer({
        url: "https://185.18.245.153:6443/arcgis/rest/services/15000_V1/MapServer",
        title: 'Ortofoto 1',
        id: 'layer15000',
        visible: false
    });

    let layer25000 = new TileLayer({
        url: "https://msgis.mst.gov.az/server/rest/services/Orthophoto_v3_1/MapServer",
        title: 'Ortofoto 2',
        id: 'layer25000',
        visible: false
    });

    let KandTasarrufati = new TileLayer({
        title: 'KTN uqodiya',
        visible: false,
        url: 'https://185.18.245.153:6443/arcgis/rest/services/kandtasarrufati/MapServer'
    });

    
    map.add(layer15000);
    map.add(layer25000);
    map.add(KandTasarrufati);


    createLayer(14, 'İnzibati ərazi vahidləri');
    createLayer(16, 'Açıq suvarma şəbəkəsinin örtüyü');

    function createLayer(id, name) {

        let definitionExpression = 'is_deleted=0';
        switch (id) {
            case ARCGIS_SERVICES.ACHIQ_SUVARMA_SHEBEKESI:
            case ARCGIS_SERVICES.QAPALI_SUVARMA_SHEBEKESI:
            case ARCGIS_SERVICES.KOLLEKTOR_DRENAJ_SHEBEKESI:
            case ARCGIS_SERVICES.HIDROTEXNIKI_QURGULAR:
            case ARCGIS_SERVICES.HIDROQOVSHAQ:
            case ARCGIS_SERVICES.BINALAR_VE_TIKILILER:
            case ARCGIS_SERVICES.NASOS_STANSIYALARI:
            case ARCGIS_SERVICES.ISTISMAR_YOLLARI:
            case ARCGIS_SERVICES.SU_TUTARLAR:
            case ARCGIS_SERVICES.QISH_OTLAQLARIN_SU_TEMINATI_SISTEMLERI:
            case ARCGIS_SERVICES.MUHAFIZE_BENDLERI:
                definitionExpression += regionsFilter;
                break;
            case ARCGIS_SERVICES.ARTEZIAN_VE_SUBARTEZIAN_QUYULARI:
                definitionExpression += regionsFilter.replace('fk_id_admin_unit', 'fk_admin_unit');
                break;
            default:

                break;
        }


        let l = new FeatureLayer({
            title: name,
            url: serviceUrl + id,
            returnGeometry: true,
            id: 'layer-' + id,
            visible: id === ARCGIS_SERVICES.INZIBATI_ERAZI_VAHIDLERI,
            outFields: ["*"],
            view: view,
            definitionExpression: definitionExpression,
            popupTemplate: null
        });


        l.labelingInfo = [labelClass];


        if (id === ARCGIS_SERVICES.ARTEZIAN_VE_SUBARTEZIAN_QUYULARI) {
            l.labelingInfo = [{
                // autocasts as new LabelClass()
                symbol: {
                    type: "text", // autocasts as new TextSymbol()
                    color: "black",
                    font: { // autocast as new Font()
                        //family: "Playfair Display",
                        size: 8,
                        //weight: "bold"
                    }
                },
                //labelPlacement: "above-center",
                labelExpressionInfo: {
                    expression: "$feature.no"
                }
            }];

        }


        if (id === ARCGIS_SERVICES.INZIBATI_ERAZI_VAHIDLERI) {

            l.renderer = {
                type: "simple",
                symbol: {
                    color: "white", //"#506ee4",
                    type: "simple-line",
                    style: "solid",
                    width: 1
                }
            }
        }

        map.add(l);
    }


    view.showLayer = (id) => {
        view.allLayerViews.items.filter(m => m.layer.id === 'layer-' + id).map(m => m.visible = true);
    }

    view.hideAllLayer = () => {
        view.allLayerViews.items.filter(m => m.layer.id != 'layer-14' && m.layer.id.includes('layer-')).map(m => m.visible = false);
    }


    IdentityManager.registerToken({
        server: serviceUrl,
        token: token.token,
        outFields: ["*"]
    });


    identifyTaskLineTool = new IdentifyTaskLineTool({ view: view, map: map });


    var coordsWidget = document.createElement("div");
    coordsWidget.id = "coordsWidget";
    coordsWidget.className = "esri-widget esri-component";
    coordsWidget.style.padding = "7px 15px 5px";


    coordinateAndScale = new CoordinateAndScale({ view: view, map: map });


    view.watch(["stationary"], function () {
        coordinateAndScale.runCommand(view.center);
    });
    //*** Add event to show mouse coordinates on click and move ***//
    view.on(["pointer-down", "pointer-move"], function (evt) {
        coordinateAndScale.runCommand(view.toMap({ x: evt.x, y: evt.y }));
    });

    view.ui.add(coordsWidget, "bottom-right");


    document.querySelector('[data-action-id="filter"]').addEventListener('click', function () {
        let popup = $('#filterPopup').dxPopup('instance');
        popup.option('width', '400px');
        popup.option('height', window.innerHeight + 'px');
        if (popup.option('visible')) {
            popup.hide();
        } else {
            popup.show();
        }
    });

    map.when(() => {

        let activeWidget;

        const handleActionBarClick = ({ target }) => {
            if (target.tagName !== "CALCITE-ACTION") {
                return;
            }


            if (activeWidget) {
                document.querySelector(`[data-action-id=${activeWidget}]`).active = false;
                document.querySelector(`[data-panel-id=${activeWidget}]`) == null ? "" : document.querySelector(`[data-panel-id=${activeWidget}]`).hidden = true;
                action = draw.reset();
                view.graphics.removeAll();
                view.popup.visible = false;
            }

            const nextWidget = target.dataset.actionId;
            if (nextWidget !== activeWidget) {
                document.querySelector(`[data-action-id=${nextWidget}]`).active = true;
                document.querySelector(`[data-panel-id=${nextWidget}]`) == null ? "" : document.querySelector(`[data-panel-id=${nextWidget}]`).hidden = false;
                activeWidget = nextWidget;
            } else {
                activeWidget = null;
            }
        };

        document.querySelector("calcite-action-bar").addEventListener("click", handleActionBarClick);

        let actionBarExpanded = false;

        document.addEventListener("calciteActionBarToggle", event => {
            actionBarExpanded = !actionBarExpanded;
            view.padding = {
                left: actionBarExpanded ? 135 : 45
            };
        });
    });


    //setup before functions
    var typingTimer;                //timer identifier
    var doneTypingInterval = 2000;  //time in ms, 2 seconds for example
    var $input = $('#myInput');

    $input.on('keydown', function () {
        clearTimeout(typingTimer);
    });

    $("#searchInput").keyup(function () {

        if ($("#searchselectlayer").val() == "value") {
            return;
        }

        $("#ui-id-1").find("li").remove();

        if ($("#searchInput").val().trim().length == 0) {
            $("#ui-id-1").css("display", "none");
            return;
        }


        let l = map.layers.items.find(x => x.title == $('#layers').select2('data')[0].text);


        const queryParams = l.createQuery();
        queryParams.where = "name like n'%" + document.getElementById("searchInput").value + "%'";
        if ($('#layers').select2('data')[0].id === '5') {
            queryParams.where = "no like n'%" + document.getElementById("searchInput").value + "%'";
        }
        queryParams.geometry = view.extent;
        queryParams.returnGeometry = true;
        queryParams.num = 100000;

        l.queryFeatures(queryParams).then(showResults)
    });

    function showResults(response) {
        $("#ui-id-1").find("li").remove();

        if ($("#searchInput").val().trim().length == 0) {
            $("#ui-id-1").css("display", "none");
            return;
        }

        // If no results are returned from the find, notify the user
        if (response.features.length === 0) return;

        $("#ui-id-1").css("display", "block");
        clearTimeout(typingTimer);

        // Loop through each result in the response and add as a row in the table
        response.features.forEach(function (findResult, i) {

            // Get each value of the desired attributes
            var city = findResult.attributes["name"] ?? "";
            var obId = findResult.attributes["OBJECTID"];
            var adminunit0 = findResult.attributes["fk_id_admin_unit"] ?? "";
            var adminunit1 = findResult.attributes["additional_admin_unit"] ?? "";

            if ($('#layers').select2('data')[0].id === '5') {
                city = findResult.attributes["no"] ?? "";
            }

            if (findResult.layer.layerId == 0 || findResult.layer.layerId == 1 || findResult.layer.layerId == 2 || findResult.layer.layerId == 4) {
                $("#ui-id-1").append(
                    "<li class='each ui-menu-item'  tabindex='-1'><div class='acItem'><span hidden>" +
                    obId +
                    "</span> <span hidden>" +
                    findResult.layer.layerId +
                    "</span> <span class='object-name'>" +

                    city +

                    "</span><br>" +
                    "<span class= 'name' > " +
                    findResult.sourceLayer.sourceJSON.name +

                    "</span><br />" + "<span class= 'adminunit' > " +
                    adminunit0 + " " + adminunit1 +

                    "</span></div></li>"
                );
            } else {

                $("#ui-id-1").append(
                    "<li class='each ui-menu-item'  tabindex='-1'><div class='acItem'><span hidden>" +
                    obId +
                    "</span> <span hidden>" +
                    findResult.layer.layerId +
                    "</span> <span class='object-name'>" +

                    city +

                    "</span><br>" +
                    "<span class= 'name' > " +
                    findResult.sourceLayer.sourceJSON.name +

                    "</span></div></li>"
                );
            }
        });

        $(".name").on("click", function (event) {
            let objectID = event.currentTarget.parentElement.children[0].outerText; // OBJECTID
            let layerID = event.currentTarget.parentElement.children[1].outerText;//layerID
            SearchGoTo(objectID, layerID);
            return;
        });

        $(".object-name").on("click", function (event) {
            let objectID = event.currentTarget.parentElement.children[0].outerText; // OBJECTID
            let layerID = event.currentTarget.parentElement.children[1].outerText;//layerID
            SearchGoTo(objectID, layerID);
            return;
        });

        $(".acItem").on("click", function (event) {
            event.preventDefault();
            event.stopPropagation();
            let objectID = event.target.childNodes[0]?.outerText;
            let layerID = event.target.childNodes[2]?.outerText;

            SearchGoTo(objectID, layerID);
            return;

        });

        function SearchGoTo(objectID, layerID) {

            if (objectID === undefined || layerID === undefined) {
                return;
            }

            map.layers.map(m => {
                if (m.id.includes('searchlayer-')) {
                    map.remove(m);
                }
            });


            view.zoom = 1;
            view.graphics.removeAll();
            let layer = "layer-" + layerID;

            if (map.layers.items.filter(x => x.id == layer).length > 0) {

                let definitionExpression = "OBJECTID=" + objectID;

                let l = new FeatureLayer({
                    title: name,
                    url: serviceUrl + layerID,
                    returnGeometry: true,
                    id: 'searchlayer-' + layerID,
                    visible: false,
                    outFields: ["*"],
                    view: view,
                    definitionExpression: definitionExpression,
                    popupTemplate: null,
                    labelingInfo: [labelClass]
                });

                map.add(l);
                clearMap();

                l.visible = true;
                const queryParams = l.createQuery();
                queryParams.where = "OBJECTID=" + objectID;
                queryParams.geometry = view.extent;
                queryParams.returnGeometry = true;

                l.queryFeatures(queryParams).then(function (response) {

                    higlightGraphic(response);
                    view.zoom = 15;
                    view.goTo(response.features[0]);
                });
            }
        }
    }

    function higlightGraphic(response) {

        if (response.features.length === 0) return;
        let { features } = response;
        let { geometry, layer, attributes } = features[0];
        let iconUrl = "/Content/mapicons/red-pin.png";

        let color = response.geometryType === "polygon" ? layer.sourceJSON.drawingInfo.renderer.symbol.color : layer.sourceJSON.drawingInfo.renderer.defaultSymbol.color;
        color[2] = 250;

        let symbol;
        switch (response.geometryType) {
            case "polygon":

                symbol = {
                    type: "simple-marker",
                    style: "diagonal-cross",
                    outline: {
                        color: color, // White
                        width: 2
                    }
                };


                break;
            case "polyline":

                symbol = {
                    attributes: attributes,
                    type: "simple-line", // autocasts as new SimpleFillSymbol
                    color: color, //"#06a9f4",
                    style: "solid",
                    width: 4,
                    cap: "round",
                    join: "round",
                };


                break;
            case "point":
                return;                
                break;
            default:
        }

        let graphic = new Graphic({
            attributes: attributes,
            geometry: geometry,
            symbol: symbol
        });

        view.graphics.add(graphic);
    }


    // Executes each time the promise from find.execute() is rejected.
    function rejectedPromise(error) {
        
    }

});