

function SameAddress() {
    let postal_fieldset = document.getElementById("postal");
    var checkBox = document.getElementById("same-address");
    
    if (checkBox.checked == true) {
        postal_fieldset.style.display = "none";
    } else {
        postal_fieldset.style.display = "block";
    }
}

function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

var countries = ["Afghanistan", "Albania", "Algeria", "Andorra", "Angola",
    "Anguilla", "Antigua &amp; Barbuda", "Argentina",
    "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
    "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
    "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
    "Bosnia &amp; Herzegovina", "Botswana", "Brazil", "British Virgin Islands",
    "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon",
    "Canada", "Cape Verde", "Cayman Islands", "Central Arfrican Republic",
    "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica",
    "Cote D Ivoire", "Croatia", "Cuba", "Curacao", "Cyprus", "Czech Republic",
    "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador",
    "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia",
    "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland",
    "France", "French Polynesia", "French West Indies", "Gabon", "Gambia",
    "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland",
    "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau",
    "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland",
    "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man",
    "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan",
    "Kazakhstan", "Kenya", "Kiribati", "Kosovo", "Kuwait", "Kyrgyzstan",
    "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein",
    "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi",
    "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania",
    "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia",
    "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
    "Nauro", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia",
    "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Korea", "Norway",
    "Oman", "Pakistan", "Palau", "Palestine", "Panama", "Papua New Guinea",
    "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico",
    "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre &amp; Miquelon",
    "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal",
    "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia",
    "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan",
    "Spain", "Sri Lanka", "St Kitts &amp; Nevis", "St Lucia", "St Vincent",
    "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan",
    "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga",
    "Trinidad &amp; Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks &amp; Caicos",
    "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom",
    "United States of America", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City",
    "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe"];

autocomplete(document.getElementById("nation"), countries);

function getID() {

    let idNumber = document.getElementById("idnumber").value;
    var valid_id = document.getElementById("invalid-ID");
    valid_id.innerHTML = "";

    if (idNumber.length < 13) {
        //Do nothing
    } else {
        document.getElementById("DoB").value = getDob(idNumber);    
        if (checkLuhn(idNumber) === false) {
            valid_id.innerHTML  = "ID number is invalid"
        }
    }
}

function getDob(idNumber) {
    var Year = idNumber.substring(0, 2);
    var Month = idNumber.substring(2, 4);
    var Day = idNumber.substring(4, 6);
    var Gender = idNumber.substring(6, 10);
    var Citizen = idNumber.substring(10, 11);
    var DOB;
    if (Year > 22) {
        DOB = "19" + Year + "-" + Month + "-" + Day;
    } else {
        DOB = "20" + Year + "-" + Month + "-" + Day;
    }
    //Used to change the gender using the ID provided
    let changeGender = document.getElementById("Gender");
    if (Gender < 5000) {
        changeGender.value = "Female";
    } else {
        changeGender.value = "Male";
    }
    //Used to change the nationality to SA using the ID provided
    if (Citizen == 0) {
        document.getElementById("nation").value = "South Africa"
    }
    return DOB;
}
function checkLuhn(purportedCC) {
    var nDigits = (purportedCC).length;
    var sum = 0;
    var parity = (nDigits - 2) % 2;
    for (var i = 0; i <= nDigits - 1; i++) {
        var digit = parseInt(purportedCC.charAt(i));
        //parseInt((""+d).charAt(0))
        if (i % 2 == parity)
            digit = digit * 2;
        if (digit > 9)
            digit = digit - 9;
        sum = sum + digit;
    }
    return (sum % 10) == 0;
}

document.getElementById("fund-request").addEventListener("click", function () {
    document.getElementById("fund-request").value = "Approved on date" + new Date().getDate().toString();
});

//document.getElementById("StudentUser").addEventListener("click", function () {
//    document.getElementById("change-user").ariaPlaceholder = "ID Number";
//    alert("Studen");
//});
//document.getElementById("InstitutionUser").addEventListener("click", function () {
//    document.getElementById("change-user").innerHTML = "Email";
//    alert("Institution");
//});
//document.getElementById("FunderUser").addEventListener("click", function () {
//    document.getElementById("change-user").innerText = "Email";
//    alert("Funder");
//})

//window.addEventListener("load",  function () {
//    changeUser();
//});

function changeUser() {
    var user = document.getElementById("active-user").value;
    var change_placeholder = document.getElementById("username");
    change_placeholder.value = "";
    if (user === "None") {
        $('#username').attr('placeholder', 'Please select user type first');
    } else if (user === "STUDENT_USER") {
            $('#username').attr('placeholder', 'Identity Number');
    }else {
        $('#username').attr('placeholder', 'Email Address');
    }
    
}
//Tax number has 10 digit and can only start with 0, 1, 2, 3 or 9 example 0001339050
function validatedTaxNr() {
    let taxNr = document.getElementById("tax-number").value;
    let validation = document.getElementById("invalid-TaxNr");
    validation.innerHTML = "";
    if (taxNr.length === 10) {
        if (checkFirstNr(parseInt(taxNr.charAt(0))) === false) {
            validation.innerHTML = "Invalid Tax Number, Check the first digit"
        }else if (checkLuhn(taxNr) === false) {
            validation.innerHTML = "Invalid Tax Number"
        }
    }
}
function checkFirstNr(digit) {
    if (digit <= 3 || digit === 9) {
        return true;
    } else {
        return false;
    }
}

