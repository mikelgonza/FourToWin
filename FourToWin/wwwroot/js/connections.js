
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub").build();

var lobby;
var player;

connection.start().then(function () {
    // Este bloque se ejecuta cuando se establece la conexion con el servidor
    console.log("SignalR Connected successfully");
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("GetPlayerNum", function (playerNum) {
    player = playerNum;
});

connection.on("GetRandomLobbyId", function (lobbyId) {
    lobby = lobbyId;
});

connection.on("Ready", function () {
    // llamar al juego
});

connection.on("ReceiveMessage", function (userName, message) {
    // mensajes chat
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    // mandar mensaje al chat
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageToGroup", message, lobby).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});
