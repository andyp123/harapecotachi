using UnityEngine;
using System.Collections;

// for file serialization
[System.Serializable]
public struct SerializableWave
{
  public string monsterType;
  public string pathName;
  public int groupId;
  public int spawnCount;
  public float startDelay;
  public float spawnDelay;
}

public class Wave : MonoBehaviour
{
  public int _groupID = 1; // used by the wave manager to group waves
  public string _monsterType = ""; // monster type to spawn
  public int _spawnCount = 10; // number of monsters in the wave
  public float _startDelay = 0f; // delay before first monster is spawned
  public float _spawnDelay = 1f; // spawn next monster after this many seconds
  public Path _path; // which path to spawn monsters on

  bool _finishedSpawning = false;
  public bool FinishedSpawning 
  {
    get { return _finishedSpawning; }
  }


  void Start ()
  {
    StartCoroutine(SpawnMonsters());
  }

  IEnumerator SpawnMonsters ()
  {
    yield return new WaitForSeconds(_startDelay);

    AssetManager assetManager = Game.Instance.AssetManager;

    for (int i = 0; i < _spawnCount; ++i)
    {
      GameObject monster = assetManager.InstantiatePrefab(_monsterType, Vector3.zero, Quaternion.identity);

      if (_path != null && monster != null)
      {
        PathMover pm = monster.GetComponent<PathMover>();
        if (pm != null)
          pm._path = _path;
      }
      yield return new WaitForSeconds(_spawnDelay);
    }

    _finishedSpawning = true;
  }
}
