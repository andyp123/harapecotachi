using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The GUISync class is used to automatically link GUI text components within a hierarchy
/// to the GUIManager when the the root node of the hierarchy (where the GUISync is attached)
/// gets enabled or disabled. This allows for effortless localization of static UI text.
/// GUISync also links text elements that are supposed to contain value strings to the
/// GUIManager so that they can be easily updated as long as the caller knows the name.
/// </sumary>
public class GUISync : MonoBehaviour
{
  public class ValueInfo
  {
    public Text textComponent;
    public string dataKey;
    public GameData.DataType dataType;
    public bool autoSync;
  }

  Dictionary<string, Text> _localizedComponents = null;
  Dictionary<string, ValueInfo> _valueComponents = null;

  void Awake ()
  {
    _localizedComponents = new Dictionary<string, Text>();
    _valueComponents = new Dictionary<string, ValueInfo>();

    Text[] textComponents = gameObject.GetComponentsInChildren<Text>();
    string loc = "LOC_";
    string val = "VAL(";
    string valEnd = ")";
    foreach (Text t in textComponents)
    {
      t.text.Trim();
      string text = t.text;
      
      if (text.StartsWith(loc))
      {
        if (!_localizedComponents.ContainsKey(text))
          _localizedComponents.Add(text, t);
        else
          Debug.LogWarning(string.Format("GUI contains multiple Text components with '{0}' localization key.", text));
      }
      else if (text.StartsWith(val) && text.EndsWith(valEnd))
      {
        string key;
        ValueInfo info;
        if (ParseValue(text, out key, out info))
        {
          info.textComponent = t;
          if (!_valueComponents.ContainsKey(text))
            _valueComponents.Add(key, info);
          else
            Debug.LogWarning(string.Format("GUI contains multiple Text components with '{0}' value key.", text));
        }
      }
    }
  }

  bool ParseValue (string valueString, out string key, out ValueInfo info)
  {
    // "VAL(DATA_KEY)"
    // shorthand that uses defaults:
    // GUI_KEY becomes "VAL_[DATA_KEY]"
    // DATA_TYPE defaults to int
    // AUTO_SYNC defaults to true

    // "VAL(GUI_KEY, DATA_KEY, DATA_TYPE, AUTO_SYNC)"
    // GUI_KEY: key used to access the text component through the GUIManager
    // DATA_KEY: key used to access the data in GameData
    // DATA_TYPE: type of data represented (Int, Float, String)
    // AUTO_SYNC: bind delegate to Data object in GameData that syncs the text component to the value (true/false)

    key = "";
    info = new ValueInfo();

    string argumentString = valueString.Substring(4, valueString.Length-5);
    string[] tokenStrings = argumentString.Split(',');
    if (tokenStrings.Length == 1)
    {
      key = "VAL_" + tokenStrings[0];
      info.dataKey = tokenStrings[0];
      info.dataType = GameData.DataType.Int;
      info.autoSync = true;
      return true;
    }
    if (tokenStrings.Length == 4)
    {
      key = tokenStrings[0];
      info.dataKey = tokenStrings[1];
      info.dataType = GameData.GetDataTypeFromString(tokenStrings[2]);
      info.autoSync = (tokenStrings[3] == "true") ? true : false;
      return true;
    }
    
    Debug.LogError(string.Format("[GUISync] Failed to parse value string with incorrect number of arguments: '{0}'", valueString));

    return false;
  }

  void OnEnable ()
  {
    if (!Application.isPlaying)
      return;

    if (Game.Instance != null)
      Game.Instance.GUIManager.AddActiveTextComponents(_localizedComponents, _valueComponents);
  }

  void OnDisable ()
  {
    if (Game.Instance != null)
      Game.Instance.GUIManager.RemoveActiveTextComponents(_localizedComponents, _valueComponents);
  }
}
