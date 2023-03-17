////define([
////    "dijit/_WidgetBase",
////    "dojo/_base/declare"
////], function (_WidgetBase, declare) {

////    var LAYER_TYPE = null;
////    var LAYER_DEFINITION = null;
////    var LAYER = null;
////    return declare([_WidgetBase], {
////        options: {
////            map: null,
////            view: null,
////        },
////        constructor: function (options) {
////            declare.safeMixin(this.options, options);
            
////            this.set("map", this.options.map);
////            this.set("view", this.options.view);
////            this.set("LAYER_TYPE", this.options.LAYER_TYPE);
////            this.set("LAYER_DEFINITION", this.options.LAYER_DEFINITION);

////            //this.LAYER = this.options.LAYER_TYPE({
////            //    url: serviceUrl + serviceQueryVersion + "/MapServer/" + this.options.LAYER_DEFINITION
////            //});

////            console.log("POI_LAYER", LAYER);
////        },
////        runCommand: function (args) {
////            console.log("args", args);
////        }
////    })
////});

//////const { FeatureLayer, TileLayer } = require(["esri/layers/FeatureLayer", "esri/layers/TileLayer"]);
////const url = serviceUrl + serviceQueryVersion + "/MapServer/";

////export default class ARCGIS_LAYERS {
////    constructor(args) {
////        super(args);
////        console.log(args)
////        this.layerTileRegionSatellite = new args.TileLayer({
////            url: url,
////        });
////        this.layerTileRegionVector = new args.TileLayer({
////            url: url,
////        });
////        this.layerPoi = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerAirport = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerRegion = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerRoads = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerLand = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerYashayismenteqeleri = new args.FeatureLayer({
////            url: url,
////        });
////        this.layerHydroObject = new args.FeatureLayer({
////            url: url,
////        });
////    }

////    layerTileRegionSatellite = {};
////    layerTileRegionVector = {};
////    layerPoi = {};
////    layerAirport = {};
////    layerRegion = {};
////    layerRoads = {};
////    layerLand = {};
////    layerYashayismenteqeleri = {};
////    layerHydroObject = {};

////}

//const ARCGIS_LAYERS = {
//    layerTileRegionSatellite: {},
//    layerTileRegionVector: {},
//    layerPoi: {},
//    layerAirport: {},
//    layerRegion: {},
//    layerRoads: {},
//    layerLand: {},
//    layerYashayismenteqeleri: {},
//    layerHydroObject: {}
//}

//require([
//    "esri/Map",
//    "esri/views/MapView",
//    "esri/widgets/Expand",
//    "esri/widgets/BasemapToggle",
//    "esri/layers/FeatureLayer",
//    "esri/widgets/LayerList",
//    "esri/widgets/Home",
//    "esri/Graphic",
//    "esri/PopupTemplate",
//    "esri/layers/TileLayer",
//    "esri/geometry/geometryEngine",
//    "esri/geometry/Polygon",
//    "esri/widgets/Bookmarks",
//    "esri/widgets/Sketch",
//    "esri/layers/GraphicsLayer",
//    "esri/symbols/PictureMarkerSymbol"
//], function (Map, MapView, Expand, BasemapToggle, FeatureLayer, LayerList, Home, Graphic, PopupTemplate, TileLayer, geometryEngine, Polygon, Bookmarks, Sketch, GraphicsLayer, PictureMarkerSymbol) {

//    ARCGIS_LAYERS.layerTileRegionSatellite = new TileLayer({
//        url: serviceUrl + serviceTileVersionSatellite + "/MapServer",
//        id: 'regionLayerTileSatellite',
//        visible: true,
//    });

//    ARCGIS_LAYERS.layerTileRegionVector = new TileLayer({
//        url: serviceUrl + serviceTileVersionVector + "/MapServer",
//        id: 'regionLayerTileVector',
//        visible: false,
//    });

//    const roadRenderer = {
//        type: "simple",
//        symbol: {
//            color: "#e59972",
//            type: "simple-line",
//            style: "solid",
//            width: 1,
//            opacity: 0.5
//        },
//        visualVariables: [{
//            type: "size",
//            minDataValue: 0,
//            maxDataValue: 2300,
//            minSize: "3px",
//            maxSize: "7px"
//        }]
//    };

//    const layerLandLabelClass = {
//        // autocasts as new LabelClass()
//        symbol: {
//            type: "text",  // autocasts as new TextSymbol()
//            color: "#faf884",
//            font: {  // autocast as new Font()
//                //family: "Playfair Display",
//                size: 10,
//                weight: "bold"
//            }
//        },
//        labelPlacement: "above-center",
//        labelExpressionInfo: {
//            expression: "$feature.NAME"
//        }
//    };

//    const pt = new PopupTemplate({
//        title: "<b>{TYPE}</b>",
//        content: "<table class='table table-responsive'><tr><td><p>{NOTE}</p></td></tr></table>"
//    })

//    ARCGIS_LAYERS.layerPoi = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.POI_SERVICE,
//        outFields: "*",
//        //renderer: poirenderer,
//        popupTemplate: pt,
//        visible: false,
//    });

//    ARCGIS_LAYERS.layerAirport = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.AIRPORT_SERVICE,
//        outFields: "*",
//    });

//    ARCGIS_LAYERS.layerRegion = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/0",
//        id: 'regionLayer',
//        definitionExpression: 'ID IN (' + cityIds.join(',') + ')',
//    });

//    ARCGIS_LAYERS.layerRoads = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.ROAD_SERVICE,
//        id: 'roadLayer',
//        renderer: roadRenderer,
//        labelingInfo: [layerLandLabelClass]
//    });

//    ARCGIS_LAYERS.layerLand = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.RESIDENTIAL_AREA_SERVICE,
//        labelingInfo: [layerLandLabelClass]
//    });

//    ARCGIS_LAYERS.layerYashayismenteqeleri = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.RESIDENTIAL_AREA_SERVICE,
//        id: 'yashayishLayer',
//        visible: false,
//        definitionExpression: "1=1"
//    });

//    ARCGIS_LAYERS.layerHydroObject = new FeatureLayer({
//        url: serviceUrl + serviceQueryVersion + "/MapServer/" + ARCGIS_SERVICES.HYDRO_OBJECTS_SERVICE,
//        id: "hydroObjectsLayer",
//        visible: false,
//        labelingInfo: [layerLandLabelClass],
//    });
//});
