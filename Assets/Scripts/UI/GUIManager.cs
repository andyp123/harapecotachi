using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : MonoBehaviour
{
  Dictionary<string, List<Text>> _localizedTextComponents;
  Dictionary<string, GUISync.ValueInfo> _valueTextComponents;

  void Awake ()
  {
    _localizedTextComponents = new Dictionary<string, List<Text>>();
    _valueTextComponents = new Dictionary<string, GUISync.ValueInfo>();
  }

  public void AddActiveTextComponents(Dictionary<string, Text> localizedComponents, Dictionary<string, GUISync.ValueInfo> valueComponents)
  {
    foreach (KeyValuePair<string, Text> entry in localizedComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      List<Text> textComponents = null;
      if (!_localizedTextComponents.TryGetValue(key, out textComponents))
      {
        textComponents = new List<Text>();
        _localizedTextComponents.Add(key, textComponents);
      }
      textComponents.Add(t);
      t.text = Localization.GetLocalizedText(key);
    }

    foreach (KeyValuePair<string, GUISync.ValueInfo> entry in valueComponents)
    {
      string key = entry.Key;
      GUISync.ValueInfo info = entry.Value;
      Text t = info.textComponent;

      if (!_valueTextComponents.ContainsKey(key))
      {
        _valueTextComponents.Add(key, info);
        if (info.autoSync)
          RegisterOnChangeUI(info, t);
      }
      else
        Debug.LogWarning(string.Format("[GUIManager] The string '{0}' is already in use. Skipping this object ({1}).", key, t.gameObject.name));
    }
  }

  public void RemoveActiveTextComponents(Dictionary<string, Text> localizedComponents, Dictionary<string, GUISync.ValueInfo> valueComponents)
  {
    foreach (KeyValuePair<string, Text> entry in localizedComponents)
    {      
      string key = entry.Key;
      Text t = entry.Value;

      List<Text> textComponents = null;
      if (_localizedTextComponents.TryGetValue(key, out textComponents))
      {
        if (textComponents.Count <= 1) // remove entire list
          _localizedTextComponents.Remove(key);
        else // remove matching text component
        {
          for (int i = 0; i < textComponents.Count; ++i)
          {
            if (Object.ReferenceEquals(t, textComponents[i]))
            {
              textComponents.RemoveAt(i);
              break;
            }
          }
        }
      }
    }

    foreach (KeyValuePair<string, GUISync.ValueInfo> entry in valueComponents)
    {
      string key = entry.Key;

      if (_valueTextComponents.ContainsKey(key))
      {
        GUISync.ValueInfo info = entry.Value;
        if (info.autoSync)
          DeregisterOnChangeUI(info);
        _valueTextComponents.Remove(key);
      }
    }
  }

  void RegisterOnChangeUI (GUISync.ValueInfo info, Text t)
  {
    switch (info.dataType)
    {
      case GameData.DataType.Int:
        GameData.Data<int> intData = GameData.GetIntData(info.dataKey);
        if (intData != null)
        {
          t.text = intData.ToString();
          intData.RegisterOnChangeUI( (v) => {
            t.text = v.ToString();
            });
        }
        break;
      case GameData.DataType.Float:
        GameData.Data<float> floatData = GameData.GetFloatData(info.dataKey);
        if (floatData != null)
        {
          t.text = floatData.ToString();
          floatData.RegisterOnChangeUI( (v) => {
            t.text = v.ToString();
            });                
        }
        break;
      case GameData.DataType.String:
        GameData.Data<string> stringData = GameData.GetStringData(info.dataKey);
        if (stringData != null)
        {
          t.text = stringData.ToString();
          stringData.RegisterOnChangeUI( (v) => {
            t.text = v.ToString();
            });                
        }
        break;
      default:
        Debug.LogWarning("[GUIManager] Can't autoSync unsupported data type.");
        break;
    }
  }

  void DeregisterOnChangeUI (GUISync.ValueInfo info)
  {
    switch (info.dataType)
    {
      case GameData.DataType.Int:
        GameData.Data<int> intData = GameData.GetIntData(info.dataKey);
        if (intData != null)
          intData.DeregisterOnChangeUI();
        break;
      case GameData.DataType.Float:
        GameData.Data<float> floatData = GameData.GetFloatData(info.dataKey);
        if (floatData != null)
          floatData.DeregisterOnChangeUI();
        break;
      case GameData.DataType.String:
        GameData.Data<string> stringData = GameData.GetStringData(info.dataKey);
        if (stringData != null)
          stringData.DeregisterOnChangeUI();
        break;
    }
  }

  public void SetGUITextValue (string key, string val)
  {
    if (_valueTextComponents.ContainsKey(key))
      _valueTextComponents[key].textComponent.text = val;
  }

  public void ApplyLanguage(string languageCode = null)
  {
    if (languageCode != null)
      Localization.SetCurrentLanguage(languageCode);

    foreach(KeyValuePair<string, List<Text>> entry in _localizedTextComponents)
    {
      string key = entry.Key;
      List<Text> textComponents = entry.Value;

      string localizedText = Localization.GetLocalizedText(key);
      foreach (Text t in textComponents)
      {
        t.text = localizedText;
      }
    }
  }
}
