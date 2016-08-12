using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour
{
  public string _playerPrefabKey = "PLAYER";
  public bool _parentSpawnedObject = true;

  bool _spawned = false;

  void Awake ()
  {
    Spawn();
  }

  public void Spawn ()
  {
    if (!_spawned)
    {
      Player player = Game.Instance.AssetManager.InstantiatePrefab(_playerPrefabKey, transform.position, transform.rotation).GetComponent<Player>();
      
      if (player != null)
      {
        if (_parentSpawnedObject)
          player.transform.parent = transform.parent;

        Game.Instance.GameManager.RegisterPlayer(player);
      }

      _spawned = true;
    }
  }
}
