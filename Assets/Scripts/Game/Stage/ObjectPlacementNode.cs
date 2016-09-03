using UnityEngine;
using System.Collections;

 // TODO: clean this up with something better :D
public class ObjectPlacementNode : MonoBehaviour
{
  public bool _occupied = false;
  public GameObject _visibleObjectRoot;

  public void PlaceObject ()
  {
    if (_occupied)
      return;

    _occupied = true;
    if (_visibleObjectRoot != null)
    {
      _visibleObjectRoot.SetActive(false);
    }
  }
}
