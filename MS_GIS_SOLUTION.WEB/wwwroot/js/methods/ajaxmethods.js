
function callAjax(url, data, type = 'GET') {
    return JSON.parse($.ajax({
        type: type,
        url: url,
        cache: false,
        async: false,
        data: data
    }).responseText);
}

function checkBeforeSubmit() {
    var svbtn = $('#Save').dxButton('instance');
    svbtn.option('disabled', true);
}   

var showAlert = function (msg, title, callbackFunction) {
    var result = DevExpress.ui.dialog.alert(msg, title, true);
    result.done(function () {
        if (callbackFunction)
            callbackFunction();
    });
};

function setDefaultValueSelectBox(e, index) {
    var items = e.component.option("items");
    if (items.length === 1) {
        e.component.option("value", items[index]);
    }
}

function notify(type, message, displayTime = 500, position = 'RightTop') {


    let Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        onOpen: function (toast) {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: type,
        title: message
    })



    //DevExpress.ui.notify({

    //    message: message,
    //    type: type,
    //    displayTime: 1000,
    //    closeOnClick: true,
    //    position: { my: 'right', at: 'top right', of: window },
    //    width: function () { return $(window).width() * 0.4 },
    //    animation: {
    //        show: {
    //            type: "slide",
    //            from: {
    //                left: $(window).width() ,
    //                marginTop: 35 , 
    //                opacity: 0.2
    //            },
    //            to: {
    //                opacity: 1
    //            },
    //            duration: 1500
    //        },
    //        hide: {
    //            type: "slide",
    //            to: {
    //                opacity: 0,
    //                left: -1 * $(window).width()
    //            },
    //            duration: 1000
    //        }
    //    }
    //});



}


function getCurrencySymbol(code) {
    return new Promise((resolve, reject) => {
        $.get('/Erp/Currency/GetCurrencySymbol', { code: code }).done(function (symbol) {
            resolve(symbol);
        }).fail((err) => {
            reject(err);
        });
    })
}