using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : Singleton<GUIManager>
{
  public TextAsset _localizationJSON;

  private Dictionary<string, Text> _localizedTextComponents;
  private Dictionary<string, Text> _valueTextComponents;

  private Dictionary<string, Localization.LocalizedString> _localizationStrings = null;
  private string _currentLanguage = "en";

  private bool _initialized = false;


  protected GUIManager ()
  {
    Initialize();
  }

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

    if (_localizationJSON != null)
    {
      _localizationStrings = Localization.LoadLanguageData(_localizationJSON.text);

      ApplyLanguage(_currentLanguage);
    }
  }

  public void ApplyLanguage (string languageKey)
  {
    if (_localizationStrings != null)
    {

    }
  }

  public void AddActiveTextComponents(List<Text> localizedComponents, List<Text> valueComponents)
  {
    foreach (Text t in localizedComponents)
    {
      if (!_localizedTextComponents.ContainsKey(t.text))
      {
        _localizedTextComponents.Add(t.text, t);
        
        if (_localizationStrings.ContainsKey(t.text))
        {
          Localization.LocalizedString localizedString = _localizationStrings[t.text];
          t.text = localizedString.GetText(_currentLanguage);
        }
      }
      else
      {
        Debug.LogWarning(string.Format("The string '{0}' is already in use. Skipping this object ({1}).", t.text, t.gameObject.name));
      }
    }

    foreach (Text t in valueComponents)
    {
      if (!_valueTextComponents.ContainsKey(t.text))
        _valueTextComponents.Add(t.text, t);
      else
        Debug.LogWarning(string.Format("The string '{0}' is already in use. Skipping this object ({1}).", t.text, t.gameObject.name));
    }
  }

  public void RemoveActiveTextComponents(List<Text> localizedComponents, List<Text> valueComponents)
  {
    // TODO: this doesn't work because the Text.text is being used as the key, but the value changes
    // when the game is running, so it can't be looked up once it has changed.
    // think about what is really necessary, necessary, kimi wa primary
  }

  public void SetGUITextValue (string key, string val)
  {
    if (_valueTextComponents.ContainsKey(key))
      _valueTextComponents[key].text = val;
  }
}
