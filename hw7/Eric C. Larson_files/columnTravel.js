//g_xmlData = null;

function loadXmlTravelData() {
    $.get('travel.xml', function (xmlData) {
        //alert(xmlData);
        //$(xmlData).find('entry')
        displayTravelFeed(xmlData);
    });
}

// EXAMPLE XML TO PARSE
//  <event name="BECC 2010">
//    <location>Sacramento, CA</location>
//    <link>http://peec.stanford.edu/events/2010/becc/</link>
//    <startdate>11/14/2010</startdate>
//    <enddate>11/17/2010</enddate>
//  </event>
function displayTravelFeed(xmlData) {
    g_xmlData = xmlData;
    var xmlEvents = xmlData.getElementsByTagName('event');
    var travelEvents = [];

    for (var i = 0; i < xmlEvents.length; i++) {
        var event = xmlEvents[i];
        var eventName = event.getAttribute('name');
        var eventChildNodes = event.childNodes;
        var mapNameToValue = {};
        for (var j = 0; j < eventChildNodes.length; j++) {
            var eventChild = eventChildNodes[j];
            if (eventChild.nodeType == 1) {
                if (eventChild.nodeName.indexOf("date") != -1) {
                    var dateStr = $(eventChild).text();
                    var date = new Date(Date.parse(dateStr));
                    mapNameToValue[eventChild.nodeName] = date;
                }
                else {
                    mapNameToValue[eventChild.nodeName] = $(eventChild).text();
                }
            }
        }
        var travelEvent = new TravelEvent(eventName, mapNameToValue);
        travelEvents.push(travelEvent);
    }
    travelEvents.sort(sortTravelEventsByStartDate);
    var indexOfNextTravelEvent = getIndexOfNextTravelEvent(travelEvents);
    var numShowPrevTravelEvents = 2;
    var startIndex = Math.max(0, indexOfNextTravelEvent - numShowPrevTravelEvents);
    
    var html = "";
    var curDate = new Date();
    for (var i = startIndex; i < travelEvents.length; i++) {
        var travelEvent = travelEvents[i];
        if (curDate.getTime() > travelEvent.getEndDate().getTime()) {
            html += "<span class='travel_past'>";
        }
        else {
            html += "<span class='travel_future'>";
        }
        
        html += "<span class='travel_dates'>" + getDateString(travelEvent.getStartDate(), travelEvent.getEndDate()) + "</span><br />"

        if (travelEvent.getLink() == null) {
            html += "<a href='#'>" + travelEvent.name + "</a><br />";
        }
        else {
            html += "<a href='" + travelEvent.getLink() + "'>" + travelEvent.name + "</a><br />";
        }
        
        html += travelEvent.getLocation() + "</span><br /><br />";
    }
    document.getElementById('travel_feed').innerHTML = html;
}

function getIndexOfNextTravelEvent(travelEvents) {
    var curDate = new Date();
    for (var i = 0; i < travelEvents.length; i++) {
        var travelEvent = travelEvents[i];
        if (travelEvent.getEndDate().getTime() >= curDate.getTime()) {
            return i - 1;
        }
    }
    return travelEvents.length - 1;
}

function sortTravelEventsByStartDate(travelEvent1, travelEvent2) {
    var x = travelEvent1.getStartDate().getTime();
    var y = travelEvent2.getStartDate().getTime();
    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
}

function getDateString(startDate, endDate){
    if (startDate.getTime() == endDate.getTime()) {
        return DateTimeUtils.getMonthNameFromNumber(startDate.getMonth() + 1) + " " + startDate.getDate() + ", " + startDate.getFullYear();  
    }

    var startDateStr = DateTimeUtils.getShortMonthNameFromNumber(startDate.getMonth() + 1) + " " + startDate.getDate();
    var endDateStr = "";

    if (startDate.getYear() == endDate.getYear() &&
       startDate.getMonth() == endDate.getMonth()) {
        endDateStr = "" + endDate.getDate();
    }
    else {
        endDateStr = DateTimeUtils.getShortMonthNameFromNumber(endDate.getMonth() + 1) + " " + endDate.getDate();
    }
    
    if(startDate.getYear() != endDate.getYear()){
        startDateStr = startDateStr + ", " + startDate.getFullYear();
    }
    return startDateStr + " - " + endDateStr + ", " + endDate.getFullYear();
}

function TravelEvent(eventName, mapNameToValue) {
    this.name = eventName;
    var _mapNameToValue = mapNameToValue;
    
    this.getStartDate = function () {
        return _mapNameToValue['startdate'];
    };

    this.getEndDate = function () {
        return _mapNameToValue['enddate'];
    };

    this.getLink = function () {
        return _mapNameToValue['link'];
    };

    this.getLocation = function () {
        return _mapNameToValue['location'];
    };
}