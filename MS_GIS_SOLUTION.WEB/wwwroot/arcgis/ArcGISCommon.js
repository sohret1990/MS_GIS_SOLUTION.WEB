
function loadRegionsFromArcGis(where) {

    let filter = {
        f: 'json',
        outFields: ["id_admin_unit,name"],
        returnGeometry: false,
        orderByFields: ['name'],
        where: where,
        token: token.token
    };

    return new Promise((resolve, reject) => {
        let url = serviceUrl + ARCGIS_SERVICES.INZIBATI_ERAZI_VAHIDLERI + '/query?outFields=OBJECTID,name&returnGeometry=false&returnDistinctValues=true&orderByFields=name';
        $.get(url, filter).then((regions) => {
            
            let { features } = regions;
            let regionList = [];
            features.map((item, index) => {

                let { id_admin_unit, name } = item.attributes;
                regionList.push({ Id: id_admin_unit, Name: name });
            });
            resolve(regionList);

        }).catch((err) => {
            reject(err.message)
            notify('error', err.message);
        });
    });
}




var regionStore = new DevExpress.data.CustomStore({
    key: "Id",
    load: function (loadOptions) {
        return loadRegionsFromArcGis("id_admin_unit > 0 and is_deleted=0");
    },
    byKey: function (key) {
        return loadRegionsFromArcGis("id_admin_unit > 0 and is_deleted=0");
    },
});




function getLayerStore() {
    let d = $.Deferred();    
    $.get('/CommonEntity/GetUserLayers').then((resp) => {
        d.resolve(resp.map((m) => {
            return {
                id: m.Id,
                name: m.Name
            }
        }));
    });

    return d.promise();
}


function getLayerStoreIdNameList() {
    let d = $.Deferred();
    getLayerStore().then((items) => {
        d.resolve(items.filter(m => m.Id !== 14));
    });
    
    return d.promise();
}



var layerStore = new DevExpress.data.CustomStore({
    key: "id",
    load: function (loadOptions) {
        return getLayerStore();
    },
    byKey: function (key) {
        return getLayerStore();
    },
});

var layerIdNameListStore = new DevExpress.data.CustomStore({
    key: "id",
    load: function (loadOptions) {
        return getLayerStoreIdNameList();
    },
    byKey: function (key) {
        return getLayerStoreIdNameList();
    },
});


//function showLayerInfoPopup() {
//    //let popup = $('#layerInfoPopup').dxPopup('instance');
//    let filterPopup = $('#filterPopup').dxPopup('instance');

//    //popup.option('width', ($(window).width() - parseInt(filterPopup.option('width'))) + "px");
//    //popup.option('height', '400px');
//    //popup.show();
//}


//function hideLayerInfoPopup() {
//    //let popup = $('#layerInfoPopup').dxPopup('instance');
//    //popup.hide();
//}



function showLayerInfoPopup() {
    let filterPopup = $('#filterPopup').dxPopup('instance');
}