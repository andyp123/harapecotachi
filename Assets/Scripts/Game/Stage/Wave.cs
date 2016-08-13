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
  public int _moneyPot = 0; // money to be distributed evenly among spawned monsters
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

    float moneyPerMonster = (float)_moneyPot / _spawnCount;
    float moneyPaid = 0f;

    for (int i = 0; i < _spawnCount; ++i)
    {
      int money = (int)Mathf.Floor(moneyPerMonster * (i + 1) - moneyPaid);
      moneyPaid += money;
      GameObject monster = assetManager.InstantiatePrefab(_monsterType, Vector3.zero, Quaternion.identity);
      monster.GetComponent<Monster>().money = money;

      GameData.Data<int> intData = GameData.GetIntData("VAL_SPAWNED");
      if (intData != null)
        intData.Value += 1;

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
