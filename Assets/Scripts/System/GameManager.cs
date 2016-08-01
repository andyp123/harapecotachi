using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
  public NamedPrefab[] _prefabs;

  private PrefabMap _prefabMap;


  void Awake ()
  {
    DontDestroyOnLoad(this.gameObject);
    _prefabMap = new PrefabMap();

    if (_prefabs != null)
      _prefabMap.AddPrefabs(_prefabs);
  }

  // TODO: should really move to an instance manager or some such class
  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    return _prefabMap.InstantiatePrefab(prefabName, position, rotation);
  }

  // TODO: want a proper pause implementation. This is kind of weird
  //   also, Game should probably deal with pausing, or have a PauseManager class
  public void TogglePause ()
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }
}
