using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathMover))]
public class MonsterTest : Monster
{
  private PathMover _pathMover;

  void Awake ()
  {
    _pathMover = gameObject.GetComponent<PathMover>();
  }

  void Update()
  {
    if (_pathMover.AtPathEnd())
    {
      Destroy(gameObject);
    }
  }
}
