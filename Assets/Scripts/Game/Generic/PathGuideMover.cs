using UnityEngine;
using System.Collections;

public class PathGuideMover : PathMover
{
  public float _separationDistanceMax = 1f;
  public float _separationDistanceMin = 0.5f;
  public bool _waitOnSeparated = true;

  Vector3 _guidePosition = Vector3.zero;

  void Start ()
  {
    _distance = _separationDistanceMax;
    if (_path != null)
    {
      transform.position = _path.GetPositionAtDistance(0f);
      _guidePosition = _path.GetPositionAtDistance(_separationDistanceMax);
    }
  }

  void Update ()
  {
    if (_path != null)
    {
      float sqrMinSeparation = _separationDistanceMin * _separationDistanceMin;
      float sqrMaxSeparation = _separationDistanceMax * _separationDistanceMax;
      float sqrSeparation = (transform.position - _guidePosition).sqrMagnitude;

      // update guide if within range
      if (_waitOnSeparated && sqrSeparation < sqrMaxSeparation)
      {
        // speedScalar tries to prevent the object catching up and overtaking the guide if hit by a sudden impulse
        float speedScalar = (sqrSeparation < sqrMinSeparation) ? 1f + (sqrMinSeparation - sqrSeparation) / sqrMinSeparation * 5f : 1f;
        _distance = Mathf.Clamp(_distance + _speed * speedScalar * Time.deltaTime, 0f, _path.Length);
        _guidePosition = _path.GetPositionAtDistance(_distance);
      }

      // move towards the guide
      Vector3 dir = (_guidePosition - transform.position).normalized;
      transform.position += dir * _speed * Time.deltaTime;
      transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
  }
  
  public override bool AtPathEnd ()
  {
    if (_path != null)
    {
      if (_distance >= _path.Length)
      {
        if ((transform.position - _guidePosition).sqrMagnitude < 0.05f)
          return true;  
      }
    }
    return false;
  }

  void OnDrawGizmos ()
  {
    Color gizmosColor = Gizmos.color;
    Vector3 o = _guidePosition;
    Gizmos.color = Color.red;
    Gizmos.DrawLine(o - Vector3.right, o + Vector3.right);
    Gizmos.color = Color.green;
    Gizmos.DrawLine(o - Vector3.up, o + Vector3.up);
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(o - Vector3.forward, o + Vector3.forward);
    Gizmos.color = gizmosColor;
  }
}
