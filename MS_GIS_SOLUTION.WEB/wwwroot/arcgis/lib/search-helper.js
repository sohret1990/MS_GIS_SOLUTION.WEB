let searchfilter = {
    where: "1=1",
    outFields: "id, name",
    f: "pjson",
    returnGeometry: false
};

$(document).ready(function () {


    $('#layers').select2({
        placeholder: "Lay seçin...",
        allowClear: true,
        theme: "classic",
        ajax: {
            url: "/CommonEntity/GetUserLayers",
            processResults: function (data) {
                let layerObjects = [];
                data.map((item, index) => {
                    let { Id, Name } = item;
                    layerObjects.push({ id: Id, text: Name });
                });

                return {
                    results: layerObjects
                };
            },
            data: function (params) {
                var query = {
                    search: params.term,
                    type: 'public'
                }
               
                searchfilter.type = "public";
                if (params.term) {
                    searchfilter.where = "lower(name) like '%" + params.term + "%'";
                }

                return searchfilter;
            }
        },
        templateResult: function (state) {
            var baseUrlService = "/content/mapicons/red-pin.png";
            var $stateService = $(
                '<span><img src="' + baseUrlService + '" class="img-flag" style="max-width:15px; max-height:15px;" /> &nbsp;&nbsp;&nbsp;' + state.text + '</span>'
            );
            return $stateService;
        }
    });

});

