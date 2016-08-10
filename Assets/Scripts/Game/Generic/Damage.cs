using UnityEngine;
using System.Collections;

public enum DamageType
{
  Explosive,
  Ballistic,
  Fire,
  Ice,
  Electrical
}

public class DamageInfo
{
  // originator (e.g player, or some tower)
  // type of damage (ballistic, fire, freeze, electrical, etc.)
  // knock back direction
  // knock back force
  // shield break
  // damage amount
  public float baseDamage;
  public float knockbackForce;
  public Vector3 knockbackDir;
  public DamageType damageType;
  public GameObject originator;
}

public class Damage : MonoBehaviour
{
  public float _health;

  private float _healthMax;
  private SimplePhysics _physics = null;

  public bool Dead
  {
    get { return _health <= 0f; }
  }

  void Awake ()
  {
    _physics = gameObject.GetComponent<SimplePhysics>();
    _healthMax = _health;
  }

  public void ApplyDamage (DamageInfo info)
  {
    _health = Mathf.Clamp(_health - info.baseDamage, 0f, _healthMax);

    if (info.knockbackForce > 0f && _physics != null)
    {
      _physics.AddForce(info.knockbackDir * info.knockbackForce);
    }
  }
}
