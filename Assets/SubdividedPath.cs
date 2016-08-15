using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubdividedPath : MonoBehaviour
{
  public struct PathPoint
  {
    public Vector3 position;
    public Vector3 tangent;
    public Quaternion rotation;
  }

  public Path _path;
  public float _subdivisionSize = 1f; // will get changed slightly through initialization

  List<SubdividedPath.PathPoint> _points;
  float _length = 0f; // will be the same as the original path length

  bool _initialized = false;

  void Awake ()
  {
    StartCoroutine(Initialize());
  }

  // TODO: will need a better system than this!
  IEnumerator Initialize ()
  {
    yield return new WaitForSeconds(1f);

    if (_path != null)
    {
      _points = _path.GetEvenlySubdividedPath(_subdivisionSize);
      if (_points != null)
      {
        _length = _path.Length;
        _subdivisionSize = (_points[1].position - _points[0].position).magnitude; // get actual subdivisionSize
        _initialized = true;
      }
    }
    yield return null;
  }

  public PathPoint GetInterpolatedPointAtDistance (float distance)
  {
    if (_initialized)
      debug.LogError("[SubdividedPath] Cannot get point from a path that has not been initialized.");

    distance = Mathf.Clamp(distance, 0f, _length);
    if (distance == 0f)
      return _points[0]; // TODO: check this is returned by value (copy)

    int maxIndex = _points.Count - 1;
    int index = (int)Mathf.Floor(distance / _length * maxIndex);
    if (index == maxIndex)
      return _points[maxIndex];
    float t = distance - index * (_length / maxIndex); // 0-1

    PathPoint a = _points[index];
    PathPoint b = _points[index + 1];

    PathPoint p = new PathPoint();
    p.position = Vector3.Lerp(a.position, b.position, t);
    p.rotation = Quaternion.Lerp(a.rotation, b.rotation, t);
    p.tangent = p.rotation * Vector3.forward;

    return p;
  }

  // public Vector3 GetInpterpolatedPositionAtDistance (float distance)
  // {
  //   return Vector3.zero;
  // }

  void OnDrawGizmos ()
  {
    if (_initialized)
    {
      Gizmos.color = Color.red;
      float scale = _subdivisionSize * 0.5f;
      foreach (PathPoint p in _points)
      {
        Gizmos.DrawLine(p.position, p.position + p.tangent * scale);
      }
    }
  }
}
