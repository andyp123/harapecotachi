using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: convert everything to use the actual language codes

/// <summary>
/// Static class that handles storage and interaction with localization data.
/// </summary>
public static class Localization
{
  /// <summary>
  /// Enum that contains ISO 639-1 language codes instead of using Unity's built in enum.
  /// </summary>
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

  /// <Summary>
  /// Used to store localization keys and the appropriate strings for each supported translation.
  /// </summary>
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


  // store localization data here
  static Dictionary<string, LocalizedString> _languageData = null;
  static string _currentLanguage = "en";

  public static void LoadLocalizationFile (TextAsset localizationFile = null)
  {
    if (localizationFile == null)
    {
      // string defaultPath = "Assets/Resources/LocalizationText.json";
      // localizationFile = (TextAsset)UnityEditor.AssetDatabase.LoadAssetAtPath(defaultPath, typeof(TextAsset));
      localizationFile = Resources.Load<TextAsset>("LocalizationText");
    }

    if (localizationFile != null)
      DeserializeLanguageFile(localizationFile.text);
    else
      Debug.LogError("Localization file not found");
  }

  static void DeserializeLanguageFile (string json)
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
            Debug.LogWarning(string.Format("[Localization] The language code '{0}' does not match a supported language.", languageCode));
          }
        }

        languageData.Add(key, localizedString);
      }
    }

    _languageData = languageData;
  }

  /// <summary>
  /// Gets the text for the passed in localization key in the currently set language or an
  /// optionally specified one.
  /// </summary>
  public static string GetLocalizedText(string key, string languageCode = null)
  {
    if (_languageData == null)
    {
      Debug.LogWarning("[Localization] No localization data has been loaded, returning input.");
      return key;
    }

    LocalizedString localizedString = null;
    if (_languageData.TryGetValue(key, out localizedString))
    {
      if (string.IsNullOrEmpty(languageCode))
        return localizedString.GetText(_currentLanguage);
      else
        return localizedString.GetText(languageCode);
    }

    Debug.LogWarning(string.Format("[Localization] No data for key {0} exists in the localization data.", key));
    return key;
  }

  public static string GetCurrentLanguage()
  {
    return _currentLanguage;
  }

  public static void SetCurrentLanguage(string languageCode)
  {
    if (IsSupportedLanguage(languageCode))
      _currentLanguage = languageCode;
    else
      Debug.LogWarning(string.Format("[Localization] The language code '{0}' does not match a supported language.", languageCode));
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
