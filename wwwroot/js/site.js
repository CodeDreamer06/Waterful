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

function tryAndIgnore(callback) {
    try { callback(); }
    catch {}
}

$(document).ready(() => {
    tryAndIgnore(() => {
        hideElements([...allCounters, ...allUnCounters]);
        showElementsGreaterThanOne();
    });

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

    tryAndIgnore(() => {
        document.getElementById("deleteSelected").style.display = "none";
        $('.form-check-input').change(() => {
            if ($('.form-check-input:checked').length > 0)
                document.getElementById("deleteSelected").style.display = "inline-block";
        });
    });

    tryAndIgnore(() => {
        $("#editCounter").click(() => {
            var newCount = parseInt($("#editCounterValueDisplay").text()) + 1;
            $("#editCounterValueDisplay").text(newCount);
            $("#editCounterValue").val(newCount);
        });

        $("#editUnCounter").click(() => {
            var counterValue = parseInt($("#editCounterValueDisplay").text());
            if (counterValue > 1) {
                $("#editCounterValueDisplay").text(counterValue - 1);
                $("#editCounterValue").val(counterValue - 1);
            }
        });
    });
});
