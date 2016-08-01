using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
  public NamedPrefab[] _prefabs;

  private PrefabMap _prefabMap;


  void Awake ()
  {
    _prefabMap = new PrefabMap();
    _prefabMap.AddPrefabs(_prefabs);
  }

  // void Update ()
  // {
  //   if (Input.GetKeyDown(KeyCode.R))
  //   {
  //     Restart();
  //   }
  // }

  // TODO: should really move to an instance manager or some such class
  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    return _prefabMap.InstantiatePrefab(prefabName, position, rotation);
  }

  // TODO: want a proper pause implementation. This is kind of weird
  public void TogglePause ()
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }
}
