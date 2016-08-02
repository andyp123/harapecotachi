using UnityEngine;
using System.Collections;

public class AssetManager : MonoBehaviour
{
  PrefabMap _prefabMap = null;

  void Awake ()
  {
    _prefabMap = new PrefabMap();
  }

  public void LoadAssets (NamedPrefab[] prefabs)
  {
    _prefabMap.AddPrefabs(prefabs);
  }

  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    return _prefabMap.InstantiatePrefab(prefabName, position, rotation);
  }
}
