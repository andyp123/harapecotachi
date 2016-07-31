using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceTable
{
  private int _subdivisions = 10;
  private List<float> _distances;
  private bool _isValid = false;

  public DistanceTable ()
  {
    _distances = new List<float>();
  }

  public float Length
  {
    get { return (_distances.Count < 2) ? 0f : _distances[_distances.Count - 1]; }
  }
  public bool IsValid
  {
    get { return _isValid; }
  }

  public void Invalidate()
  {
    _isValid = false;
  }

  public float GetT(float distance, int startIndex = 0)
  {
    if (!_isValid)
      return 0f;

    if (distance <= 0f)
      return 0f;
    else if (distance >= _distances[_distances.Count - 1])
      return 1f;

    // find the first index where _distances[i] is greater than distance
    int i = Mathf.Clamp(startIndex, 0, _distances.Count - 1);
    while (_distances[i] < distance)
      ++i;

    // correct t value is between i-1 and i, so interpolate
    int start = (i > 0) ? i - 1 : 0;
    int end = (i >= _distances.Count) ? _distances.Count - 1 : i;
    if (start == end)
      return _distances[end];

    float fraction = 1f - (_distances[end] - distance) / (_distances[end] - _distances[start]);
    float t = (start + fraction) / (_distances.Count - 1);

    return t;
  }

  public void Initialize(Path path, int segmentIndex)
  {
    if (_subdivisions < 1)
      return;

    Vector3[] interpolateInput = path.GetInterpolateInput(segmentIndex);
    if (interpolateInput != null)
    {
      _distances = new List<float>(_subdivisions + 1);
      float tIncrement = 1f / _subdivisions;
      float distance = 0f;

      _distances.Add(0f);

      Vector3 a = path.HermiteInterpolate(interpolateInput, 0f);
      for (int i = 1; i <= _subdivisions; ++i)
      {
        Vector3 b = path.HermiteInterpolate(interpolateInput, tIncrement * i);
        distance += (b - a).magnitude;
        _distances.Add(distance);
        a = b;
      }

      _isValid = true;
    }
  }
}
