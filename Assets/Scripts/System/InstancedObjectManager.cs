using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct NamedPrefab
{
  public string key;
  public GameObject prefab;
}

public class InstancedObjectManager : MonoBehaviour
{
  public NamedPrefab[] _prefabs;
  private List<GameObject> _instantiatedObjects;
  private Dictionary<string, GameObject> _prefabMap;

  void Awake ()
  {
    _instantiatedObjects = new List<GameObject>();
    _prefabMap = new Dictionary<string, GameObject>();
    foreach (NamedPrefab item in _prefabs)
    {
      if (item.key != "" && item.prefab != null)
        _prefabMap.Add(item.key, item.prefab);
    }
  }

  public GameObject Instantiate (string prefabName, Vector3 position, Quaternion rotation)
  {
    GameObject prefab = null;
    GameObject obj = null;

    if (_prefabMap.TryGetValue(prefabName, out prefab))
    {
      obj = Instantiate(prefab, position, rotation) as GameObject;
      _instantiatedObjects.Add(obj);
    }

    return obj;
  }

  public GameObject GetNearestObject (Vector3 position)
  {
    float nearestDistanceSq = -1f;
    GameObject nearestObj = null;

    foreach (GameObject obj in _instantiatedObjects)
    {
      float distSq = (obj.transform.position - position).sqrMagnitude;
      if (distSq < nearestDistanceSq || nearestDistanceSq == -1f)
      {
        nearestDistanceSq = distSq;
        nearestObj = obj;
      }
    }

    return nearestObj;
  }
}
