var view;
var map;

require([
    "esri/Map",
    "esri/views/MapView",
    "esri/widgets/Expand",
    "esri/widgets/BasemapGallery",
    "esri/layers/FeatureLayer",
    "esri/widgets/LayerList",
    "esri/Basemap",
    "esri/layers/GroupLayer",
    "esri/widgets/Home",
    "esri/Graphic",
    "esri/PopupTemplate",
    "esri/layers/TileLayer",
    "esri/geometry/geometryEngine",
], function (Map, MapView, Expand, BasemapGallery, FeatureLayer, LayerList, Basemap, GroupLayer, Home, Graphic, PopupTemplate, TileLayer, geometryEngine) {

    const map = new Map({});

    view = new MapView({
        container: "map",
        map: map,
        constraints: {
            rotationEnabled: false,
        },
    });

})