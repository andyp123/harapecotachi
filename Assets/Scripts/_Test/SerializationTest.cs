using UnityEngine;
using System.Collections;

public class SerializationTest : MonoBehaviour
{
  void Start ()
  {
    Serialize();
  }

  void Serialize ()
  {
    string json = "";
    string fileContent = "\"paths\": [\n";

    Path[] paths = gameObject.GetComponentsInChildren<Path>();
    foreach (Path p in paths)
    {
      json = JsonUtility.ToJson(p, false);
      fileContent += json + ",\n";
    }
    fileContent += "],\n \"waves\": [\n";

    Wave[] waves = gameObject.GetComponentsInChildren<Wave>();
    foreach (Wave w in waves)
    {
      json = JsonUtility.ToJson(w, false);
      fileContent += json + ",\n";
    }
    fileContent += "]\n";
    Debug.Log(fileContent);
  }
}
