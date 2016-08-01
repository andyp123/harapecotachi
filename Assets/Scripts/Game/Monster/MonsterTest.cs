using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathMover))]
public class MonsterTest : Monster
{
  private PathMover _pathMover;
  private Damage _damage;

  void Awake ()
  {
    _pathMover = gameObject.GetComponent<PathMover>();
    _damage = gameObject.GetComponent<Damage>();
  }

  void Update()
  {
    if (_damage.Dead || _pathMover.AtPathEnd())
    {
      Destroy(gameObject);
    }
  }
}
