const ARCGIS_SERVICES = {
    NASOS_STANSIYALARI: 0,
    ACHIQ_SUVARMA_SHEBEKESI: 1,
    QAPALI_SUVARMA_SHEBEKESI: 2,
    KOLLEKTOR_DRENAJ_SHEBEKESI: 3,
    HIDROTEXNIKI_QURGULAR: 4,
    ARTEZIAN_VE_SUBARTEZIAN_QUYULARI: 5,
    HIDROQOVSHAQ: 6,
    ISTISMAR_YOLLARI: 7,
    MUHAFIZE_BENDLERI: 8,
    QISH_OTLAQLARIN_SU_TEMINATI_SISTEMLERI: 9,
    CHAYLAR: 10,
    SU_TUTARLAR: 11,
    BINALAR_VE_TIKILILER: 12,
    GOLLER: 13,
    INZIBATI_ERAZI_VAHIDLERI: 14,
    OBYEKTLERIN_ORTUK_NOVLERI_NOQTELERI: 15,
    ACHIQ_SUVARMA_SHEBEKESI_ORTUK: 16
};


const ARCGIS_SERVICE_ENTITY_SERVICES = [
    { id: 0, entity: "Obj08PumpStation", OrderNumber : 1 },
    { id: 1, entity: "Obj01Channel", OrderNumber : 1},
    { id: 2, entity: "Obj02ClosedChannel", OrderNumber : 1 },
    { id: 3, entity: "Obj03Collector", OrderNumber :1},
    { id: 4, entity: "Obj06HydrotechnicalInstallation", OrderNumber : 0},
    { id: 5, entity: "Obj07ArtesianSubartesianWell", OrderNumber : 0 },
    { id: 6, entity: "Obj05HydroJunction", OrderNumber : 0},
    { id: 7, entity: "Obj12ExploitationRoad", OrderNumber : 0 },
    { id: 8, entity: "Obj10ProtectionBarrier", OrderNumber : 0},
    { id: 9, entity: "Obj09WinterWaterSupplySystem", OrderNumber : 0 },
    //{ id: 10, entity: "ObjRiver" },
    { id: 11, entity: "Obj04ReservoirSystem", OrderNumber : 0 },
    //{ id: 12, entity: "Obj11AuxiliaryBuildingsConstruction" },
    { id: 12, entity: "Obj11BuildingsConstruction", OrderNumber : 0 },
    //{ id: 13, entity: "ObjLake" },
    { id: 14, entity: "ObjAdminUnit", OrderNumber : 0}
];