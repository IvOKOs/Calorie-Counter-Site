'use strict';

const foodInputs = document.querySelectorAll(".food-inputs input");
const cancelButton = document.getElementById('cancelBtn');

foodInputs.forEach(input => {
    input.addEventListener("input", function (event) {
        const inputValue = event.target.value;
        const inputName = event.target.name;

        if (inputName != "name") {
            const isValid = /^(\d*\.?\d*)$/.test(inputValue);

            if (!isValid) {
                event.target.value = "";
            }
        }
    })
}); 


cancelButton.addEventListener('click', function () {
    window.location.href = '/Home/Index'; 
});


function validateForm() {
    const name = document.getElementById("nameInput").value.trim();
    const warningMessage = document.querySelector(".food-inputs p");
    const nameLabel = document.querySelector(".food-labels .name-label"); 

    if (name.length < 2) {
        warningMessage.classList.remove("hidden");
        nameLabel.classList.add("name-label-space"); 
        return false; 
    }

    warningMessage.classList.add("hidden");
    nameLabel.classList.remove("name-label-space"); 
    return true; 
}; 