using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Monster : MonoBehaviour
{
  public int money = 0; // set in Wave.cs on spawn

  public abstract Vector3 GetPositionAfterTime (float seconds);

  protected virtual void Die ()
  {
    if (money > 0)
    {
      Game.Instance.GameManager.DropItems(money, transform.position, 5f);
    }

    GameData.Data<int> intData = GameData.GetIntData("VAL_KILLED");
    if (intData != null)
      intData.Value += 1;
    Destroy (gameObject);
  }
}
