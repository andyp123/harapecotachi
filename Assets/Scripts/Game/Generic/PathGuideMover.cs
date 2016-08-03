using UnityEngine;
using System.Collections;

public class PathGuideMover : PathMover
{
  public float _separationDistance = 1f;
  public bool _waitOnSeparated = true;

  Vector3 _guidePosition = Vector3.zero;

  void Start ()
  {
    _distance = _separationDistance;
    if (_path != null)
    {
      transform.position = _path.GetPositionAtDistance(0f);
      _guidePosition = _path.GetPositionAtDistance(_separationDistance);
    }
  }

  void Update ()
  {
    if (_path != null)
    {
      // update guide if within range
      if (_waitOnSeparated && (transform.position - _guidePosition).sqrMagnitude < _separationDistance * _separationDistance)
      {
        _distance = Mathf.Clamp(_distance + _speed * Time.deltaTime, 0f, _path.Length);
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
