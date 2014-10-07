//for more information on why/how I implemented this static function
//pattern, see: http://blog.anselmbradford.com/2009/04/09/object-oriented-javascript-tip-creating-static-methods-instance-methods/

function DateTimeUtils() {
    //empty
}

DateTimeUtils.convertMonthNameToNumber = function(monthName) {
    if (monthName == null) {
        alert("Month name is null!");
    }

    var monthName3Letters = monthName.toLowerCase().substring(0, 3);

    switch (monthName3Letters) {
        case "jan":
            return 1;
        case "feb":
            return 2;
        case "mar":
            return 3;
        case "apr":
            return 4;
        case "may":
            return 5;
        case "jun":
            return 6;
        case "jul":
            return 7;
        case "aug":
            return 8;
        case "sep":
            return 9;
        case "oct":
            return 10;
        case "nov":
            return 11;
        case "dec":
            return 12;
    }
    return -1;
}

DateTimeUtils.getShortDateString = function (date) {
    return DateTimeUtils.getShortMonthNameFromNumber(date.getMonth() + 1) + " " + date.getDate() + ", " + date.getFullYear();
}

// monthNumber must be 1 - 12
DateTimeUtils.getMonthNameFromNumber = function (monthNumber) {
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    return monthNames[monthNumber - 1];
}

// monthNumber must be 1 - 12
DateTimeUtils.getShortMonthNameFromNumber = function (monthNumber) {
    return DateTimeUtils.getMonthNameFromNumber(monthNumber).substr(0, 3);
}

//static function declaration for SortUtils
function SortUtils() {

}

SortUtils.sortNumbersDescending = function (num1, num2) {
    return (num2 - num1);
}

SortUtils.sortNumbersAscending = function (num1, num2) {
    return (num1 - num2);
}

function HttpUtils() {

}

// from http://stackoverflow.com/questions/3646914/how-do-i-check-if-file-exists-in-jquery-or-javascript
HttpUtils.checkIfUrlExist = function(url){
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status!=404;
}