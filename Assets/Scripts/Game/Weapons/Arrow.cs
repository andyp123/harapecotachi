using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
  public float _damage = 1f;

  void OnTriggerEnter (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    Damage damage = go.GetComponent<Damage>();

    if (damage != null)
    {
      DamageInfo info = new DamageInfo();
      info.baseDamage = _damage;
      info.knockbackForce = _damage;
      info.knockbackDir = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
      info.damageType = DamageType.Ballistic;

      damage.ApplyDamage(info);
      // Debug.Log("Arrow hit target!");
    }
    // else
    //   Debug.Log(string.Format("Arrow missed target and hit {0}!", go.name));

    Destroy(this.gameObject);
  }
}
