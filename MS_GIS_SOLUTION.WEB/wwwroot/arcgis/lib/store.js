var stores = {
    adminUnit: getStore('/AdminUnit/LoadIdName'),
    sourceType: getStore('/SourceType/LoadIdName'),
    source: getStore('/Source/LoadIdName'),
    activityStatus: getStore('/ActivityStatus/LoadIdName'),
    technicalStatus: getStore('/TechnicalStatus/LoadIdName'),
    appointment: getStore('/Appointment/LoadIdName'),
    pumpStationType: getStore('/PumpStationType/LoadIdName'),
    pumpStationEngineType: getStore('/PumpStationEngineType/LoadIdName'),
    ownershipType: getStore('/OwnershipType/LoadIdName'),
    owner: getStore('/Owner/LoadIdName'),
    protectionType: getStore('/ProtectionType/LoadIdName'),
    hydroJunctionType: getStore('/HydroJunctionType/LoadIdName')
};



//return devextreme customStore 
function getStore(url, key = "Id", loadParams = {}) {
    return new DevExpress.data.AspNet.createStore({ key: key, loadUrl: url, loadParams: loadParams });
}

//return devextreme DataSource
function getDatasource(url, key = "Id", loadParams = {}) {
    return new DevExpress.data.DataSource({
        store: getStore(url, key, loadParams),
        paginate: true,
        pageSize: 10
    });
}





//Filter grid data store
function createDynamicStore(options) {
    return new DevExpress.data.AspNet.createStore({ key: options.key, loadUrl: options.url, loadParams: options.loadParams, loadMethod: options.storeOptions.loadMethod });
}

//Filter grid datasource
function createDynamicStore(options) {
    return new DevExpress.data.DataSource({
        store: createDynamicStore(options),
        paginate: true,
        pageSize: 10
    });
}