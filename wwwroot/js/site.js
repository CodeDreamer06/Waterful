let spinner = document.querySelector('.spinner');
let spinnerBackground = document.querySelector('.spinner-background');
window.addEventListener('load', () => {
    spinner.parentElement.removeChild(spinner);
    spinnerBackground.parentElement.removeChild(spinnerBackground);
    $(() => $('[data-toggle="tooltip"]').tooltip());
});