var view;
var map;
//var filterExpression = objectId === 0 ? '1=1 and is_deleted=0' : ' is_deleted=0 and OBJECTID=' + objectId;



require([
    "esri/views/MapView",
    "esri/layers/FeatureLayer",
    "esri/widgets/Home",
    "esri/WebMap",
    "esri/identity/IdentityManager",
    "esri/geometry/Polygon",

    "dojo/domReady!"
], function (
    MapView,
    FeatureLayer,
    Home,
    WebMap,
    IdentityManager,
    Polygon
) {
    map = new WebMap({
        basemap: 'satellite',
    });

    let viewConfig = {
        container: "map",
        map: map,
        zoom: 8,
        center: [47.149635, 40.185132],
        constraints: {
            rotationEnabled: true,
        },
    };

    view = new MapView(viewConfig);

    view.ui.add("btnSaveMany", 'top-left');


    document.getElementById("btnSaveMany").addEventListener("click",
        function() {
            if (confirm('yadda saxlamaq istədiyinizdən əminsiniz?')) {
                
            }
        });

    IdentityManager.registerToken({
        server: "https://datumglb.com/arcgis/rest/services",
        token: token.token
    });

    const layerLandLabelClass = {
        // autocasts as new LabelClass()
        symbol: {
            type: "text",  // autocasts as new TextSymbol()
            color: "#faf884",
            font: {  // autocast as new Font()
                //family: "Playfair Display",
                size: 10,
                weight: "bold"
            }
        },
        labelPlacement: "above-center",
        labelExpressionInfo: {
            expression: "$feature.name"
        }
    };

    let dynamicLayer = new FeatureLayer({
        url: serviceUrl + serviceId,
        f: 'json',
        visible: true,
        outFields: ['*'],
        labelingInfo: [layerLandLabelClass]
    });

    //dynamicLayer.definitionExpression = ["fk_id_source=3626"];
    let loadQuery = dynamicLayer.createQuery();
    loadQuery.returnGeometry = true;
    loadQuery.where = "fk_id_source<>null and fk_id_source_type=1 and is_deleted=0";
    map.add(dynamicLayer);


    view.on("click", function (event) {
        highlightGraphic(event);
    });

    function highlightGraphic(evnt) {
        view.hitTest(evnt /*{ exclude: view.graphics }*/).then(function (response) {

            view.whenLayerView(response.results[0].layer).then((layerView) => {
                if (highlightSelect) {
                    highlightSelect.remove();
                }

                objectId = response.results[0].graphic.attributes.OBJECTID;
                let { length } = response.results[0].graphic.attributes;

                $('#adminunitform').dxForm('instance').getEditor("Length").option('value', length);
                highlightSelect = layerView.highlight(objectId);


            });
        });
    }

    function setObjectId() {
        $("input[name='Objectid']").val(objectId);
    }

});
