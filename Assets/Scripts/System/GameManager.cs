using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
  protected GameManager () {}

  public NamedPrefab[] _prefabs;

  private PrefabMap _prefabMap;


  void Awake ()
  {
    DontDestroyOnLoad(this.gameObject);
    _prefabMap = new PrefabMap();
    _prefabMap.AddPrefabs(_prefabs);

    SceneManager.LoadScene(1); // load Main scene, which should be at position 1 in the build list
  }

  void Update ()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      Restart ();
    }
    if (Input.GetKeyDown(KeyCode.P))
    {
      TogglePause ();
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
      GUIManager.Instance.ToggleLanguage();
    }
  }

  public GameObject InstantiatePrefab (string prefabName, Vector3 position, Quaternion rotation)
  {
    return _prefabMap.InstantiatePrefab(prefabName, position, rotation);
  }

  // TODO: want a proper pause implementation. This is kind of weird
  public void TogglePause ()
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }

  // TODO: totally broken. Fix
  public void Restart ()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
