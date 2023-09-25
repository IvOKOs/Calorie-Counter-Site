'strict'

const multiStepForm = document.querySelector("[data-multi-step]");
const allFormSteps = [...multiStepForm.querySelectorAll("[data-step]")];

let currStep = allFormSteps.findIndex(step => {
    return step.classList.contains("active");
});

// if currStep = 0 then no form is active thus make step 1 active
if (currStep < 0) {
    currStep = 0;
    showCurrentForm();
}


// to transfer between the different forms (steps) 
multiStepForm.addEventListener("click", function (e) {
    let modifier;
    let goNext = true;
    // get all input fields from current form 
    const inputs = [...allFormSteps[currStep].querySelectorAll("input")];

    inputs.forEach(input => {

        // if(inputs.classList.contains("first-name-error")){
        //     if(!validateFirstNameText(input)){ 
        //         goNext = false; 
        //     }
        // }
        if (input.type === "text") {
            if (!validateText(input)) {
                goNext = false;
            }
        }

        if (input.type === "email") {
            //validate email
            if (!validateEmail(input)) {
                goNext = false;
                console.log(input, "email");
            }
        }
    });
    // const inputsValid = inputs.every(input => input.reportValidity());  // check if the inputs of current form are valid 
    // // console.log(inputsValid); 

    // if(inputsValid){
    //     currStep += modifier; 
    //    showCurrentForm(); 
    // }

    if (goNext) { // only go to the next form if all inputs are valid 
        if (e.target.matches("[data-next]")) { // it means I clicked on one of the "next" buttons
            modifier = 1;
        } else if (e.target.matches("[data-previous]")) {
            modifier = -1;
        } else {
            modifier = 0;
        }

        if (modifier == null) { // if we did not click on "next" or "previous" buttons
            return;
        }
        currStep += modifier; // only change the current form if all inputs are valid 
        showCurrentForm();
    }
});



// take all forms and for each - transition and animation
allFormSteps.forEach(step => {
    step.addEventListener("animationend", e => {
        allFormSteps[currStep].classList.remove("hide"); // remove "hide" class as soon as it starts with the animation
        e.target.classList.toggle("hide", !e.target.classList.contains("active")) // if it does not have the "active" class then add the "hide" class at the end of the transition for animation 
    })
});



// to hide and show the currStep (current form) 
function showCurrentForm() {
    allFormSteps.forEach((step, index) => {
        // remove the "active" class from the previous form and add it to the current form (currStep)
        step.classList.toggle("active", index === currStep);
    })
};


function validateEmail(input) {
    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (input.value.match(validRegex)) {
        document.querySelector(".email-error").style.display = "none";
        return true;
    } else {
        //   document.form1.text1.focus();
        document.querySelector(".email-error").innerHTML = "The email is not correct";
        document.querySelector(".email-error").style.display = "block";
        return false;
    }
};

function validateText(input) {
    if (input.value !== "" && input.value.length >= 2) {
        document.querySelector(".name-error").style.display = "none";
        return true;
    } else {
        document.querySelector(".name-error").innerHTML = "Please enter a valid name that is more than 3 characters";
        document.querySelector(".name-error").style.display = "block";
        return false;
    }
};



