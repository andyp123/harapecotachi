using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Localization
{
  // use Application.systemLanguage instead?
  public enum LanguageCode
  {
    en, // English
    ja, // Japanese
  //   fr, // French
  //   de, // German
  //   es, // Spanish
  //   it, // Italian
  //   ru, // Russian
  //   pt, // Portugese
  //   zh, // Zhongwen, Chinese
  }

  // used only for reading JSON using JsonUtility, which is somewhat limited
  [System.Serializable]
  public struct SerializableLanguageString
  {
    public string languageCode; // using ISO 639-1
    public string text;
  }

  [System.Serializable]
  public struct SerializableLocalizedString
  {
    public string key;
    public SerializableLanguageString[] languages;
  }

  [System.Serializable]
  public struct SerializableLocalizedStrings
  {
    public SerializableLocalizedString[] localizedStrings;
  }


  // used for actual storage of data in memory
  public class LocalizedString
  {
    public string key;
    private Dictionary<string, string> _languages;

    public LocalizedString(string key)
    {
      this.key = key;
      _languages = new Dictionary<string, string>();
    }

    public void AddLanguage(string languageCode, string text)
    {
      if (!_languages.ContainsKey(languageCode))
      {
        _languages.Add(languageCode, text);
      }
      else
      {
        Debug.LogWarning(string.Format("The string '{0}' already contains a value for the language '{1}'", key, languageCode));
      }
    }

    public string GetText (string languageCode)
    {
      string text = "";
      if (_languages.TryGetValue(languageCode, out text))
        return text;
      else
        return key;
    }
  }


  public static Dictionary<string, LocalizedString> LoadLanguageData (string json)
  {
    SerializableLocalizedStrings sLocalizedStrings = JsonUtility.FromJson<SerializableLocalizedStrings>(json);

    Dictionary<string, LocalizedString> languageData = new Dictionary<string, LocalizedString>();

    foreach (SerializableLocalizedString sLocalizedString in sLocalizedStrings.localizedStrings)
    {
      string key = sLocalizedString.key;

      if (key != "")
      {
        LocalizedString localizedString = new LocalizedString(key);

        foreach (SerializableLanguageString sLanguageString in sLocalizedString.languages)
        {
          string languageCode = sLanguageString.languageCode;
          string text = sLanguageString.text;

          if (IsSupportedLanguage(languageCode))
          {
            localizedString.AddLanguage(languageCode, text);
          }
          else
          {
            Debug.LogWarning(string.Format("The language code '{0}' does not match a supported language.", languageCode));
          }
        }

        languageData.Add(key, localizedString);
      }
    }

    return languageData;
  }

  public static bool IsSupportedLanguage(string languageCode)
  {
    foreach (string code in System.Enum.GetNames(typeof(LanguageCode)))
    {
      if (languageCode == code)
        return true;
    }

    return false;
  }
}
