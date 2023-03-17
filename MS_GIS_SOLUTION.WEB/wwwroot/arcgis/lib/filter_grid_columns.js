let cellTemplate = (element, info) => {
    if (info.rowType !== 'data') return;
    switch (info.column.name) {
        case 'FkIdActivityStatus':
            if (info.data.FkIdActivityStatus == 1) {
                element.append('<span class="badge badge-primary">' + info.text + '</div>');
            } else {
                element.append('<span class="badge badge-danger">' + info.text + '</div>');
            }
            break;
        case 'FkIdTechnicalStatus':
            if (info.data.FkIdTechnicalStatus == 1) {
                element.append('<span class="badge badge-warning">' + info.text + '</div>');
            } else if (info.data.FkIdTechnicalStatus == 2) {

            } else {
                element.append('<span class="badge badge-danger">' + info.text + '</div>');
            }
            break;
        default:
            element.append('<span class="badge badge-secondary">' + info.text + '</div>');
            break;
    }
}


//return LayerInfo Grid instance
function getGridInstance() {
    return $('#layerInfoGrid').dxDataGrid('instance');
}


var commonColumns = {
    objectId: {
        dataField: 'Objectid',
        caption: 'Arcgis kodu',
        visible: false
    },
    name: {
        dataField: 'Name',
        caption: 'Adı',
        width: 120
    },
    adminUnit: {
        dataField: 'FkIdAdminUnit',
        caption: 'Yerləşdiyi ərazi',
        visible: true,
        width: 120,
        cellTemplate: cellTemplate,
        lookup: {
            dataSource: stores.adminUnit,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    activityStatus: {
        dataField: 'FkIdActivityStatus',
        caption: 'Fəaliyyətdə olub-olmaması',
        cellTemplate: cellTemplate,
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.activityStatus,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    technicalStatus: {
        dataField: 'FkIdTechnicalStatus',
        caption: 'Texniki vəziyyəti',
        visible: true,
        width: 120,
        cellTemplate: cellTemplate,
        lookup: {
            dataSource: stores.technicalStatus,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    appointment: {
        dataField: 'FkIdAppointment',
        caption: 'Məqsədi',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.appointment,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    ownershipType: {
        dataField: 'FkIdOwnershipType',
        caption: 'Mülkiyyət növü',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.ownershipType,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    owner: {
        dataField: 'FkIdOwner',
        caption: 'Mülkiyyətçisi',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.owner,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    sourceType: {
        dataField: 'FkIdSourceType',
        caption: 'Mənbə tipi',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.sourceType,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    source: {
        dataField: 'FkIdSource',
        caption: 'Mənbə',
        visible: true,
        width: 120,
        cellTemplate: cellTemplate,
        lookup: {
            dataSource: stores.source,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    exploitationDate: {
        dataField: 'ExploitationDate',
        caption: 'İstismara verildiyi tarix(il)',
        visible: true,
        width: 80
    },
    controlled: {
        dataField: 'Controlled',
        caption: 'Kontrol edildi',
        visible: true,
        width: 100
    },
    protectionType: {
        dataField: 'FkIdProtectionType',
        caption: 'Mühafizə növü',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.protectionType,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    }
};


var pumpStationFilterGridColumns = [
    {
        dataField: 'Objectid',
        caption: '#',
        visible: false
    },
    {
        dataField: 'IdPumpStation',
        caption: '#',
        visible: false
    },
    commonColumns.name,
    commonColumns.adminUnit
    , {
        dataField: 'AdditionalAdminUnit',
        caption: 'Qəsəbə, kənd',
        visible: true,
        width: 120
    },
    commonColumns.sourceType,
    commonColumns.source,
    commonColumns.activityStatus,
    commonColumns.technicalStatus,
    commonColumns.appointment,
    {
        dataField: 'FkIdPumpStationType',
        caption: 'Nasos stansiyasının növü',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.pumpStationType,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    }, {
        dataField: 'FkIdPumpStationEngineType',
        caption: 'Nasos stansiyasının tipi',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.pumpStationEngineType,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    commonColumns.ownershipType,
    commonColumns.owner,
    commonColumns.protectionType,
    {
        dataField: 'ProtectionZoneArea',
        caption: 'Mühafizə sahəsi (kv.m)',
        visible: true,
        width: 60
    }, {
        dataField: 'TotalCapacity',
        caption: 'Ümumi gücü (kvt/saat)',
        visible: true,
        width: 60
    }, {
        dataField: 'ServiceArea',
        caption: 'Xidmət etdiyi sahə',
        visible: true,
        width: 60
    }, {
        dataField: 'Productivity',
        caption: 'Məhsuldarlığı (m³/san)',
        visible: true,
        width: 60
    }, {
        dataField: 'AggregatesCount',
        caption: 'Aqreqatların sayı (ədəd)',
        visible: true,
        width: 60
    }, {
        dataField: 'AggregatesModel',
        caption: 'Aqreqatların markası',
        visible: true,
        width: 100
    },
    commonColumns.exploitationDate, {
        dataField: 'FkIdLocWaterSourceType',
        caption: 'Yerləşdiyi su mənbəyi',
        visible: true
    }, {
        dataField: 'LastInstallationDate',
        caption: 'Nasosun son quraşdırılma tarixi',
        visible: true,
        width: 80
    },
    commonColumns.controlled,
];

var hydrotechnicalInstallationColumns = [
    commonColumns.objectId,
    {
        dataField: 'IdHydrotechnicalInstallations',
        caption: '#',
        visible: false
    },
    commonColumns.name,
    commonColumns.adminUnit,
    {
        dataField: 'FkIdHtiNetworkType',
        caption: 'Qurğu şəbəkə növü',
        visible: true,
        lookup: {
            dataSource: stores.adminUnit,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    commonColumns.activityStatus,
    commonColumns.technicalStatus,
    {
        dataField: 'FkIdHtInstallationType',
        caption: 'Qurğu tipləri',
        visible: true,
        lookup: {
            dataSource: stores.technicalStatus,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    },
    commonColumns.ownershipType,
    commonColumns.owner,
    commonColumns.protectionType,
    {
        dataField: 'ProtectionZoneArea',
        caption: 'Mühafizə sahəsi',
        visible: true,
    },
    commonColumns.sourceType,
    commonColumns.source,
    {
        dataField: 'Depth',
        caption: 'Dərinliyi (m)',
        visible: true,
    }, {
        dataField: 'WaterPermeability',
        caption: 'Su keçirmə qabiliyyəti (m³/s)',
        visible: true,
    }, {
        dataField: 'TechnicalParameters',
        caption: 'Texniki parametrləri',
        visible: true,
    },
    commonColumns.exploitationDate,
    commonColumns.controlled

];

var hydrojunctionColumns = [
    commonColumns.objectId,
    commonColumns.name,
    commonColumns.adminUnit,
    commonColumns.appointment,
    commonColumns.sourceType,
    commonColumns.source,
    commonColumns.activityStatus,
    commonColumns.technicalStatus,
    {
        dataField: 'FkIdHydroJunctionType',
        caption: 'Hidroqovşaq tipleri',
        visible: true,
        width: 120,
        //lookup: {
        //    dataSource: stores.protectionType,
        //    displayExpr: "Name",
        //    valueExpr: "Id"
        //}
    },
    commonColumns.ownershipType,
    commonColumns.owner,
    commonColumns.protectionType,
    {
        dataField: 'ProtectionZoneArea',
        caption: 'Mühafizə sahəsi (ha)',
        width: 80,
        visible: true,
    }, {
        dataField: 'ServiceArea',
        caption: 'Xidmət etdiyi sahə',
        width: 80,
        visible: true,
    }, {
        dataField: 'MaxWaterReleaseCapacity',
        caption: 'Maksimal suburaxma qabiliyyəti (m³/s)',
        width: 80,
        visible: true,
    }, {
        dataField: 'DamLength',
        caption: 'Dambanın uzunluğu (km)',
        width: 80,
        visible: true,
    },
    {
        dataField: 'ServiceAdminUnit',
        caption: 'Xidmət etdiyi suvarma regionu',
        visible: true,
        width: 120,
        lookup: {
            dataSource: stores.adminUnit,
            displayExpr: "Name",
            valueExpr: "Id"
        }
    }, {
        dataField: 'FkIdFedObjects',
        caption: 'Qidalandırdığı kanallar',
        visible: true,
        width: 120,
        //lookup: {
        //    dataSource: stores.protectionType,
        //    displayExpr: "Name",
        //    valueExpr: "Id"
        //}
    },
    commonColumns.exploitationDate,
    commonColumns.controlled
];

var riverColumns = [
    commonColumns.objectId,
    {
        dataField: 'IdRiver',
        caption: '#',
        visible: false
    },
    commonColumns.name,
    {
        dataField: "Lenght",
        caption: "Uzunluğu",
        width: 80
    }
];

var lakeColumns = [
    commonColumns.objectId,
    {
        dataField: 'IdLake',
        caption: '#',
        visible: false
    },
    commonColumns.name,
    commonColumns.adminUnit,
    {
        dataField: "Volume",
        caption: "Həcmi",
        width: 80
    }
];

var exploitationRoadGridColumns = [
    commonColumns.objectId,
    {
        dataField: "IdExploitationRoads",
        caption: '#',
        visible: false
    },
    commonColumns.adminUnit,
    commonColumns.activityStatus,
    commonColumns.ownershipType,
    commonColumns.owner,
    {
        dataField: "Length",
        caption: "",
        visible: true
    },
    {
        dataField: "LengthA",
        caption: "",
        visible: true
    },
    {
        dataField: "LengthB",
        caption: "",
        visible: true
    },
    {
        dataField: "RoadWidth",
        caption: "",
        visible: true
    },
    commonColumns.exploitationDate,
    commonColumns.controlled
];