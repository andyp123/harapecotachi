using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game : Singleton<Game>
{
  protected Game () {}

  AssetManager _assetManager;
  GUIManager _guiManager;
  GameManager _gameManager;
  // messaging system
  // stagedata ... part of game manager?

  public NamedPrefab[] _namedPrefabs = null;

  public AssetManager AssetManager
  {
    get { return _assetManager; }
  }
  public GUIManager GUIManager
  {
    get { return _guiManager; }
  }
  public GameManager GameManager
  {
    get { return _gameManager; }
  }

  void Awake ()
  {
    Initialize();
  }

  void Initialize ()
  {
    DontDestroyOnLoad(this.gameObject);

    Localization.LoadLocalizationFile();

    GameObject go;

    go = new GameObject("AssetManager");
    go.transform.parent = transform;
    _assetManager = go.AddComponent<AssetManager>();
    if (_namedPrefabs != null)
      _assetManager.LoadAssets(_namedPrefabs);

    go = new GameObject("GUIManager");
    go.transform.parent = transform;
    _guiManager = go.AddComponent<GUIManager>();

    go = new GameObject("GameManager");
    go.transform.parent = transform;
    _gameManager = go.AddComponent<GameManager>();

    if (SceneManager.GetActiveScene().buildIndex == 0)
      SceneManager.LoadScene(1); // load the main scene (build index 1)
  }
}
