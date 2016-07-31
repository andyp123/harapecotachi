using UnityEngine;
using System.Collections;

public class TowerTest : Tower
{
  public GameObject _towerTop;
  public float _rotationSpeed = 60f; // degrees per second

  public float _shotDamage = 1f;
  public float _shotDelay = 1f; // delay between shots
  private float _nextAttackEnableTime = 0f;

  private Sensor _sensor = null;
  private GameObject _target;

  void Awake ()
  {
    _sensor = gameObject.GetComponentInChildren<Sensor>();
  }

  void TrackTarget ()
  {
    if (_target != null)
    {
      Vector3 targetDir = _target.transform.position - transform.position;
      targetDir.y = 0;
      Quaternion targetRotation = Quaternion.LookRotation(targetDir);
      _towerTop.transform.rotation = Quaternion.RotateTowards(_towerTop.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
  }

  void TryAttackTarget ()
  {
    if (_target != null && Time.time >= _nextAttackEnableTime)
    {
      Damage damage = _target.GetComponent<Damage>();
      if (damage != null)
      {
        damage.ApplyDamage(_shotDamage);
        _nextAttackEnableTime = Time.time + _shotDelay;
      }
    }
  }

  void Update()
  {
    if (_target == null)
    {
      _target = _sensor.GetNearestTarget();
    }
    else
    {
      if (_sensor.IsTracked(_target))
      {
        TrackTarget();
        TryAttackTarget();
      }
      else
      {
        _target = null;
      }
    }
  }
}
