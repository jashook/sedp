//g_xmlData = null;

function loadXmlNewsData() {
    $.get('news.xml', function (xmlData) {
        //alert(xmlData);
        //$(xmlData).find('entry')
        displayNewsFeed(xmlData, 9);
    });
}

// EXAMPLE NEWS ELEMENT TO PARSE
//  <entry title="CHI2012 Honorable Mention" date="4/2/2012">
//    <![CDATA[
//   Our CHI2012 eco-feedback paper received an "Honorable Mention" award. Download the paper <a href="publications.html">here</a>.
//   ]]>
//  </entry>

function displayNewsFeed(xmlData, totalNumToDisplay) {
    //g_xmlData = xmlData;
    var entries = xmlData.getElementsByTagName('entry');
    var html = "";
    for (var i = 0; i < entries.length && i < totalNumToDisplay; i++) {
        var entry = entries[i];
        var entryTitle = entry.getAttribute('title');
        var entryDateStr = entry.getAttribute('date');
        var entryDate = new Date(Date.parse(entryDateStr));
        var entryContent = $.trim($(entry).text());

        //OLD WAY OF GETTING AROUND IE NOT USING TEXTCONTENT
//        var entryContent = "";
//        if ($.browser.msie) {
//            entryContent = $.trim(entry.text);
//        }
//        else {
//            entryContent = $.trim(entry.textContent);
//        }

        //alert(entryContent);
        html += "<span class='news_header'>" + (entryDate.getMonth() + 1) + "." + entryDate.getDate() + " - " + entryTitle + "</span><br/>";
        html += entryContent;
        html += "<br/><br/>";
    }
    document.getElementById('news_feed').innerHTML = html;
}