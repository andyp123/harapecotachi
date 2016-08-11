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
          SetGameOver();
      });
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
