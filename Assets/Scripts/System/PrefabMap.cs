using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct NamedPrefab
{
  public string key;
  public GameObject prefab;
}


public class PrefabMap
{
  private Dictionary<string, GameObject> _prefabMap;

  public PrefabMap ()
  {
    _prefabMap = new Dictionary<string, GameObject>();
  }

  public void AddPrefabs (NamedPrefab[] prefabs)
  {
    foreach (NamedPrefab item in prefabs)
    {
      if (item.key != "" && item.prefab != null)
        _prefabMap.Add(item.key, item.prefab);
    }
  }

  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    GameObject prefab = null;

    _prefabMap.TryGetValue(prefabName, out prefab);
    if (prefab != null)
    {
      GameObject obj = GameObject.Instantiate(prefab, position, rotation) as GameObject;
      return obj;
    }
    else
    {
      Debug.LogError("[PrefabMap] Prefab with id \"" + prefabName + "\" not found.");
    }

    return null;
  }
}
