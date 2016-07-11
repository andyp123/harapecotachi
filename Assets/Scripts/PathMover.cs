using UnityEngine;
using System.Collections;

public class PathMover : MonoBehaviour
{
  public Path _path;
  public float _speed = 5f;

  private float _distance = 0f;

  void Update ()
  {
    if (_path != null)
    {
      _distance += _speed * Time.deltaTime;
      if (_distance > _path.Length)
        _distance = _path.Length;
        
      Vector3 pos = _path.GetPositionAtDistance(_distance);
      transform.position = pos;
    }
  }
}
