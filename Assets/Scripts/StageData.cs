using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class SerializableObjectData
{
  public string _prefabKey = "";
  public Vector3 _position = Vector3.zero;
  public Quaternion _rotation = Quaternion.identity;
  public Vector3 _scale = Vector3.one;
}

public class StageData
{
  // data that can be serialized directly
  public List<Wave> _waves = new List<Wave>();
  public List<Path> _paths = new List<Path>();
  public List<SerializableObjectData> _objects = new List<SerializableObjectData>();

  // more convenient structures for working with the level while it's in memory


  public bool SaveDataToFile(string filename)
  {
    string path = Application.persistentDataPath + filename;

    using (FileStream fs = new FileStream(path, FileMode.Create))
    {
      using (StreamWriter writer = new StreamWriter(fs))
      {
        string json = "";
        foreach (Path p in _paths)
        {
          json = JsonUtility.ToJson(p);
          writer.Write(json);
        }
        foreach (Wave w in _waves)
        {
          json = JsonUtility.ToJson(w);
          writer.Write(json);
        }
      }
    }

    return false;
  }

  public bool LoadDataFromFile(string filename)
  {
    return false;
  }
}
