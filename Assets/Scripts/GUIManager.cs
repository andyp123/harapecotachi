using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUIManager : Singleton<GUIManager>
{

  // TODO: MAKE THIS WORK!

  // put GUI manager helper at root of every GUI prefab.
  // GUI manager helper gets all text elements in UI
  // if Text.text starts with "LOC_", it is a localization table key
  // if Text.text starts with "VAL_", it is a value that can change (e.g. score etc.)


  Dictionary<string, Text> _localizedTextComponents;
  Dictionary<string, Text> _valueTextComponents;

  Dictionary<string, string> _localizationStrings;

  protected GUIManager ()
  {
    _localizedTextComponents = new Dictionary<string, Text>();
    _valueTextComponents = new Dictionary<string, Text>();

    LoadLocalizationStrings();
    ApplyLanguage("en");
  }

  public void LoadLocalizationStrings ()
  {
    _localizationStrings = new Dictionary<string, string>();
  }

  public void ApplyLanguage (string languageKey)
  {

  }

  public void AddActiveTextComponents(List<Text> localizedComponents, List<Text> valueComponents)
  {
    foreach (Text t in localizedComponents)
    {
      if (!_localizedTextComponents.ContainsKey(t.text))
        _localizedTextComponents.Add(t.text, t);
        // TODO: load localization for this string
      else
        Debug.LogWarning(string.Format("The string '{0}' is already in use. Skipping this object ({1}).", t.text, t.gameObject.name));
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
