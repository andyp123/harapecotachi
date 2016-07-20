using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
  public float _health;

  private float _healthMax;

  public bool Dead
  {
    get { return _health <= 0f; }
  }

  void Awake ()
  {
    _healthMax = _health;
  }

  public float ApplyDamage (float damage)
  {
    float oldHealth = _health;

    _health = Mathf.Clamp(_health - damage, 0f, _healthMax);

    return oldHealth - _health;
  }
}
