var player = "p1";

var playerSpan = document.getElementById("CurPlayer")
playerSpan.innerHTML = "&nbsp1"
playerSpan.style.color = "red"

var round = 0
var roundDiv = document.getElementById("Round")
roundDiv.innerHTML = 0

var allSquares = document.querySelectorAll(".sq")
var columns = document.querySelectorAll(".col")

columns.forEach(c => {
    //var numColumn
    var squares = c.querySelectorAll(".sq")
    c.addEventListener("click", function () { play(squares /*numColumn*/) })
})

columns[0].addEventListener("mouseenter", function () { IluminateColumn(0) })
columns[1].addEventListener("mouseenter", function () { IluminateColumn(1) })
columns[2].addEventListener("mouseenter", function () { IluminateColumn(2) })
columns[3].addEventListener("mouseenter", function () { IluminateColumn(3) })
columns[4].addEventListener("mouseenter", function () { IluminateColumn(4) })
columns[5].addEventListener("mouseenter", function () { IluminateColumn(5) })
columns[6].addEventListener("mouseenter", function () { IluminateColumn(6) })

columns[0].addEventListener("mouseleave", function () { RestoreColumn(0) })
columns[1].addEventListener("mouseleave", function () { RestoreColumn(1) })
columns[2].addEventListener("mouseleave", function () { RestoreColumn(2) })
columns[3].addEventListener("mouseleave", function () { RestoreColumn(3) })
columns[4].addEventListener("mouseleave", function () { RestoreColumn(4) })
columns[5].addEventListener("mouseleave", function () { RestoreColumn(5) })
columns[6].addEventListener("mouseleave", function () { RestoreColumn(6) })




var time = 20
var countdownDiv = document.getElementById('Countdown')
countdownDiv.innerHTML = "GO!"

var winner

countdown()

function play(squares/*numColumn*/) {
    let ocupadas = 0;

    //var squares = document.getElementById("column" + numColumn).querySelectorAll("sq")

    squares.forEach(square => {
        if (square.classList.contains("iluminate" + player)) {
            square.classList.remove("iluminate" + player)
        }

        if (square.classList.contains("played")) { ocupadas += 1; }
    });

    if (ocupadas < 6) {
        selectedRow = 5 - ocupadas
        selectedSquare = squares[selectedRow]

        selectedSquare.classList.add("played")
        selectedSquare.classList.add(player)

        checkVictory()

        changeTurn()
    }
}

function IluminateColumn(numColumn) {
    let ocupadas = 0;
    var squares = document.querySelectorAll(".col" + numColumn)
    
    squares.forEach(square => {
        if (square.classList.contains("played")) { ocupadas += 1; }
        else { square.classList.add("iluminate"+player) }
    });

    if (ocupadas < 6) {
        squares[5 - ocupadas].classList.add("provisional"+player) 
    }
}

