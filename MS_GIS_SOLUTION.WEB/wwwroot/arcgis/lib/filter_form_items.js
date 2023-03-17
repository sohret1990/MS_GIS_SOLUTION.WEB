
//return filter form instance
function getFormInstance() {
    return $('#frmInfoLayerFilter').dxForm('instance');
}


var commonFilterFormItems = {
    name: {
        dataField: 'Name',
        colSpan: 2,
        editorType: 'dxTextBox',
        label: { text: 'Ad' },
    },
    adminUnit: {
        dataField: 'FkIdAdminUnit',
        editorType: 'dxTagBox',
        label: { text: 'Yerləşdiyi ərazi' },
        editorOptions: {
            dataSource: stores.adminUnit,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            paginate: true,
            pageSize: 10,
            showSelectionControls: true
        }
    },
    sourceType: {
        dataField: 'FkIdSourceType',
        editorType: 'dxTagBox',
        label: { text: 'Mənbə tipi' },
        editorOptions: {
            id: "tgbSourceType",
            dataSource: stores.sourceType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            paginate: true,
            pageSize: 10,
            showSelectionControls: true,
            onValueChanged: function (e) {
                let dropDown = $('#frmInfoLayerFilter').dxForm('instance').getEditor("FkIdSource");
                let dataSource = dropDown?.getDataSource();

                dataSource._storeLoadOptions.customQueryParams = {
                    sourceType: $('#frmInfoLayerFilter').dxForm('instance').getEditor("FkIdSourceType").option('value')
                };
                dataSource.load();
            }
        }
    },
    source: {
        dataField: 'FkIdSource',
        colSpan: 2,
        editorType: 'dxTagBox',
        label: { text: 'Mənbə' },
        editorOptions: {
            dataSource: stores.source,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    activityStatus: {
        dataField: 'FkIdActivityStatus',
        editorType: 'dxTagBox',
        label: { text: 'Fəaliyyətdə olub olmaması' },
        editorOptions: {
            dataSource: stores.activityStatus,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    technicalStatus: {
        dataField: 'FkIdTechnicalStatus',
        editorType: 'dxTagBox',
        label: { text: 'Texniki vəziyyəti' },
        editorOptions: {
            dataSource: stores.technicalStatus,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    appointment: {
        dataField: 'FkIdAppointment',
        editorType: 'dxTagBox',
        label: { text: 'Məqsədi' },
        editorOptions: {
            dataSource: stores.appointment,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    pumpStationType: {
        dataField: 'FkIdPumpStationType',
        editorType: 'dxTagBox',
        label: { text: 'Nasos stansiyasının növü' },
        editorOptions: {
            dataSource: stores.pumpStationType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    pumpStationEngineType: {
        dataField: 'FkIdPumpStationEngineType',
        editorType: 'dxTagBox',
        label: { text: 'Nasos stansiyasının tipi' },
        editorOptions: {
            dataSource: stores.pumpStationEngineType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    ownershipType: {
        dataField: 'FkIdOwnershipType',
        editorType: 'dxTagBox',
        label: { text: 'Mülkiyyət növü' },
        editorOptions: {
            dataSource: stores.ownershipType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    owner: {
        dataField: 'FkIdOwner',
        editorType: 'dxTagBox',
        label: { text: 'Mülkiyyətçi' },
        editorOptions: {
            dataSource: stores.owner,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    protectionType: {
        dataField: 'FkIdProtectionType',
        editorType: 'dxTagBox',
        label: { text: 'Mühafizə növü' },
        editorOptions: {
            dataSource: stores.protectionType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    }
};

var pumpStationFilterFormItems = [
    commonFilterFormItems.name,
    commonFilterFormItems.adminUnit,
    {
        dataField: 'AdditionalAdminUnit',
        label: { text: 'Kənd, qəsəbə' },
    },
    commonFilterFormItems.sourceType,
    commonFilterFormItems.source,
    commonFilterFormItems.activityStatus,
    commonFilterFormItems.technicalStatus,
    commonFilterFormItems.appointment,
    commonFilterFormItems.pumpStationType,
    commonFilterFormItems.pumpStationEngineType,
    commonFilterFormItems.ownershipType,
    commonFilterFormItems.owner,
    commonFilterFormItems.protectionType
];

var hydroTechnicalInstallationFilterFormItems = [
    commonFilterFormItems.name,
    commonFilterFormItems.adminUnit,
    commonFilterFormItems.sourceType,
    commonFilterFormItems.source,
    commonFilterFormItems.activityStatus,
    commonFilterFormItems.technicalStatus,
    commonFilterFormItems.appointment,
    commonFilterFormItems.pumpStationType,
    commonFilterFormItems.pumpStationEngineType,
    commonFilterFormItems.ownershipType,
    commonFilterFormItems.owner,
    commonFilterFormItems.protectionType
];

var hydrojunctionFilterFormItems = [
    commonFilterFormItems.name,
    commonFilterFormItems.adminUnit,
    commonFilterFormItems.sourceType,
    commonFilterFormItems.source,
    commonFilterFormItems.activityStatus,
    commonFilterFormItems.technicalStatus,
    commonFilterFormItems.appointment,
    {
        dataField: 'FkIdHydroJunctionType',
        editorType: 'dxTagBox',
        label: { text: 'Hidroqovşaq tipleri' },
        editorOptions: {
            dataSource: stores.hydroJunctionType,
            displayExpr: "Name",
            valueExpr: "Id",
            searchEnabled: true,
            showSelectionControls: true,
            paginate: true,
            pageSize: 10,
        },
        paginate: true,
        pageSize: 10
    },
    commonFilterFormItems.pumpStationEngineType,
    commonFilterFormItems.ownershipType,
    commonFilterFormItems.owner,
    commonFilterFormItems.protectionType,
    {
        dataField: 'ProtectionZoneArea',
        editorType: 'dxNumberBox',
        label: { text: 'Mühafizə sahəsi (ha)' },
    }, {
        dataField: 'ServiceArea',
        editorType: 'dxNumberBox',
        label: { text: 'Xidmət etdiyi sahə' },
    }, {
        dataField: 'MaxWaterReleaseCapacity',
        editorType: 'dxNumberBox',
        label: { text: 'Maksimal suburaxma qabiliyyəti (m³/s)' },
    }, {
        dataField: 'DamLength',
        editorType: 'dxNumberBox',
        label: { text: 'Dambanın uzunluğu (km)' },
    }, {
        dataField: 'ServiceAdminUnit',
        editorType: 'dxNumberBox',
        label: { text: 'Xidmət etdiyi suvarma regionu' },
    }, {
        dataField: 'FkIdFedObjects',
        editorType: 'dxNumberBox',
        label: { text: 'Qidalandırdığı kanallar' },
    }, {
        dataField: 'ExploitationDate',
        editorType: 'dxNumberBox',
        label: { text: 'İstismara verildiyi tarix(il)' },
    }, {
        dataField: 'Controlled',
        label: { text: 'Kontrol edilib' },
    }
];

var lakeFilterFormItems = [
    commonFilterFormItems.name,
    commonFilterFormItems.adminUnit,
    {
        dataField: 'Volume',
        editorType: 'dxNumberBox',
        label: { text: 'Həcmi' },
    }
];

var riverFilterFormItems = [
    commonFilterFormItems.name,
    {
        dataField: 'Lenght',
        editorType: 'dxNumberBox',
        label: { text: 'Uzunluğu' },
    }
];

var exploitationRoadFormFilterItems = [
    commonFilterFormItems.name,
    commonFilterFormItems.adminUnit,
    commonFilterFormItems.activityStatus,
    commonFilterFormItems.ownershipType,
    commonFilterFormItems.owner,
    {
        dataField: 'Length',
        editorType: 'dxNumberBox',
        label: { text: 'Uzunluğu' },
    },
    {
        dataField: 'LengthA',
        editorType: 'dxNumberBox',
        label: { text: 'Uzunluğu' },
    },
    {
        dataField: 'LengthB',
        editorType: 'dxNumberBox',
        label: { text: 'Uzunluğu' },
    },
    {
        dataField: 'RoadWidth',
        editorType: 'dxNumberBox',
        label: { text: 'Uzunluğu' },
    }
];

