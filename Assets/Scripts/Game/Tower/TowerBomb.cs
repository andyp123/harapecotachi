using UnityEngine;
using System.Collections;

public class TowerBomb : Tower
{

  public GameObject _towerTop;
  public GameObject _bombPrefab;
  public float _rotationSpeed = 60f; // degrees per second

  public float _shotDamage = 1f; // TODO: this kind of data should be elsewhere
  public float _shotDelay = 1f; // delay between shots
  public float _shotTimeToTarget = 1f; // how long a shot takes to reach its target
  public float _shotInaccuracy = 0f;

  static float _angleThreshold = 5f; // turret aiming direction must be within this amount of the angle to target

  Sensor _sensor = null;
  GameObject _target;
  float _nextAttackEnableTime = 0f;
  Vector3 _attackPosition = Vector3.zero;
  bool _attackIsAimed = false;


  void Awake ()
  {
    _sensor = gameObject.GetComponentInChildren<Sensor>();
  }

  void TrackTarget ()
  {
    if (_target != null)
    {
      _attackPosition = _target.GetComponent<Monster>().GetPositionAfterTime(_shotTimeToTarget);
      Vector3 targetDir = _attackPosition - transform.position;
      targetDir.y = 0;
      Quaternion targetRotation = Quaternion.LookRotation(targetDir);
      _towerTop.transform.rotation = Quaternion.RotateTowards(_towerTop.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

      // check if the attack is aimed here, since we already calculated the rotations
      _attackIsAimed = (Quaternion.Angle(targetRotation, _towerTop.transform.rotation) < _angleThreshold);
    }
  }

  bool ReadyToAttack ()
  {
    if (_target != null && Time.time >= _nextAttackEnableTime && _attackIsAimed)
      return true;

    return false;
  }

  void AttackTarget ()
  {
    Vector3 shotStartPosition = transform.position + Vector3.up * 2f;
    Vector3 attackPosition = _attackPosition;
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

  void Update ()
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
        if (ReadyToAttack()) 
          AttackTarget();
      }
      else
      {
        _target = null;
      }
    }
  }

  void OnDrawGizmos ()
  {
    if (_target != null)
    {
      Debug.DrawLine(transform.position, _target.transform.position, Color.gray);
      if (_attackIsAimed)
        Debug.DrawLine(transform.position, _attackPosition, Color.red);
    }
  }
}