function RestoreColumn(numColumn) {
    let ocupadas = 0;
    var squares = document.querySelectorAll(".col" + numColumn)

    squares.forEach(square => {
        if (square.classList.contains("iluminate" + player)) {
            square.classList.remove("iluminate"+player)
        }
        if (square.classList.contains("provisional"+player)) {
            square.classList.remove("provisional"+player)
        }
    });
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
    
    for (i = 0; i < columns.length; i++) {
        RestoreColumn(i);
    }
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
            } else if (time < 5){
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
    else if(winner == "none") {
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



/* var leftCol
var rightCol

var abajo
var izquierda
var horizontal
var izqAbajo
var izqArriba
var deAbajo
var deArriba

var SqAbajo
var SqIzquierda
var SqDerecha
var SqIzqAbajo
var SqIzqArriba
var SqDeAbajo
var SqDeArriba

function comprobarVictoria(selectedSquare, cuadrosCol, selectedRow) {

    MapSquares(cuadrosCol, selectedRow)
    comprobarAbajo(cuadrosCol)

    if (SqDerecha != null && SqDerecha.classList.contains(playing) && horizontal < 4) {
        MapSquares(cuadrosCol, selectedRow)
        comprobarDerecha(selectedRow)

        MapSquares(cuadrosCol, selectedRow)
        comprobarIzquierda(selectedRow)
    }

     comprobarDerechaAbajo()

    if (SqDeArriba.classList.contains(playing)) {
        deArriba += 1
        comprobarDerechaArriba()
    }
    if (SqIzquierda.classList.contains(playing)) {
        izquierda += 1
        ComprobarIzquierda()
    }
    if (SqIzqAbajo.classList.contains(playing)) {
        izqAbajo += 1
        ComprobarIzquierdaAbajo()
    }
    if (SqIzqArriba.classList.contains(playing)) {
        izqArriba += 1
        ComprobarIzquierdaArriba()
    }

}

function comprobarAbajo(cuadrosCol) {
    if (SqAbajo != null && SqAbajo.classList.contains(playing) && cuadrosCol[selectedRow + 2].classList.contains(playing)
        && cuadrosCol[selectedRow + 3].classList.contains(playing)) {
        victory();
    }
}

function comprobarDerecha(cuadrosCol, selectedRow) {
    let columna = cuadrosCol
    let numFila = selectedRow

    horizontal += 1
    if (horizontal += 3) {
        victory()
    }
    columna = rightCol
    MapSquares(columna, selectedRow)

    if (SqDerecha != null && SqDerecha.classList.contains(playing)) {
        horizontal += 1
        if (horizontal += 3) {
            victory()
        }
        columna = rightCol
        MapSquares(columna, selectedRow)
    }

    if (SqDerecha != null && SqDerecha.classList.contains(playing)) {
        horizontal += 1
        if (horizontal += 3) {
            victory()
        }
        columna = rightCol
        MapSquares(columna, selectedRow)
    }
}

function comprobarIzquierda(cuadrosCol, selectedRow) {
    let columna = cuadrosCol
    let numFila = selectedRow

    horizontal += 1
    if (horizontal += 3) {
        victory()
    }
    MapSquares(leftCol, selectedRow)

    if (SqIzquierda != null && SqIzquierda.classList.contains(playing)) {
        horizontal += 1
        if (horizontal += 3) {
            victory()
        }
        MapSquares(leftCol, selectedRow)
    }

    if (SqIzquierda != null && SqIzquierda.classList.contains(playing)) {
        horizontal += 1
        if (horizontal += 3) {
            victory()
        }
        MapSquares(leftCol, selectedRow)
    }
}
 */
/* function comprobarDerechaAbajo(numFila) {
    if (SqDeAbajo.classList.contains(playing)) {
        deAbajo += 1
        while (SqDeAbajo != null && deAbajo < 5) {
            columna = rightCol
            numeroFila = numFila + 1
            MapSquares(columna, numeroFila)
            if (SqDeAbajo.classList.contains(playing)) {
                deAbajo += 1
            }
            if (deAbajo == 4) {
                victory()
            }
        }
    }
} */




/* function MapSquares(cuadrosCol, selectedRow) {
    identifyCols(cuadrosCol)

    SqAbajo = cuadrosCol[selectedRow + 1]
    SqIzquierda = leftCol[selectedRow]
    SqDerecha = rightCol[selectedRow]
    SqIzqAbajo = leftCol[selectedRow + 1]
    SqIzqArriba = leftCol[selectedRow - 1]
    SqDeAbajo = rightCol[selectedRow + 1]
    SqDeArriba = rightCol[selectedRow - 1]
}

function identifyCols(cuadrosCol) {
    switch (cuadrosCol) {
        case cuadrosCol1:
            leftCol = null
            rightCol = cuadrosCol2
        case cuadrosCol2:
            leftCol = cuadrosCol1
            rightCol = cuadrosCol3
        case cuadrosCol3:
            leftCol = cuadrosCol2
            rightCol = cuadrosCol4
        case cuadrosCol4:
            leftCol = cuadrosCol3
            rightCol = cuadrosCol5
        case cuadrosCol5:
            leftCol = cuadrosCol4
            rightCol = cuadrosCol6
        case cuadrosCol6:
            leftCol = cuadrosCol5
            rightCol = cuadrosCol7
        case cuadrosCol7:
            leftCol = cuadrosCol6
            rightCol = null
    }
} */
