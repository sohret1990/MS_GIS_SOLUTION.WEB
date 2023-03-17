var currentKey = -1;
var ProductId = null;

/*==========+++++++++++++++++++++++++++++++++++++=============*/
//Səhifədə dəyişiklik edildikdən sonra yadda saxlamadan başqa səhifəyə keçmək istədikdə bildiriş verir!
var showFormSavingConfirmationOnUnload = false;
function enableConfirmBeforeUnloadConfirmation() {
    window.addEventListener('beforeunload', (event) => {
        if (showFormSavingConfirmationOnUnload) {
            // Cancel the event as stated by the standard.
            event.preventDefault();
            // Chrome requires returnValue to be set.
            event.returnValue = 'Dəyişikliklər yadda saxlanılmayıb. \nDavam etmək istəyirsiniz?';
        }
    });
}
/*==========+++++++++++++++++++++++++++++++++++++=============*/


function onEditingStart(e) {

    currentKey = e.key;
}
function setPopupTitle(e) {
    if (currentKey > -1) {
        e.component.option("title", "Məlumatı redaktə et");
        currentKey = -1;
    }
    else {
        e.component.option("title", "Yeni məlumat");
    }
}

function setPlantingProductId(rowData, value) {

    rowData.ProductId = value;
    rowData.ProductTypeId = null;
}

//function setPlantingPlanProductId(rowData, value) {

//    rowData.ProductId = value;
//    rowData.ProductTypeId = null;
//    rowData.ProductReproductionId = null;
//    ProductId = value;
//}

function setPlantingProductIdRep(rowData, value) {

    rowData.ProductId = value;
    rowData.ReproductionId = null;
}


function getPlantingProductTypes(options) {
    return {
        store: DevExpress.data.AspNet.createStore({
            key: "ID",
            loadUrl: '/CommonEntity/GetProductTypes'
        }),
        filter: options.data ? ["ProductId", "=", options.data.ProductId] : null
    };
}

function getPlantingReproductionTypes(options) {
    return {
        store: DevExpress.data.AspNet.createStore({
            key: "ID",
            loadUrl: '/CommonEntity/GetReproductionTypes'
        }),
        filter: options.data ? ["ProductId", "=", options.data.ProductId] : null
    };
}
