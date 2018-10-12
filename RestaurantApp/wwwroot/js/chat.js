"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pushhub").build();

connection.on("ReceiveMessage", function (message) {
    alert('here');
    if (message == "EnablePayButton") {
        document.getElementById("payButton").style.visibility = "visible";
        return;
    }
    document.getElementById("orderStatus").innerHTML = "Order status " + message;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});