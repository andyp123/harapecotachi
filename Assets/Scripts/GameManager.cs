using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
  public NamedPrefab[] _prefabs;

  private PrefabMap _prefabMap;

  protected GameManager ()
  {
    _prefabMap = new PrefabMap();
  }

  void Awake ()
  {
    _prefabMap.AddPrefabs(_prefabs);
  }

  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    return _prefabMap.InstantiatePrefab(prefabName, position, rotation);
  }
}
