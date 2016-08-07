using UnityEngine;
using System.Collections;

public class DamageInfo
{
  // originator (e.g player, or some tower)
  // hittype flags (e.g. can hit ground, air, mix)
  // type of damage (ballistic, fire, freeze, electrical, etc.)
  // damage location or direction
  // knock back force
  // shield break
  // damage amount
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

  public float ApplyDamage (float damage, Vector3 dir)
  {
    float damageTaken = _health;
    _health = Mathf.Clamp(_health - damage, 0f, _healthMax);
    damageTaken = damageTaken - _health;

    if (_physics)
    {
      _physics.AddForce(dir * damageTaken * Random.Range(1f, 2f));
    }

    return damageTaken;
  }

  public float ApplyDamage (float damage)
  {
    float damageTaken = _health;
    _health = Mathf.Clamp(_health - damage, 0f, _healthMax);
    damageTaken = damageTaken - _health;

    return damageTaken;
  }
}
