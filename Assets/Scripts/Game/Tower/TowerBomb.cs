using UnityEngine;
using System.Collections;

public class TowerBomb : Tower
{
  public GameObject _towerTop;
  public GameObject _bombPrefab;
  public float _rotationSpeed = 60f; // degrees per second

  public float _shotDamage = 1f;
  public float _shotDelay = 1f; // delay between shots
  public float _shotTimeToTarget = 1f; // how long a shot takes to reach its target
  public float _shotInaccuracy = 0f;
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
      Vector3 shotStartPosition = transform.position + Vector3.up * 2f;
      Vector3 attackPosition = _target.GetComponent<Monster>().GetPositionAfterTime(_shotTimeToTarget);
      attackPosition += new Vector3(Random.value, 0, Random.value) * _shotInaccuracy;

      GameObject bomb = GameObject.Instantiate(_bombPrefab, shotStartPosition, Quaternion.identity) as GameObject;
      BallisticPhysics physics = bomb.gameObject.GetComponent<BallisticPhysics>();
      if (physics != null)
      {
        float maxHeight = shotStartPosition.y * 1.5f;
        Vector3 fireVelocity;
        float gravity;
        if (Utility.SolveBallisticArc (shotStartPosition, attackPosition, _shotTimeToTarget, maxHeight, out fireVelocity, out gravity))
        {
          physics._gravity = gravity;
          physics.AddForce(fireVelocity);
        }
      }

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
