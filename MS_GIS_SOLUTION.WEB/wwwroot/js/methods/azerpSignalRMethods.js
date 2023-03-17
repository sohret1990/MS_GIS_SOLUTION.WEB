var connection = new signalR.HubConnectionBuilder().withUrl("/signalrhub").build();

if (!connection.started)
    connection.start();

connection.on("ReceiveMessage", ReceiveMessage);

connection.on("SendMessage", SendMessage);

//connection.on("ReceiveMessage", function (user, d) {
//    var options = JSON.parse(d);
//    ShowPushNotification(options);
//});


//function showMessage(user, options) {
//    if (!connection.started) {
//        connection.start().then(function () {
//            connection.invoke("SendMessage", user, options).catch(function (err) {

//            });
//        });
//    } else {
//        connection.invoke("SendMessage", user, options).catch(function (err) {

//        });
//    }
//}

function ReceiveMessage(usr, message) {
    //ShowPushNotification({ title: 'Bildiriş məktubu', message: message, icon: 'https://' + window.location.host + '/Content/assets/images/agro.png' });
}

function SendMessage(usr) {
    connection.invoke("SendMessage", 'user', 'message').catch(function (err) {

    });
}

//function onGenerateReportNotificationClick(event, link) {
//    window.open(link, '_blank');
//}


//function showMessage(user, options) {
//    if (!connection.started) {
//        connection.start().then(function () {
//            connection.invoke("SendMessage", user, options).catch(function (err) {

//            });
//        });
//    } else {
//        connection.invoke("SendMessage", user, options).catch(function (err) {

//        });
//    }
//}