using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: what really belongs in this class? :)
public class GameManager : MonoBehaviour
{
  GameData.Data<int> _chances;
  List<Player> _players;

  void Awake ()
  {
    _players = new List<Player>(2);

    _chances = GameData.GetIntData("VAL_CHANCES");
    _chances.RegisterOnChange( (v) => {
        if (v == 0)
          SetGameOver();
      });
  }

  // should be called on level load
  public void Reset ()
  {
    _players.Clear();
  }

  public void RegisterPlayer (Player player)
  {
    int uid = player.gameObject.GetInstanceID();
    foreach (Player p in _players)
    {
      if (uid == p.GetInstanceID())
      {
        Debug.LogError(string.Format("[GameManager] Player '{0}' is already registered.", player.gameObject.name));
        return;
      }
    }

    _players.Add(player);
    Debug.Log(string.Format("[GameManager] Registered Player {0}.", _players.Count));
  }

  public Player GetNearestPlayer (Vector3 position, float radius)
  {
    Player nearestPlayer = null;
    float sqrNearestDistance = 99999999f;

    foreach (Player p in _players)
    {
      float sqrDistance = (p.transform.position - position).sqrMagnitude;
      if (sqrDistance < sqrNearestDistance)
      {
        nearestPlayer = p;
        sqrNearestDistance = sqrDistance;
      }
    }

    return (sqrNearestDistance < radius * radius) ? nearestPlayer : null;
  }

  public void SetGameOver ()
  {
    // TODO: this is shit, so change it once a better solution is ready
    GameObject ui = GameObject.Find("UI");
    GameObject gameUI = ui.transform.Find("GameUI").gameObject;
    GameObject gameOverUI = ui.transform.Find("GameOverUI").gameObject;

    gameUI.SetActive(false);
    gameOverUI.SetActive(true);

    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }
}
