using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : Singleton<GUIManager>
{
  protected GUIManager () {}

  Dictionary<string, Text> _localizedTextComponents;
  Dictionary<string, Text> _valueTextComponents;

  Dictionary<string, Localization.LocalizedString> _localizationStrings = null; // TODO: should not be in GUIManager!
  string _currentLanguage = "en"; // TODO: should also not be in GUIManager

  bool _initialized = false; // TODO: can remove this if the initialization order can be guaranteed

  void Awake ()
  {
    Initialize();
  }

  void Initialize ()
  {
    if (_initialized)
      return;

    _localizedTextComponents = new Dictionary<string, Text>();
    _valueTextComponents = new Dictionary<string, Text>();

    string path = "Assets/Resources/LocalizationText.json";
    TextAsset localizationJson = (TextAsset)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

    if (localizationJson != null)
      _localizationStrings = Localization.LoadLanguageData(localizationJson.text);
    else
      Debug.Log("Localization file not found. All GUI text will remain as it is.");

    _initialized = true;
  }

  public void AddActiveTextComponents(Dictionary<string, Text> localizedComponents, Dictionary<string, Text> valueComponents)
  {
    if (!_initialized) // TODO: This sucks. Fix init order of various modules
      Initialize();

    foreach (KeyValuePair<string, Text> entry in localizedComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      if (!_localizedTextComponents.ContainsKey(key))
      {
        _localizedTextComponents.Add(key, t);
        
        if (_localizationStrings.ContainsKey(key))
        {
          Localization.LocalizedString localizedString = _localizationStrings[key];
          t.text = localizedString.GetText(_currentLanguage);
        }
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
        _valueTextComponents.Add(key, t);
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
      }
    }
  }

  public void SetGUITextValue (string key, string val)
  {
    if (_valueTextComponents.ContainsKey(key))
      _valueTextComponents[key].text = val;
  }

  // TODO: remove. this is a test function for checking localization
  public void ToggleLanguage ()
  {
    _currentLanguage = (_currentLanguage == "en") ? "ja" : "en";
    ApplyLanguage(_currentLanguage);
  }

  public void ApplyLanguage(string languageCode)
  {
    foreach(KeyValuePair<string, Text> entry in _localizedTextComponents)
    {
      string key = entry.Key;
      Text t = entry.Value;

      Localization.LocalizedString localizedString;// = new Localization.LocalizedString(key);
      if (_localizationStrings.TryGetValue(key, out localizedString))
      {
        t.text = localizedString.GetText(languageCode); 
      }
    }
  }
}
