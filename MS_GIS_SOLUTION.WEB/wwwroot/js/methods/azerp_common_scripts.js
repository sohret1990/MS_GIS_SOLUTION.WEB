function ShowPushNotification(settings) {
    if (Notification.permission !== 'granted')
        Notification.requestPermission();
    else {
        return new Notification(settings.title, {
            icon: settings.icon,
            body: settings.message,
            //onclick: settings.onclick,
            onclose: settings.onclose,
            ondenied: settings.ondenied,
        }).onclick = function (event) {
            window[settings.onclick](event, settings.link);
        }

    }
}




function onDataGridToolbarPreparing(e, text, callbackUrl) {

    let dataGrid = e.component;
    let data = e.toolbarOptions.items.filter(f => f.name !== 'addRowButton');
    if (e.toolbarOptions.items.filter(f => f.name === 'addRowButton').length === 0) return;

    e.toolbarOptions.items = data;

    e.toolbarOptions.items.unshift(
        {
            location: "after",
            widget: "dxButton",
            options: {
                elementAttr: {
                    class: 'btn-xs btn-outline-primary text-white bg-primary',
                },
                onClick: function () {
                    if (callbackUrl === null) {
                        e.component.addRow();
                    } else {
                        window.location.href = callbackUrl;
                    }
                },
                text: text,
                type: 'normal'
            }
        },
        {
            location: "after"
        }
    );

}