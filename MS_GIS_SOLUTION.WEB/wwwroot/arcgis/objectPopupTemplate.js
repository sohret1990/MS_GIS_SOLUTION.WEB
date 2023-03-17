const popupTemplate = {
    aciqSuvarmaTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td> {Name} </td></tr>
                        <tr><td> Tipi </td><td>{Type}</td></tr>
                        <tr><td> Yerləşdyi ərazi </td><td>{AdminUnit}</td></tr>
                        <tr><td> Mülkiyyətçisi </td><td>{Owner}</td></tr>
                        <tr><td> Su mənbəyi </td><td>{Source}</td></tr>
                        <tr><td> Xidmət etdiyi sahə(ha) </td><td style="vertical-align:bottom;">{ServiceArea}</td></tr>
                        <tr><td> Suburaxma Qabiliyyəti(m³/s)</td><td>{Capacity}</td></tr>
                        <tr><td> Coğrafi uzunluq(km)</td><td>{Length}</td></tr>
                        <tr><td> Ümumi coğrafi uzunluq(km)</td><td>{AllLength}</td></tr>
                        <tr><td> Layihə üzrə uzunluq(km)</td><td>{ActualLength}</td></tr>
                        <tr><td> Layihə üzrə ümumi uzunluq(km)</td><td>{AllActualLength}</td></tr>
                        <tr><td> İstismara verilmə tarixi(il)</td><td>{ExploitationDate} </td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/Channel/CreateEditChannelAdminUnit/?id={key}&mode=true" class="btn-xs btn-outline-primary">Ətraflı</a>
                        </td></tr>
                        <tr><td colspan="2"> </td></tr>
                        </table>`,
    qapaliSuvarmaTemplate: `<table id="tblInfo-{key}"  class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Tipi </td><td>{Type}</td></tr>
                        <tr><td> Yerləşdyi ərazi </td><td>{AdminUnit}</td></tr>
                        <tr><td> Su mənbəyi </td><td>{Source}</td></tr>
                        <tr><td> Xidmət etdiyi sahə(ha)</td><td style="vertical-align:bottom;">{ServiceArea}</td></tr>
                        <tr><td> Suburaxma Qabiliyyəti(m³/s)</td><td>{Capacity}</td></tr>
                        <tr><td> Coğrafi uzunluq(km)</td><td>{Length}</td></tr>
                        <tr><td> Ümumi coğrafi uzunluq(km)</td><td>{AllLength}</td></tr>
                        <tr><td> Layihə üzrə uzunluq(km)</td><td>{ActualLength}</td></tr>
                        <tr><td> Layihə üzrə ümumi uzunluq(km)</td><td>{AllActualLength}</td></tr>
                        <tr><td> İstismara verilmə tarixi(il)</td><td>{ExploitationDate} </td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/ClosedChannel/CreateEditClosedChannelAdminUnit/?id={key}&mode=true" class="btn-xs btn-outline-primary">Ətraflı</a>
                        </td></tr>
                        <tr><td colspan="2"> </td></tr>
                        </table>`,
    nasosStansiyasiTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Tipi </td><td>{Type}</td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> İstismara verilmə tarixi(il)</td><td>{ExploitationDate} </td></tr>
                        <tr><td> Aqreqatların sayı(ədəd)</td><td>{AggregatesCount}</td></tr>
                        <tr><td> Ümumi gücü(kvt/saat)</td><td>{Power}</td></tr>
                        <tr><td> Kənd/Qəsəbə </td><td>{AdditionalAdminUnit}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/pumpstation/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    collectorTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Tipi </td><td>{Type}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> İstismara verilmə tarixi(il)</td><td>{ExploitationDate} </td></tr>
                        <tr><td> Su sərfi(m3/san) </td><td>{Capacity}</td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> Drenaj Sularının Qəbuledicisi</td><td>{Source}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/Collector/CreateEditCollectorAdminUnit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    hydroTechnicalTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Qurğu tipləri </td><td>{Type}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Su mənbəyi </td><td> {Source}</td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/HydrotechnicalInstallation/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    hydroJunctionTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> İstismara verilmə tarixi(il)</td><td>{ExploitationDate} </td></tr>
                        <tr><td> Su mənbəyi </td><td> {Source}</td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/HydroJunction/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    exploitationRoadsTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Yolun uzunluğu (km) </td><td>{Length} </td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/ExploitationRoads/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    protectionBarriersTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Bəndin növü </td><td>{Type} </td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/ProtectionBarrier/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    winterWaterSupplyTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> Xidmət etdiyi sahə(ha) </td><td>{ServiceArea} </td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/WinterWaterSupply/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    reservoirSystemTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Yerləşməsinə görə tipi</td><td>{Type}</td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> Əsas təyinatı</td><td>{Appointment}</td></tr>
                        <tr><td> Su mənbəyi </td><td>{Source}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/ReservoirSystem/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    buildingsConstructionsTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Ad </td><td>{Name}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> Təyinatı</td><td>{Appointment}</td></tr>
                        <tr><td> Ümumi sahəsi(kvm)</td><td>{Area}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/BuildingAndConstructions/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    artesianSubartesianTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Reper nömrəsi </td><td>{No}</td></tr>
                        <tr><td> Yerləşdiyi ərazi </td><td style="vertical-align:bottom;">{AdminUnit} </td></tr>
                        <tr><td> Mülkiyyətçisi</td><td>{Owner}</td></tr>
                        <tr><td> Artezian növü</td><td>{Type}</td></tr>
                        <tr><td> Məhsuldarlığı(l/san)</td><td>{Productivity}</td></tr>
                        <tr><td style="border:0;" colspan="2" class="text-right">
                            <a target="_blank" href="/ArtesianSubartesianWell/CreateEdit/?id={key}&mode=true" class="btn-xs btn-outline-primary" value="Ətraflı">Ətraflı</a>
                        </td></tr>
                        </table>`,
    riverTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Adı</td><td>{Name}</td></tr>
                        <tr><td> Uzunuluğu(km)</td><td>{Lenght}</td></tr>
                        </table>`,
    lakeTemplate: `<table id="tblInfo-{key}" class="table table-bordered table-hover">
                        <tr><td> Adı</td><td>{Name}</td></tr>
                        <tr><td> Həcmi(kub.m)</td><td>{Volume}</td></tr>
                        </table>`
}
