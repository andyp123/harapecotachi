using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUISync : MonoBehaviour
{
  List<Text> localizedComponents = null;
  List<Text> valueComponents = null;

  void Awake ()
  {
    localizedComponents = new List<Text>();
    valueComponents = new List<Text>();

    Text[] textComponents = gameObject.GetComponentsInChildren<Text>();
    string loc = "LOC_";
    string val = "VAL_";

    foreach (Text t in textComponents)
    {
      if (t.text.StartsWith(loc))
      {
        localizedComponents.Add(t);
      }
      else if (t.text.StartsWith(val))
      {
        valueComponents.Add(t);
      }
    }
  }

  void OnEnable ()
  {
    if (!Application.isPlaying)
      return;

    GUIManager.Instance.AddActiveTextComponents(localizedComponents, valueComponents);
  }

  void OnDisable ()
  {
    if (GUIManager.Instance != null)
      GUIManager.Instance.RemoveActiveTextComponents(localizedComponents, valueComponents);
  }
}
