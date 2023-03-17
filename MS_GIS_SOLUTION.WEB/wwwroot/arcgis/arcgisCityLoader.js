
var view;
var map;

require([
    "esri/Map",
    "esri/views/MapView",
    "esri/layers/FeatureLayer",
], function (Map, MapView, FeatureLayer) {

    let layerRegion = new FeatureLayer({
        url: serviceUrl + "/Query/MapServer/1",
        id: 'regionLayer',
    });

    layerRegion.queryFeatures().then((data, error) => {
        if (error) {
        }

        let { features } = data;
        let selectize = $selectCity[0].selectize;

        features.map((item, index) => {
            selectize.addOption({ value: item.attributes.Region_id, text: item.attributes.Region_name });
        });

        selectize.refreshOptions();
    });


    $('#cmbCity').change(function () {
        let cmbSettlement = document.getElementById("cmbSettlement");

        let layerSettlement = new FeatureLayer({
            url: serviceUrl + "/Query/MapServer/0",
            id: 'regionLayer',
            definitionExpression: "Region_id='" + $(this).val() + "'",
            orderByFields: ["Resid_ID asc"]
        });

        layerSettlement.queryFeatures().then((data, error) => {

            if (error) {
            }

            let { features } = data;
            let selectize = $selectSettlement[0].selectize;
            selectize.clearOptions();

            features.map((item, index) => {
                selectize.addOption({ value: item.attributes.Resid_ID, text: item.attributes.Name });
            });

            selectize.refreshOptions();
        })
    })

})