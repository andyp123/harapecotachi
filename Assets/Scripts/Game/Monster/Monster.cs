using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Monster : MonoBehaviour
{
  public abstract Vector3 GetPositionAfterTime (float seconds);

  protected virtual void Die ()
  {
    GameData.IntValue intValue = GameData.GetIntValue ("VAL_KILLED");
    if (intValue != null)
      intValue.Value += 1;
    Destroy (gameObject);
  }
}
