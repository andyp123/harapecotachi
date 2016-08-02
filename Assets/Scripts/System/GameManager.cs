using UnityEngine;
using System.Collections;

// TODO: what really belongs in this class? :)
public class GameManager : MonoBehaviour
{
  // TODO: want a proper pause implementation. This is kind of weird
  //   also, Game should probably deal with pausing, or have a PauseManager class
  public void TogglePause ()
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }
}
