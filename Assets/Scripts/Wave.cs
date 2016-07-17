using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
  public string _monsterType = ""; // monster type to spawn
  public int _spawnCount = 10; // number of monsters in the wave
  public float _spawnDelay = 1f; // spawn next monster after this many seconds
  public Path _path; // which path to spawn monsters on

  // quick hack for now
  public InstancedObjectManager _objectManager;


  void Start ()
  {
    if (_objectManager != null)
     StartCoroutine(SpawnMonsters());
  }

  IEnumerator SpawnMonsters ()
  {
    for (int i = 0; i < _spawnCount; ++i)
    {
      Debug.Log("spawn monster");
      GameObject monster = _objectManager.Instantiate(_monsterType, Vector3.zero, Quaternion.identity);
      if (_path != null && monster != null)
      {
        PathMover pm = monster.GetComponent<PathMover>();
        if (pm != null)
          pm._path = _path;
      }
      yield return new WaitForSeconds(_spawnDelay);
    }
  }
}
