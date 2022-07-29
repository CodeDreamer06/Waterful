let spinner = document.querySelector('.spinner');
let spinnerBackground = document.querySelector('.spinner-background');

window.addEventListener('load', () => {
    spinner.parentElement.removeChild(spinner);
    spinnerBackground.parentElement.removeChild(spinnerBackground);
    $(() => $('[data-toggle="tooltip"]').tooltip());
});

let miniGlassQuantity = 0, glassQuantity = 0, bottleQuantity = 0;

$(document).ready(() => {
    document.getElementById("drinkOptionCounter--mini-glass").style.display = "none";
    document.getElementById("drinkOptionCounter--glass").style.display = "none";
    document.getElementById("drinkOptionCounter--bottle").style.display = "none";
    $("#drinkOption--mini-glass").click(() => {
        miniGlassQuantity++;
        document.getElementById("drinkOptionCounter--mini-glass").style.display = "inline-block";
        $("#drinkOptionCounter--mini-glass").text(miniGlassQuantity);
    });

    $("#drinkOption--glass").click(() => {
        glassQuantity++;
        document.getElementById("drinkOptionCounter--glass").style.display = "inline-block";
        $("#drinkOptionCounter--glass").text(glassQuantity);
    });

    $("#drinkOption--bottle").click(() => {
        bottleQuantity++;
        document.getElementById("drinkOptionCounter--bottle").style.display = "inline-block";
        $("#drinkOptionCounter--bottle").text(bottleQuantity);
    });
});