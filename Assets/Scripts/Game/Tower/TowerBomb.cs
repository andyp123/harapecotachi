using UnityEngine;
using System.Collections;

public class TowerBomb : Tower
{
  public GameObject _towerTop;
  public GameObject _bombPrefab;
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
      Vector3 attackPosition = _target.transform.position + new Vector3(Random.value, 0f, Random.value);
      GameObject.Instantiate(_bombPrefab, attackPosition, Quaternion.identity);
      _nextAttackEnableTime = Time.time + _shotDelay;
    }
  }

  void Update()
  {
    if (_target == null)
    {
      TargetInfo targetInfo = _sensor.GetNearestTarget();
      _target = (targetInfo != null) ? targetInfo.target : null;
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
