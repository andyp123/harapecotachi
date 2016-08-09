using UnityEngine;
using System.Collections;

// TODO: what really belongs in this class? :)
public class GameManager : MonoBehaviour
{
  GameData.Data<int> _chances;

  void Awake ()
  {
    _chances = GameData.GetIntData("VAL_CHANCES");
    _chances.RegisterOnChange( (v) => {
        if (v == 0)
        {
          SetGameOver();
          _chances.ReadOnly = true;
        }
      });
  }

  public void SetGameOver ()
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
    Debug.Log("Game Over!");
  }
}
