using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Monster : MonoBehaviour
{
  public abstract Vector3 GetPositionAfterTime (float seconds);

  protected virtual void Die ()
  {
    GameData.Data<int> intData = GameData.GetIntData("VAL_KILLED");
    if (intData != null)
      intData.Value += 1;
    Destroy (gameObject);
  }
}
