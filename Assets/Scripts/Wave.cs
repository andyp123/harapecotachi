using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave : MonoBehaviour
{
  // TODO: need a proper solution for game data such as this
  static int _spawned = 0;

  public enum WaveState
  {
    ready,
    spawning,
    finished
  };

  public int _groupID = 1; // used by the wave manager to group waves
  public string _monsterType = ""; // monster type to spawn
  public int _spawnCount = 10; // number of monsters in the wave
  public float _startDelay = 0f; // delay before first monster is spawned
  public float _spawnDelay = 1f; // spawn next monster after this many seconds
  public Path _path; // which path to spawn monsters on

  // quick hack for now
  // public PrefabMap _prefabMap;

  private WaveState _state = WaveState.ready;
  public WaveState State
  {
    get { return _state; }
  }


  void Start ()
  {
    StartCoroutine(SpawnMonsters());
  }

  IEnumerator SpawnMonsters ()
  {
    _state = WaveState.spawning;
    yield return new WaitForSeconds(_startDelay);

    for (int i = 0; i < _spawnCount; ++i)
    {
      GameObject monster = GameManager.Instance.InstantiatePrefab(_monsterType, Vector3.zero, Quaternion.identity);
      _spawned += 1;
      GUIManager.Instance.SetGUITextValue("VAL_SPAWNED", _spawned.ToString());
      if (_path != null && monster != null)
      {
        PathMover pm = monster.GetComponent<PathMover>();
        if (pm != null)
          pm._path = _path;
      }
      yield return new WaitForSeconds(_spawnDelay);
    }
    _state = WaveState.finished;
  }
}
