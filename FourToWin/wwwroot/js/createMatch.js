
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub").build();

var lobby;
var player;

// Create connection
connection.start().then(function () {

    console.log("SignalR Connected successfully");

    connection.invoke("CreateGame").catch(err => console.error(err.toString()));

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("GetPlayerNum", function (playerNum) {
    player = playerNum;
    console.log("player: " + player);
    document.getElementById("player").textContent = player;
});

connection.on("GetLobbyId", function (lobbyId) {
    lobby = lobbyId;
    console.log("lobbyId: " + lobby);
    document.getElementById("lobbyid").textContent = lobby;
    document.getElementById("status").textContent = "Waiting...";

});

connection.on("Ready", function () {
    document.getElementById("status").textContent = "Ready";
    // llamar al juego
});

connection.on("ReceiveMessage", function (userName, message) {
    // mensajes chat
});


