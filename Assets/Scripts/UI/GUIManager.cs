using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : MonoBehaviour
{
  Dictionary<string, Text> _localizedTextComponents;
  Dictionary<string, Text> _valueTextComponents;

  void Awake ()
  {
    _localizedTextComponents = new Dictionary<string, Text>();
    _valueTextComponents = new Dictionary<string, Text>();
  }

  public void AddActiveTextComponents(Dictionary<string, Text> localizedComponents, Dictionary<string, Text> valueComponents)
  {
    foreach (KeyValuePair<string, Text> entry in localizedComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      if (!_localizedTextComponents.ContainsKey(key))
      {
        _localizedTextComponents.Add(key, t);
        t.text = Localization.GetLocalizedText(key);
      }
      else
      {
        Debug.LogWarning(string.Format("The string '{0}' is already in use. Skipping this object ({1}).", key, t.gameObject.name));
      }
    }

    foreach (KeyValuePair<string, Text> entry in valueComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      if (!_valueTextComponents.ContainsKey(key))
      {
        _valueTextComponents.Add(key, t);
        GameData.Data<int> intData = GameData.GetIntData(key);
        if (intData != null)
        {
          t.text = intData.ToString();
          intData.RegisterOnChangeUI( (v) => {
            t.text = v.ToString();
            });
        }
      }
      else
        Debug.LogWarning(string.Format("The string '{0}' is already in use. Skipping this object ({1}).", key, t.gameObject.name));
    }
  }

  public void RemoveActiveTextComponents(Dictionary<string, Text> localizedComponents, Dictionary<string, Text> valueComponents)
  {
    foreach (KeyValuePair<string, Text> entry in localizedComponents)
    {      
      string key = entry.Key;

      if (_localizedTextComponents.ContainsKey(key))
      {
        _localizedTextComponents.Remove(key);
      }
    }
    foreach (KeyValuePair<string, Text> entry in valueComponents)
    {
      string key = entry.Key;

      if (_valueTextComponents.ContainsKey(key))
      {
        _valueTextComponents.Remove(key);
        GameData.Data<int> intData = GameData.GetIntData(key);
        if (intData != null)
          intData.DeregisterOnChangeUI();
      }
    }
  }

  public void SetGUITextValue (string key, string val)
  {
    if (_valueTextComponents.ContainsKey(key))
      _valueTextComponents[key].text = val;
  }

  public void ApplyLanguage(string languageCode = null)
  {
    if (languageCode != null)
      Localization.SetCurrentLanguage(languageCode);

    foreach(KeyValuePair<string, Text> entry in _localizedTextComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      t.text = Localization.GetLocalizedText(key);
    }
  }
}
