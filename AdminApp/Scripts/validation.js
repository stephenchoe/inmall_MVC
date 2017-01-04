function atLeastOneUppercaseLetter(value) {
    return /[A-Z]+/.test(value);
}

function validateDate(value) {   
    dtRegex = new RegExp(/\d{4}\b[\/-]\d{1,2}[\/-]\d{1,2}/);
    return dtRegex.test(value);
}