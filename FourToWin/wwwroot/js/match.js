﻿
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub").build();

var lobby;
var userPlayer;

// GAME
var player = "p1";

var playerSpan = document.getElementById("CurPlayer")
playerSpan.innerHTML = "&nbsp1"
playerSpan.style.color = "red"

var round = 0
var roundDiv = document.getElementById("Round")
roundDiv.innerHTML = 0

var allSquares = document.querySelectorAll(".sq")
var columns = document.querySelectorAll(".col")

var time = 20
var countdownDiv = document.getElementById('Countdown')
countdownDiv.innerHTML = "GO!"

var winner

function play(numColumn) {

    let ocupadas = 0;
    var squares = document.querySelectorAll(".col" + numColumn)

    squares.forEach(square => {
        if (square.classList.contains("played")) { ocupadas += 1; }
    });

    if (ocupadas < 6) {
        selectedRow = 5 - ocupadas
        selectedSquare = squares[selectedRow]

        selectedSquare.classList.add("played")
        console.log("Player: " + player)
        selectedSquare.classList.add(player)

        checkVictory()

        changeTurn()
    }
}

function checkVictory() {

    for (i = 0; i < winningPositions.length; i++) {
        var square1 = allSquares[winningPositions[i][0]]
        var square2 = allSquares[winningPositions[i][1]]
        var square3 = allSquares[winningPositions[i][2]]
        var square4 = allSquares[winningPositions[i][3]]

        if (square1.classList.contains(player) &&
            square2.classList.contains(player) &&
            square3.classList.contains(player) &&
            square4.classList.contains(player)) {
            square1.classList.add("winnerSq")
            square2.classList.add("winnerSq")
            square3.classList.add("winnerSq")
            square4.classList.add("winnerSq")
            winner = player
            setTimeout(function () { victory(winner) }, 400)
        }
    }

    var playedSquares = 0
    allSquares.forEach(sq => {
        if (sq.classList.contains("played")) {
            playedSquares += 1
        }
    })
    if (playedSquares > 41) {
        victory("none")
    }
}

function changeTurn() {
    if (player == "p1") {
        player = "p2"
        playerSpan.innerHTML = "&nbsp2"
        playerSpan.style.color = "blue"
        countdown()
    }
    else {
        player = "p1"
        playerSpan.innerHTML = "&nbsp1"
        playerSpan.style.color = "red"
        round += 1
        roundDiv.innerHTML = round
        countdown()

    }
}

var countInterval

function countdown() {
    time = 20
    if (countInterval == null) {
        changeCount()
        countInterval = setInterval(changeCount, 1000)
    }
    else {
        clearInterval(countInterval)
        changeCount()
        countInterval = setInterval(changeCount, 1000)

    }
}

function changeCount() {
    if (time >= 0) {
        if (time == 20) {
            countdownDiv.style.color = "#0EDF00"
            countdownDiv.innerHTML = "GO!"
        }
        else if (time == 0) {
            countdownDiv.innerHTML = "TIME OUT!"
        }
        else {
            countdownDiv.innerHTML = time;
            if (time >= 5 && time < 10) {
                countdownDiv.style.color = "darkorange"
            } else if (time < 5) {
                countdownDiv.style.color = "red"
            }
        }
        time -= 1
    } else {
        clearInterval(countInterval)
        changeTurn()
    }
}

function victory(winner) {
    document.getElementById("Scene").style.filter = "blur(5px)";
    document.getElementById("VictoryPopUp").style.display = "block";
    if (winner == "p1") {
        document.getElementById("WinnerMsg").style.color = "red"
        document.getElementById("VictoryPopUp").style.borderColor = "red"
        document.getElementById("WinnerMsg").innerHTML = "Player 1 WINS!"
    } else if (winner == "p2") {
        document.getElementById("WinnerMsg").style.color = "blue"
        document.getElementById("VictoryPopUp").style.borderColor = "blue"
        document.getElementById("WinnerMsg").innerHTML = "Player 2 WINS!"
    }
    else if (winner == "none") {
        document.getElementById("WinnerMsg").innerHTML = "It's a DRAW!"
    }
}

