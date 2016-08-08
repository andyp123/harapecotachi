using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// TODO: Should location and values both be handled in the same component instance?
//   Why not set a field on the component that sets the mode. This would simplify the
//   code as there would only need to be one table of text components.

// TODO: move the localization code into awake of GUISync instead of the 
//   GUIManager.AddActiveTextComponents function to avoid setting on enable/disable

/// <summary>
/// The GUISync class is used to automatically link GUI text components within a hierarchy
/// to the GUIManager when the the root node of the hierarchy (where the GUISync is attached)
/// gets enabled or disabled. This allows for effortless localization of static UI text.
/// GUISync also links text elements that are supposed to contain value strings to the
/// GUIManager so that they can be easily updated as long as the caller knows the name.
/// </sumary>
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

    if (Game.Instance != null)
      Game.Instance.GUIManager.AddActiveTextComponents(_localizedComponents, _valueComponents);
  }

  void OnDisable ()
  {
    if (Game.Instance != null)
      Game.Instance.GUIManager.RemoveActiveTextComponents(_localizedComponents, _valueComponents);
  }
}
