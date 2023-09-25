'use strict';

const goalContainer = document.querySelector('.goal-container');
const overlay = document.querySelector('.overlay');
const goalCancelBtn = document.querySelector('.cancel-btn');
const changeLink = document.querySelector('.change-link');
const goalOptions = document.querySelectorAll('.goal-option');

const loseWeightRadio = document.getElementById('runner');
const maintainWeightRadio = document.getElementById('meditation');
const gainWeightRadio = document.getElementById('muscle');


function openGoalsContainer() {
    goalContainer.classList.remove("hidden");
    overlay.classList.remove("hidden");
}

function closeGoalsContainer() {
    goalContainer.classList.add("hidden");
    overlay.classList.add("hidden");
}



changeLink.addEventListener("click", function (event) {
    event.preventDefault();
    openGoalsContainer();
});

goalCancelBtn.addEventListener("click", closeGoalsContainer);

overlay.addEventListener("click", closeGoalsContainer);



goalOptions.forEach(function (option) {
    const radio = option.querySelector('input[type="radio"]');

    option.addEventListener('click', function () {
        const styledOption = document.querySelector('.goal-style');
        const styledOptionInput = styledOption.querySelector('input[type="radio"]');
        styledOptionInput.value = "";
        styledOption.classList.remove('goal-style');
        radio.checked = true;
        option.classList.add('goal-style');

        // if value == lose weight => add the value to the value of the lose weight input 
        if (loseWeightRadio.checked) {
            loseWeightRadio.value = "1";
        }
        else if (maintainWeightRadio.checked) {
            maintainWeightRadio.value = "2";
        }
        else if (gainWeightRadio.checked) {
            gainWeightRadio.value = "3";
        }
    });
}); 

function showDropdown() {
    const dropdownMenu = document.getElementById("dropdownMenu");
    dropdownMenu.style.display = "block";
}

function hideDropdown() {
    const dropdownMenu = document.getElementById("dropdownMenu");
    dropdownMenu.style.display = "none";
}


