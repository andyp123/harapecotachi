using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game : Singleton<Game>
{
  protected Game () {}

  GameManager _gameManager;
  GUIManager _guiManager;
  // prefabs / instance manager

  public GameManager GameManager
  {
    get { return _gameManager; }
  }
  public GUIManager GUIManager
  {
    get { return _guiManager; }
  }

  void Awake ()
  {
    DontDestroyOnLoad(this.gameObject);

    Localization.LoadLocalizationFile();

    _guiManager = new GUIManager();
    _gameManager = new GameManager();

    SceneManager.LoadScene(1); // load scene the main scene (build index 1)
  }
}
