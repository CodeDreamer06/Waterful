let spinner = document.querySelector('.spinner');
let spinnerBackground = document.querySelector('.spinner-background');
window.addEventListener('load', () => {
    spinner.parentElement.removeChild(spinner);
    spinnerBackground.parentElement.removeChild(spinnerBackground);
});

$(() => $('[data-toggle="tooltip"]').tooltip());

$('.counter').each((_, self) => {
    var text = $(self).text();
    text = text.replace(text.includes("ml") ? "ml" : "L", "");
    text = parseFloat(text);
    console.log(text);

    jQuery({
        Counter: 0
    }).animate({ Counter: text }, {
        duration: 2000,
        easing: 'swing',
        step: () => $(self).text(Math.ceil(this.Counter))
    });
});