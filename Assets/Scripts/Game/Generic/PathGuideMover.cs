using UnityEngine;
using System.Collections;

public class PathGuideMover : PathMover
{
  public float _maxSeparationDistance = 1f;
  public float _minSeparationDistance = 0.5f;
  public bool _waitOnSeparated = true;

  // the moving object follows a guide object instead of snapping to the path
  // note that _distance now applies to the guide object's distance, not the follower
  Vector3 _guidePosition = Vector3.zero;

  void Start ()
  {
    _distance = _maxSeparationDistance;
    if (_path != null)
    {
      transform.position = _path.GetPositionAtDistance(0f);
      _guidePosition = _path.GetPositionAtDistance(_maxSeparationDistance);
    }
  }

  void Update ()
  {
    if (_path != null)
    {
      float sqrMinSeparation = _minSeparationDistance * _minSeparationDistance;
      float sqrMaxSeparation = _maxSeparationDistance * _maxSeparationDistance;
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

  // this is approximate, as the object following the guide is not following the curve exactly, so this will not be accurate if:
  // the separation distance is large (looser curve following means larger corners can be cut)
  // the object position changed drastically due to another event
  public override Vector3 GetPositionAfterTime (float time)
  {
    Vector3 diff = _guidePosition - transform.position;
    float separation = diff.magnitude;
    float distanceAfterTime = _speed * time;

    // TODO: check this is ok, then merge first two cases
    if (separation < _maxSeparationDistance) // moving along path behind guide (probably!)
    {
      return _path.GetPositionAtDistance(_distance + distanceAfterTime - separation);
    }
    if (separation < distanceAfterTime) // separated, but will be back on path after time
    {
      return _path.GetPositionAtDistance(_distance + distanceAfterTime - separation);
    }
    // separated and will not reach path before time, so assume linear toward guide
    return transform.position + diff.normalized * distanceAfterTime;
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
