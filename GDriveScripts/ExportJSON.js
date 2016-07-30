// A script to export localization data from Google Sheets to a form
// that can be read by Unity3d's built-in JsonUtility.
//
// some of the code for this (mostly adding menu and export related) was taken from Pamela Fox's JSON
// exporter, which can be found here: https://gist.github.com/pamelafox/1878143

function onOpen() {
  var ss = SpreadsheetApp.getActiveSpreadsheet();
  var menuEntries = [
    {name: "Export JSON for this sheet", functionName: "exportSheet"},
  ];
  ss.addMenu("Export JSON", menuEntries);
}

function exportSheet() {
  var json = makeJSON_();
  displayText_(json);
}

function makeTextBox(app, name) { 
  var textArea = app.createTextArea().setWidth('100%').setHeight('100%').setId(name).setName(name);
  return textArea;
}

// TODO: some of the code in here is deprecated and might need updating at some point
function displayText_(text) {
  var app = UiApp.createApplication().setTitle('Exported JSON');
  app.add(makeTextBox(app, 'json'));
  app.getElementById('json').setText(text);
  var ss = SpreadsheetApp.getActiveSpreadsheet(); 
  ss.show(app);
  return app; 
}

// Each row in the sheet that has data in column A (EXCEPT the header) represents a localized string,
// with column being the key, and B onwards being strings in various languages.
// Due to the limited functionality of the JsonUtility in Unity, the data must be output as follows:
// { // wrapper object
//   "localizedStrings": [ // array of localized strings
//     { // a localized string
//       "key": "LOC_KEY",
//       "languages": [
//         { "languageCode": "en", "text": "defaultValue" },
//         { "languageCode": "ja", "text": "規定値" }
//       ]
//     }
//   ]
// }
function makeJSON_() {
  var sheet = SpreadsheetApp.getActiveSheet();
  var data = sheet.getDataRange().getValues();
  
  var jsonRoot = {};
  jsonRoot.localizedStrings = new Array();
  
  var keys = data[0];
  for (var i = 1; i < data.length; ++i) {
    var row = data[i];
    var localizedString = {};
    localizedString.key = row[0];
    localizedString.languages = new Array();
    
    for (var j = 1; j < row.length; ++j) {
      var languageString = {};
      languageString.languageCode = keys[j];
      languageString.text = row[j];
      localizedString.languages.push(languageString);
    }
    jsonRoot.localizedStrings.push(localizedString);
  }
  
  return JSON.stringify(jsonRoot, null, 2);
}
