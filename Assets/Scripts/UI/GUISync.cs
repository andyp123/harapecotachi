using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUISync : MonoBehaviour
{
  Dictionary<string, Text> _localizedComponents = null;
  Dictionary<string, Text> _valueComponents = null;

  void Awake ()
  {
    _localizedComponents = new Dictionary<string, Text>();
    _valueComponents = new Dictionary<string, Text>();

    Text[] textComponents = gameObject.GetComponentsInChildren<Text>();
    string loc = "LOC_";
    string val = "VAL_";

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
      else if (text.StartsWith(val))
      {
        if (!_valueComponents.ContainsKey(text))
          _valueComponents.Add(text, t);
        else
          Debug.LogWarning(string.Format("GUI contains multiple Text components with '{0}' value key.", text));
      }
    }
  }

  void OnEnable ()
  {
    if (!Application.isPlaying)
      return;

    GUIManager.Instance.AddActiveTextComponents(_localizedComponents, _valueComponents);
  }

  void OnDisable ()
  {
    if (GUIManager.Instance != null)
      GUIManager.Instance.RemoveActiveTextComponents(_localizedComponents, _valueComponents);
  }
}
