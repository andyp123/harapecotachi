using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game : Singleton<Game>
{
  protected Game () {}

  GameManager _gameManager;
  GUIManager _guiManager;
  // prefabs / instance manager / pool
  // messaging system
  // stagedata ... part of game manager?

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

    GameObject go;

    go = new GameObject("GUIManager");
    go.transform.parent = transform;
    _guiManager = go.AddComponent<GUIManager>();

    go = new GameObject("GameManager");
    go.transform.parent = transform;
    _gameManager = go.AddComponent<GameManager>();
    // _gameManager.LoadAssets(); // or something

    //SceneManager.LoadScene(1); // load scene the main scene (build index 1)
  }
}
