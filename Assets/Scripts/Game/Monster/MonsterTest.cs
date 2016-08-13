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
    if (_damage.Dead)
    {
      Die();
    }
    if (_pathMover.AtPathEnd())
    {
      Destroy(gameObject);
      GameData.Data<int> chances = GameData.GetIntData("CHANCES");
      if (chances != null)
        chances.Value--;
    }
  }

  public override Vector3 GetPositionAfterTime (float time)
  {
    return _pathMover.GetPositionAfterTime (time);
  }
}
