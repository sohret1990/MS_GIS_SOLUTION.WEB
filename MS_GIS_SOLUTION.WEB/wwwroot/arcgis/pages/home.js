/// require store.js, filter_grid_columns.js,  filter_form_items.js

var layerInfoGrid = getGridInstance('instance');
var sourceUrl = '';
var applicationDbModel = [{ fieldName: '', columnName: '' }];

function getSelectedLayerIds() {
    let keys = $('#filterAccordion').dxAccordion('instance').option("selectedItemKeys").map(m => m.Id);
    return keys;
}

function applyFilter() {
    view.graphics.removeAll();
    filteredLakeIds = filteredRiverIds = filteredReservoirIds = filteredBuildingIds = filteredLakeIds = filteredSupplyIds = filteredBarrierIds = filteredRoadIds = filtereHydrojunctionIds = filteredArtesianIds = filtereHydrotechnicalInstallationIds = filtereCollectorIds = filtereClosedChannelIds = filtereChannelIds = filterePumpStationIds = [];

    let popup = $('#layerInfoPopup').dxPopup('instance');
    let filterPopup = $('#filterPopup').dxPopup('instance');

    popup.option('width', ($(window).width() - parseInt(filterPopup.option('width'))) + "px");
    popup.option('height', '430px');
    popup.show();

    clearMap();

    configureTabPanel();
    getSelectedLayerIds().map(m => {
        getArcgisFilter(m);
    });
}

function configureTabPanel() {
    let tabLayers = $('#tabLayers').dxTabPanel('instance');
    let selectedKeys = getSelectedLayerIds();
    tabLayers.option('items').map(m => m.visible = selectedKeys.indexOf(m.Id) > -1);
    let activeTabIndex = tabLayers.option('items').map(m => m.visible).indexOf(true);
    tabLayers.option('selectedIndex', activeTabIndex);

    tabLayers.repaint();
}

