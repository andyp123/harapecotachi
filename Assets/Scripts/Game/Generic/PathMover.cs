using UnityEngine;
using System.Collections;

public class PathMover : MonoBehaviour
{
  public Path _path;
  public float _speed = 5f;

  protected float _distance = 0f;

  void Start ()
  {
    if (_path != null)
      transform.position = _path.GetPositionAtDistance(0f);
  }

  void Update ()
  {
    if (_path != null)
    {
      _distance += _speed * Time.deltaTime;
      if (_distance > _path.Length)
        _distance = _path.Length;

      Vector3 pos;
      Vector3 dir;
      _path.GetPositionAndTangentAtDistance(_distance, out pos, out dir);
      transform.position = pos;
      transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
  }

  // for predicting where a monster will be so towers can shoot them accurately
  public virtual Vector3 GetPositionAfterTime (float time)
  {
    if (_path != null)
    {
      float distance = _distance + _speed * time;
      return _path.GetPositionAtDistance(distance);
    }

    return Vector3.zero;
  }

  public virtual bool AtPathEnd ()
  {
    if (_path != null)
      return (_distance >= _path.Length);
    return false;
  }
}
