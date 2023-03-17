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
],
    function (
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


        view.ui.move(["zoom"], "top-left");

        let homeBtn = new Home({
            view: view,
        });

        view.ui.add(homeBtn, "top-left");
        homeBtn.domNode.title = "Əsas / Home ";
        homeBtn.domNode.ariaLabel = "Əsas /Home ";


        IdentityManager.registerToken({
            server: "https://datumglb.com/arcgis/rest/services",
            token: token.token,
            outFields: ["*"]
        });

        let dynamicLayer = new FeatureLayer({
            url: serviceUrl + serviceId,
            f: 'json',
            visible: true,
            outFields: ['*'],
            returnGeometry: true,

        });

        dynamicLayer.definitionExpression = dynamicDefinitionExpression;
        map.add(dynamicLayer);

        if (objectId > 0) {
            let loadQuery = dynamicLayer.createQuery();
            loadQuery.geometry = view.extent;
            loadQuery.returnGeometry = true;
            loadQuery.where = dynamicDefinitionExpression;
            dynamicLayer.queryFeatures(loadQuery).then((data, error) => {

                let { features } = data;


                features.filter(f => f !== null).map(f => {
                    let { OBJECTID } = f.attributes;
                    if (objectId === OBJECTID) {
                        let { geometry } = f;

                        if (serviceId === 5 || serviceId === 0) {
                            view.goTo({ geometry, zoom: 15, duration: 2500 });
                        } else {
                            view.goTo(f.geometry.extent?.expand(200), { duration: 2500 });
                        }


                        //view.goTo(f.geometry.extent?.expand(200), { duration: 2000 });
                        //f.geometry.extend = view.extent;
                        //f.geometry === null ? view.goTo({ target: f, zoom: 10, duration: 2000 }) : view.goTo(f.geometry.extent?.expand(200), { duration: 2000 });
                    }
                });
            });
        }

        view.on("click", function (event) {
            highlightGraphic(event);
        });

        function is_empty(x) {

            if ((x == 'undefined') || (x == null) || (x == false) || (x.length == 0) || (x == 0) || (x == "")) {
                return true;
            }

            return false;
        }

        function highlightGraphic(evnt) {
            view.hitTest(evnt /*{ exclude: view.graphics }*/).then(function (response) {

                view.whenLayerView(response.results[0].layer).then((layerView) => {
                    if (highlightSelect) {
                        highlightSelect.remove();
                    }
                    let { name } = response.results[0].graphic.attributes;
                    if (is_empty(name)) {
                        objectId = response.results[0].graphic.attributes.OBJECTID;
                        setObjectId();

                        if (serviceId == 0) {
                            let { id_pump_station } = response.results[0].graphic.attributes;
                            $("input[name='IdPumpStation']").val(id_pump_station);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 1) {
                            $('#adminunitform').dxForm('instance').getEditor("Length").option('value', length);
                            let { id_channel_admin_unit } = response.results[0].graphic.attributes;
                            $("input[name='IdChannelAdminUnit']").val(id_channel_admin_unit);

                            $('#adminunitform').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 2) {

                            $('#adminunitform').dxForm('instance').getEditor("Length").option('value', length);
                            $('#adminunitform').dxForm('instance').getEditor("Objectid").option('value', objectId);
                            let { id_closed_channel_admin_unit } = response.results[0].graphic.attributes;
                            $("input[name='IdClosedChannelAdminUnit']").val(id_closed_channel_admin_unit);
                        }
                        else if (serviceId == 3) {

                            $('#adminunitform').dxForm('instance').getEditor("Length").option('value', length);
                            $('#adminunitform').dxForm('instance').getEditor("Objectid").option('value', objectId);
                            let { id_collector_admin_unit } = response.results[0].graphic.attributes;
                            $("input[name='IdCollectorAdminUnit']").val(id_collector_admin_unit);
                        }
                        else if (serviceId == 4) {
                            let { id_hydrotechnical_installations } = response.results[0].graphic.attributes;
                            $("input[name='IdHydrotechnicalInstallations']").val(id_hydrotechnical_installations);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 5) {

                            let { id_artesian_subartesian_well } = response.results[0].graphic.attributes;
                            $("input[name='IdArtesianSubartesianWell']").val(id_artesian_subartesian_well);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 6) {

                            let { id_hydro_junctions } = response.results[0].graphic.attributes;
                            $("input[name='IdHydroJunctions']").val(id_hydro_junctions);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 7) {

                            let { id_exploitation_roads } = response.results[0].graphic.attributes;
                            $("input[name='IdExploitationRoads']").val(id_exploitation_roads);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 8) {

                            let { id_protection_barriers } = response.results[0].graphic.attributes;
                            $("input[name='IdProtectionBarriers']").val(id_protection_barriers);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 9) {

                            let { id_winter_water_supply_systems } = response.results[0].graphic.attributes;
                            $("input[name='IdWinterWaterSupplySystems']").val(id_winter_water_supply_systems);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 11) {

                            let { id_reservoir_system } = response.results[0].graphic.attributes;
                            $("input[name='IdReservoirSystem']").val(id_reservoir_system);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }
                        else if (serviceId == 12) {

                            let { id_buildings_constructions } = response.results[0].graphic.attributes;
                            $("input[name='IdBuildingsConstructions']").val(id_buildings_constructions);
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', objectId);

                        }

                        highlightSelect = layerView.highlight(objectId);

                        $("#mapPopup").dxPopup({ 'title': `İdentifikasiya kodu ${objectId} olan obyekt seçildi` });

                        var myDialog = DevExpress.ui.dialog.custom({
                            title: "Obyekt seçildi",
                            messageHtml: `<b>Seçilmiş obyektin kodu: ${objectId} </b>`,
                            buttons: [{
                                text: "Tamam",
                                onClick: function (e) {
                                    return { buttonText: e.component.option("text") }
                                }
                            },
                            ]
                        });
                        myDialog.show().done(function (dialogResult) {
                        });
                    }
                    else {
                        objectId = response.results[0].graphic.attributes.OBJECTID;
                        var myDialog = DevExpress.ui.dialog.custom({
                            title: "Obyekt seçildi",
                            messageHtml: `<b>Bu obyekt ${objectId} artıq istifadə edilib !</b>`,
                            buttons: [{
                                text: "Tamam",
                                onClick: function (e) {
                                    return { buttonText: e.component.option("text") }
                                }
                            },
                            ]
                        });
                        myDialog.show().done(function (dialogResult) {
                        });


                        if ($('#adminunitform').dxForm('instance') != null) {
                            $('#adminunitform').dxForm('instance').getEditor("Objectid").option('value', null);
                        }
                        else {
                            $('#form').dxForm('instance').getEditor("Objectid").option('value', null);
                        }
                    }

                });
            });
        }

        view.whenLayerView(dynamicLayer).then((layerView) => {
            if (highlightSelect) {
                highlightSelect.remove();
            }

            highlightSelect = layerView.highlight(objectId);
        });

        function setObjectId() {
            $("input[name='Objectid']").val(objectId);
        }

    });