function CloseResultPopUp() {
    document.getElementById("Scene").style.filter = "none";
    document.getElementById("VictoryPopUp").style.display = "none";
}

const winningPositions = [
    [0, 1, 2, 3], [1, 2, 3, 4], [2, 3, 4, 5],
    [6, 7, 8, 9], [7, 8, 9, 10], [8, 9, 10, 11],
    [12, 13, 14, 15], [13, 14, 15, 16], [14, 15, 16, 17],
    [18, 19, 20, 21], [19, 20, 21, 22], [20, 21, 22, 23],
    [24, 25, 26, 27], [25, 26, 27, 28], [26, 27, 28, 29],
    [30, 31, 32, 33], [31, 32, 33, 34], [32, 33, 34, 35],
    [36, 37, 38, 39], [37, 38, 39, 40], [38, 39, 40, 41],

    [0, 6, 12, 18], [6, 12, 18, 24], [12, 18, 24, 30], [18, 24, 30, 36],
    [1, 7, 13, 19], [7, 13, 19, 25], [13, 19, 25, 31], [19, 25, 31, 37],
    [2, 8, 14, 20], [8, 14, 20, 26], [14, 20, 26, 32], [20, 26, 32, 38],
    [3, 9, 15, 21], [9, 15, 21, 27], [15, 21, 27, 33], [21, 27, 33, 39],
    [4, 10, 16, 22], [10, 16, 22, 28], [16, 22, 28, 34], [22, 28, 34, 40],
    [5, 11, 17, 23], [11, 17, 23, 29], [17, 23, 29, 35], [23, 29, 35, 41],

    [2, 9, 16, 23],
    [1, 8, 15, 22], [8, 15, 22, 29],
    [0, 7, 14, 21], [7, 14, 21, 28], [14, 21, 28, 35],
    [6, 13, 20, 27], [13, 20, 27, 34], [20, 27, 34, 41],
    [12, 19, 26, 33], [19, 26, 33, 40],
    [18, 25, 32, 39],

    [3, 8, 13, 18],
    [4, 9, 14, 19], [9, 14, 19, 24],
    [5, 10, 15, 20], [10, 15, 20, 25], [15, 20, 25, 30],
    [11, 16, 21, 26], [16, 21, 26, 31], [21, 26, 31, 36],
    [17, 22, 27, 32], [22, 27, 32, 37],
    [23, 28, 33, 38]
];



// Create connection
connection.start().then(function () {

    console.log("SignalR Connected successfully");

    if (lobbyAction === "create") {
        connection.invoke("CreateGame").catch(err => console.error(err.toString()));
    }

}).catch(function (err) {
    return console.error(err.toString());
});

if (lobbyAction === "join") {
    document.getElementById("joinButton").addEventListener("click", function (event) {

        lobby = document.getElementById('groupInput').value;

        connection.invoke("JoinGame", lobby).catch(err => console.error(err.toString()));

        event.preventDefault();
    });
}

connection.on("GetPlayerNum", function (playerNum) {
    userPlayer = playerNum;

    document.getElementById("userPlayer").textContent = userPlayer;
});

connection.on("GetLobbyId", function (lobbyId) {
    lobby = lobbyId;

    document.getElementById("lobbyid").textContent = lobby;
    document.getElementById("status").textContent = "Waiting...";

    connection.on("ReceiveColumn", function (column) {

        play(column);
    });

    connection.on("Ready", function () {
        document.getElementById("status").textContent = "Ready";

        // Start game

        countdown();

        columns[0].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 0, lobby).catch(err => console.error(err.toString()));
        })

        columns[1].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 1, lobby).catch(err => console.error(err.toString()));
        })

        columns[2].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 2, lobby).catch(err => console.error(err.toString()));
        })

        columns[3].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 3, lobby).catch(err => console.error(err.toString()));
        })

        columns[4].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 4, lobby).catch(err => console.error(err.toString()));
        })

        columns[5].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 5, lobby).catch(err => console.error(err.toString()));
        })

        columns[6].addEventListener("click", function () {
            if (player == userPlayer)
                connection.invoke("SendColumn", 6, lobby).catch(err => console.error(err.toString()));
        })
    });

    connection.on("ReceiveMessage", function (userName, message) {
        // mensajes chat
    });