function getArcgisFilter(layerId) {

    let d = $(`#filterForm-${layerId}`).dxForm('instance')?.option('formData');
    let adminUnitIds = $('#adminUnitTagBox').dxTagBox('instance').option('value');
    d.fk_id_admin_unit = adminUnitIds;

    let filterGridOptions = {
        url: '',
        key: "",
        loadParams: null,
        storeOptions: {
            loadMethod: 'POST'
        }
    };

    switch (layerId) {
        case ARCGIS_SERVICES.NASOS_STANSIYALARI:

            $.post('/PumpStation/filter', { data: d }).then((response) => {
                filterePumpStationIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });

            break;
        case ARCGIS_SERVICES.ACHIQ_SUVARMA_SHEBEKESI:
            $.post('/Channel/filter', { data: d }).then((response) => {
                filtereChannelIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.QAPALI_SUVARMA_SHEBEKESI:
            $.post('/ClosedChannel/filter', { data: d }).then((response) => {
                filtereClosedChannelIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.KOLLEKTOR_DRENAJ_SHEBEKESI:

            $.getJSON('/AdminUnit/LoadIdName', { loadOptions: {} }).then((res) => {
                let q = 'is_deleted=0 and fk_id_admin_unit in(' + res.data.map(m => m.Id).join(",") + ')';

                if (Object.keys(d).length == 1 && d.fk_id_admin_unit.length == 0) {
                    applyToMap(layerId, q);
                    $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
                } else {
                    $.post('/Collector/filter', { data: d }).then((response) => {
                        console.log(response);
                        filtereCollectorIds = response;
                        q += ` and OBJECTID IN(${response.join(',')})`;
                        applyToMap(layerId, q);
                        $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
                    });
                }
            });
            break;
        case ARCGIS_SERVICES.HIDROTEXNIKI_QURGULAR:
            filtereHydrotechnicalInstallationFormValues = d;

            $.getJSON('/AdminUnit/LoadIdName', { loadOptions: {} }).then((res) => {
                let q = 'is_deleted=0 and fk_id_admin_unit in(' + res.data.map(m => m.Id).join(",") + ')';

                if (Object.keys(d).length == 1 && d.fk_id_admin_unit.length == 0) {
                    applyToMap(layerId, q);
                    $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
                } else {
                    $.post('/HydrotechnicalInstallation/filter', { data: d }).then((response) => {
                        filtereHydrotechnicalInstallationIds = response.filter(m => m !== null && m !== "");
                        q += ` and OBJECTID IN(${response.join(',')})`;
                        applyToMap(layerId, q);
                        $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
                    });
                }

            });

            break;
        case ARCGIS_SERVICES.ARTEZIAN_VE_SUBARTEZIAN_QUYULARI:
            $.post('/ArtesianSubartesianWell/filter', { data: d }).then((response) => {
                filteredArtesianIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.HIDROQOVSHAQ:

            $.post('/HydroJunction/filter', { data: d }).then((response) => {
                filtereHydrojunctionIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.ISTISMAR_YOLLARI:
            $.post('/ExploitationRoads/filter', { data: d }).then((response) => {
                filteredRoadIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;

                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.MUHAFIZE_BENDLERI:
            $.post('/ProtectionBarrier/filter', { data: d }).then((response) => {
                filteredBarrierIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.QISH_OTLAQLARIN_SU_TEMINATI_SISTEMLERI:
            $.post('/WinterWaterSupply/filter', { data: d }).then((response) => {
                filteredSupplyIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.CHAYLAR:
            $.post('/River/filter', { data: d }).then((response) => {
                filteredRiverIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.SU_TUTARLAR:
            $.post('/ReservoirSystem/filter', { data: d }).then((response) => {

                filteredReservoirIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.BINALAR_VE_TIKILILER:
            $.post('/BuildingAndConstructions/filter', { data: d }).then((response) => {

                filteredBuildingIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        case ARCGIS_SERVICES.GOLLER:
            $.post('/Lake/filter', { data: d }).then((response) => {

                filteredLakeIds = response;
                let q = `OBJECTID IN(${response.join(',')})`;
                applyToMap(layerId, q);
                $(`#filterInfoGrid-${layerId}`).dxDataGrid('instance').getDataSource().reload();
            });
            break;
        default:

            break;
    }
}

function clearFilter() {

    $('#adminUnitTagBox').dxTagBox('instance').option('value', []);

    ARCGIS_SERVICE_ENTITY_SERVICES.map(m => {
        let form = $(`#filterForm-${m.id}`).dxForm('instance');
        form?.option('formData', {});
    });

    applyFilter();

    let popup = $('#layerInfoPopup').dxPopup('instance');
    popup.hide();

}

function applyToMap(layerId, query) {
    view.graphics.removeAll();

    require([
        "esri/layers/FeatureLayer",
        "esri/geometry/Polygon",
        "esri/popup/content/AttachmentsContent",
        "esri/popup/content/TextContent",
        "esri/rest/support/RelationshipQuery",
        "esri/widgets/FeatureTable",
        "esri/core/reactiveUtils",
    ],
        function (
            FeatureLayer,
            Polygon,
            AttachmentsContent,
            TextContent,
            RelationshipQuery,
            FeatureTable,
            reactiveUtils
        ) {


            let layer = new FeatureLayer({
                url: serviceUrl + layerId,
                returnGeometry: true,
                id: 'filterlayer-' + layerId,
                outFields: ["*"],
                f: 'pjson',
                //query: { where: 'f=pjson&' + query },
                definitionExpression: query,
                visible: true,
            });

            let labelClass = {
                // autocasts as new LabelClass()
                symbol: {
                    type: "text",  // autocasts as new TextSymbol()
                    color: "white",
                    font: {  // autocast as new Font()
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

            if (layerId !== 16) {
                layer.labelingInfo = [labelClass];
            }

            if (layerId === ARCGIS_SERVICES.ARTEZIAN_VE_SUBARTEZIAN_QUYULARI) {
                layer.labelingInfo = [{
                    // autocasts as new LabelClass()
                    symbol: {
                        type: "text",  // autocasts as new TextSymbol()
                        color: "black",
                        font: {  // autocast as new Font()
                            //family: "Playfair Display",
                            size: 8,
                            //weight: "bold"
                        }
                    },
                    //labelPlacement: "above-center",
                    labelExpressionInfo: {
                        expression: "$feature.no"
                    }
                }]
            }

            map.add(layer);
        });
}

function zoomToGraphic(layerId, query) {

    view.graphics.removeAll();

    require([
        "esri/layers/FeatureLayer",
        "esri/geometry/Polygon",
        "esri/popup/content/AttachmentsContent",
        "esri/popup/content/TextContent",
        "esri/rest/support/RelationshipQuery",
        "esri/widgets/FeatureTable",
        "esri/core/reactiveUtils",
        "esri/geometry/Polyline",
        "esri/Graphic",
    ],
        function (
            FeatureLayer,
            Polygon,
            AttachmentsContent,
            TextContent,
            RelationshipQuery,
            FeatureTable,
            reactiveUtils,
            Polyline,
            Graphic
        ) {
            view.popup.close();
            //view.zoom = 1;
            const features = [];
            let layer = new FeatureLayer({
                view: view,
                url: serviceUrl + layerId,
                visible: true,
                id: 'filterlayer-' + layerId,
                returnGeometry: true,
                f: 'pjson',
                outFields: ["*"],
                definitionExpression: query,
                popupTemplate: {
                    title: "{name}",
                    content: function infoPopup(feature) {
                        // 1. Set how the attachments should display within the popup
                        const attachmentsElement = new AttachmentsContent({
                            displayType: "list"
                        });

                        const textElement = new TextContent();
                        return [textElement, attachmentsElement];
                    }
                }
            });

            view.graphics.removeAll();
            layer.queryFeatures().then((data) => {
                let { features } = data;
                features.map((item) => {
                    var geometryTpe = item.layer.geometryType;

                    console.log(geometryTpe)

                    let { geometry, layer } = item;
                    let iconUrl = "/Content/mapicons/red-pin.png";
                    let color = [];
                    if (layer.sourceJSON.drawingInfo.renderer.symbol) {
                        color = layer.sourceJSON.drawingInfo.renderer.symbol.color
                    } else if (layer.sourceJSON.drawingInfo.renderer.defaultSymbol) {
                        color = layer.sourceJSON.drawingInfo.renderer.defaultSymbol.color;
                    } else {
                        color = [0, 0, 0, 1];
                    }

                    if (color[2] > 200) {
                        color[2] = 5;
                    } else {
                        color[2] = 250;
                    }

                    let graphic;
                    let symbol;

                    if (geometryTpe === "point") {

                        symbol = {
                            type: "simple-marker", // autocasts as new SimpleMarkerSymbol()
                            color: "yellow",
                            size: 8,
                            outline: { // autocasts as new SimpleLineSymbol()
                                width: 5,
                                color: "darkblue"
                            }
                        };
                        view.goTo(features[0]);
                        //view.zoom = 12;

                    } else if (geometryTpe === "polyline") {

                        symbol = {
                            attributes: item.data,
                            type: "simple-line", // autocasts as new SimpleFillSymbol
                            color: color, //"#06a9f4",
                            style: "solid",
                            width: 3,
                            cap: "round",
                            join: "round",
                        };
                    } else if (geometryTpe === "polygon") {

                        symbol = {
                            attributes: item.data,
                            type: "simple-marker", // autocasts as new SimpleFillSymbol
                            color: color, //"#06a9f4",
                            style: "solid",
                            width: 3,
                            cap: "round",
                            join: "round",
                        };
                    }
                    graphic = new Graphic({
                        extent: view.extent,
                        geometry: geometry,
                        symbol: symbol,
                        attributes: item.data,
                    });

                    view.graphics.add(graphic);
                    view.goTo(graphic.geometry.extent.expand(7));
                });

            });

        });
}

function closeFilterTab(layerId) {
    let tab = $('#tabLayers').dxTabPanel('instance');
    tab.option('items').filter(m => m.Id === layerId).map(m => m.visible = false);
    tab.option('selectedIndex', tab.option('items').filter(m => m.visible == true)[0]?.Id);
    tab.repaint();

    let items = $('#filterAccordion').dxAccordion('instance').option("selectedItems");
    let newItems = items.filter(m => m.Id !== layerId);
    $('#filterAccordion').dxAccordion('instance').option("selectedItems", newItems);

    let fl = map.findLayerById('filterlayer-' + layerId);
    if (fl !== null) {
        map.remove(fl);
    }
}

function showChannelHydrotechnicalDevices(channelId) {
    let layer = map.findLayerById('filterlayer-' + 4);
    if (layer !== null) {
        map.remove(layer);
    }

    applyToMap(4, "fk_id_source=" + channelId);
}

function showChannelCoverType(data) {
    console.log(data)
    let layer = map.findLayerById('filterlayer-' + 16);
    if (layer !== null) {
        map.remove(layer);
    }
    applyToMap(16, "id_channel_cover in(" + data.CoverIds.map(m => m.IdChannelCover).join(',') + ')');
    //applyToMap(1, 'fk_id_channel=' + data.IdChannel);
}

function clearMap() {
    map.layers.map(m => {
        if (m.id.includes('filterlayer-')) {
            map.remove(m);
        } else if (m.layerId !== 14) {
            m.visible = false;
        }
    });
}