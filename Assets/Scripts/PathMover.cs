using UnityEngine;
using System.Collections;

public class PathMover : MonoBehaviour
{
  public Path _path;
  public float _speed = 5f;

  private float _distance = 0f;

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
        
      Vector3 pos = _path.GetPositionAtDistance(_distance);
      Vector3 prevPos = transform.position;
      transform.position = pos;

      if (pos != prevPos)
      {
        Vector3 dir = pos - prevPos;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
      }
    }
  }

  public bool AtPathEnd ()
  {
    if (_path != null)
      return (_distance >= _path.Length);
    return false;
  }
}
