using UnityEngine;
using System.Collections;


public class TowerBallistic : Tower
{
  public GameObject _towerTop;
  public GameObject _weaponPrefab;
  public float _rotationSpeed = 60f; // degrees per second
  public float _shotArcHeightOffset = 0f;

  public float _shotDamage = 1f; // TODO: this kind of data should be elsewhere
  public float _shotDelay = 1f; // delay between shots
  public float _shotTimeToTarget = 1f; // how long a shot takes to reach its target
  public float _shotInaccuracy = 0f;


  static float _angleThreshold = 5f; // turret aiming direction must be within this amount of the angle to target

  private Sensor _sensor = null;

  Vector3 _attackPosition = Vector3.zero;

  void Awake ()
  {
    _rangeIndicator.transform.localScale = Vector3.one * _range;
    _sensor = gameObject.GetComponentInChildren<Sensor>();
    if (_sensor == null)
      Debug.LogError(string.Format("[TowerBallistic] Ballistic tower '{0}' is missing a sensor.", gameObject.name));
  }

  void TryAquireTarget ()
  {
    float sqrRange = _range * _range;
    TargetInfo[] targetInfos = _sensor.GetNearestTargets(); // these are already sorted by distance from tower
    foreach (TargetInfo info in targetInfos)
    {
      if (info.target == null) // TODO: is this possible? should it be impossible?
        continue;
      Monster monster = info.target.GetComponent<Monster>();
      if (monster)
      {
        Vector3 futurePosition = monster.GetPositionAfterTime(_shotTimeToTarget);
        float sqrDistance = (futurePosition - transform.position).sqrMagnitude;
        if (sqrDistance < sqrRange)
        {
          _target = info.target;
          break;
        }
      }
    }
  }

  void TrackTarget ()
  {
    if (_target != null)
    {
      _attackPosition = _target.GetComponent<Monster>().GetPositionAfterTime(_shotTimeToTarget);

      if ((_attackPosition - transform.position).sqrMagnitude < _range * _range)
      {
        Vector3 targetDir = _attackPosition - transform.position;
        targetDir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        _towerTop.transform.rotation = Quaternion.RotateTowards(_towerTop.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        // check if the tower is aimed here, since we already calculated the rotations
        _attackIsAimed = (Quaternion.Angle(targetRotation, _towerTop.transform.rotation) < _angleThreshold);
      }
      else
        _target = null; // need to reaquire target, since the current one is not in range
    }
  }

  void AttackTarget ()
  {
    Vector3 shotStartPosition = transform.position + Vector3.up * 2f;
    Vector3 attackPosition = _attackPosition + Vector3.up * 0.5f;
    attackPosition += new Vector3(Random.value, 0, Random.value) * _shotInaccuracy;

    GameObject weapon = GameObject.Instantiate(_weaponPrefab, shotStartPosition, _towerTop.transform.rotation) as GameObject;
    BallisticPhysics physics = weapon.gameObject.GetComponent<BallisticPhysics>();
    if (physics != null)
    {
      float maxHeight = shotStartPosition.y + _shotArcHeightOffset;
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

  void Update()
  {
    if (_target == null)
    {
      TryAquireTarget();
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
