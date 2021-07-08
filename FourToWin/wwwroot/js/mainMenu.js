var MainMenu = document.getElementById("MainMenu")
var OnlineMenu = document.getElementById("OnlineMenu")
var Menu = document.getElementById("Menu")

var onlineDiv = document.getElementById("MenuOnline")
onlineDiv.addEventListener("click", function () {
    MainMenu.style.display = "none"
    OnlineMenu.style.display = "block"
    if (Menu.classList.contains("MenuBlue")) {
        Menu.classList.remove("MenuBlue")
        Menu.classList.add("MenuGreen")
    }
})

var backToMenu = document.getElementById("BackToMenu")
backToMenu.addEventListener("click", function () {
    OnlineMenu.style.display = "none"
    MainMenu.style.display = "block"
    if (Menu.classList.contains("MenuGreen")) {
        Menu.classList.remove("MenuGreen")
        Menu.classList.add("MenuBlue")
    }
})