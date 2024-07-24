
function show(mealTime) {
    document.querySelector(".textBox").value = mealTime;
}

function showDropdown() {
    const dropdownMenu = document.getElementById("dropdownMenu");
    dropdownMenu.style.display = "block";
}

function hideDropdown() {
    const dropdownMenu = document.getElementById("dropdownMenu");
    dropdownMenu.style.display = "none";
}

let dropdown = document.getElementById("mealOptionsDropdown");
dropdown.onclick = function () { 
    dropdown.classList.toggle("active");
} 

const searchBarContainer = document.querySelector(".searchbar");
let searchbar = searchBarContainer.querySelector("#searchInput"); 
const foodContainer = document.querySelector(".food-container"); 

let divValue;


var quantityInput = document.querySelector('.food-quantity-input');
const foodNameContainer = document.querySelector('.food-name-container');
const h5Element = foodNameContainer.querySelector('h5');
const nameValue = h5Element.textContent;

quantityInput.addEventListener('input', calculateNutrition); 


$(document).ready(function () {
    $('#searchInput').on('input', function () {
        var searchTerm = $(this).val();
        if (searchTerm.length >= 2) {
            sendAutocompleteRequest(searchTerm);
        } else {
            clearAutocompleteResults();
        }
    });
});


function calculateNutrition() {
    var quantity = parseFloat(quantityInput.value) || 0;
    var name = nameValue.replace("Name: ", ""); 
    // Make an asynchronous request to the server to calculate nutrition values
    // Send the quantity value to the server and receive the calculated values in response

     
    fetch('/Food/CalculateNutrition', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ quantity: quantity, name: name }),
    })
        .then(response => response.json())
        .then(data => {
            // Update the nutrition elements with the received data
            var nutritionInfo = document.getElementById('nutrition-info');
            nutritionInfo.querySelector('h5:nth-of-type(1)').textContent = 'Calories: ' + data.calories.toFixed(2) + ' kcal';
            nutritionInfo.querySelector('h5:nth-of-type(2)').textContent = 'Protein: ' + data.protein.toFixed(2) + ' gr';
            nutritionInfo.querySelector('h5:nth-of-type(3)').textContent = 'Carbs: ' + data.carbs.toFixed(2) + ' gr';
            nutritionInfo.querySelector('h5:nth-of-type(4)').textContent = 'Fat: ' + data.fat.toFixed(2) + ' gr';

            var nutritionInfoHidden = document.getElementById("nutrition-info-hidden");
            nutritionInfoHidden.querySelector("input[name='calories']").value = data.calories.toFixed(2);
            nutritionInfoHidden.querySelector("input[name='protein']").value = data.protein.toFixed(2);
            nutritionInfoHidden.querySelector("input[name='carbs']").value = data.carbs.toFixed(2);
            nutritionInfoHidden.querySelector("input[name='fat']").value = data.fat.toFixed(2);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function sendAutocompleteRequest(searchTerm) {
    $.ajax({
        url: '/Food/AutoComplete',
        type: 'GET',
        data: { term: searchTerm },
        success: function (data) {
            populateAutocompleteResults(data);
        },
        error: function (error) {
            console.log('Error:', error);
        }
    });
}

function populateAutocompleteResults(data) {
    var autocompleteResultsDiv = $('#autocompleteResults');
    autocompleteResultsDiv.empty();

    $.each(data, function (index, value) {
        var suggestion = $('<div>').text(value);
        autocompleteResultsDiv.append(suggestion);
    });
 
    const autocompleteResults = document.getElementById("autocompleteResults");
    let divElements = autocompleteResults.querySelectorAll("div");

    if (divElements !== null && divElements.length > 0) {
        for (let i = 0; i < divElements.length; i++) {
            const divElement = divElements[i];
            divElement.addEventListener("click", function () {
                divValue = divElement.textContent;
                searchbar.value = divValue;
                clearAutocompleteResults();
            });
        }
    }
}

function clearAutocompleteResults() {
    var autocompleteResultsDiv = $('#autocompleteResults');
    autocompleteResultsDiv.empty();
}



