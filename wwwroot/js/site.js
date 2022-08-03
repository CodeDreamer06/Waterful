let spinner = document.querySelector('.spinner');
let spinnerBackground = document.querySelector('.spinner-background');

window.addEventListener('load', () => {
    spinner.parentElement.removeChild(spinner);
    spinnerBackground.parentElement.removeChild(spinnerBackground);
    $(() => $('[data-toggle="tooltip"]').tooltip());
});

let quantities = [0, 0, 0];
let allOptions = [
    "drinkOption--mini-glass",
    "drinkOption--glass",
    "drinkOption--bottle"
]
let allCounters = [
    "drinkOptionCounter--mini-glass",
    "drinkOptionCounter--glass",
    "drinkOptionCounter--bottle",
];
let allUnCounters = [
    "drinkOptionUncounter--mini-glass",
    "drinkOptionUncounter--glass",
    "drinkOptionUncounter--bottle",
];
let quantityElements = [
    "miniGlassQuantity",
    "glassQuantity",
    "bottleQuantity"
];
function hideElements(elements) {
    for (let elementId of elements)
        document.getElementById(elementId).style.display = "none";
}

function showElements(elements) {
    for (let elementId of elements)
        document.getElementById(elementId).style.display = "inline-block";
}

function showElementsGreaterThanOne() {
    for (var i = 0; i < allCounters.length; i++)
        if (document.getElementById(allCounters[i]).innerHTML > 1) {
            quantities[i] = document.getElementById(allCounters[i]).innerHTML;
            document.getElementById(allCounters[i]).style.display = "inline-block";
            document.getElementById(allUnCounters[i]).style.display = "inline-block";
        }
}

$(document).ready(() => {
    hideElements([...allCounters, ...allUnCounters]);
    showElementsGreaterThanOne();

    for (let i = 0; i < 3; i++) {
        $("#" + allOptions[i]).click(() => {
            quantities[i]++;
            console.log(quantities[i]);
            if (quantities[i] != 0)
                showElements([allCounters[i], allUnCounters[i]]);
            else
                hideElements([allCounters[i], allUnCounters[i]]);
            $("#" + allCounters[i]).text(quantities[i]);
            document.getElementById(quantityElements[i]).value = quantities[i];
        });
    
        $("#" + allUnCounters[i]).click(() => {      
            quantities[i] -= 2;
            $("#" + allCounters[i]).text(quantities[i]);
            document.getElementById(quantityElements[i]).value = quantities[i];
        });
    }
});
