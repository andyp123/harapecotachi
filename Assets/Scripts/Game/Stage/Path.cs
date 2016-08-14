using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Might it be faster to use the matrix form for hermite interpolation: http://cubic.org/docs/hermite.htm

[System.Serializable]
public class Path : MonoBehaviour
{
  [SerializeField]
  private List<Vector3> _points;
  [SerializeField]
  private float _tension = 0f;
  private float _length = 0f;
  private bool _lengthValid = false;

  private List<DistanceTable> _distanceTables;

  public float Tension
  {
    get { return _tension; }
    set
    {
      _tension = Mathf.Clamp(value, -1f, 1f);
      InvalidateAllDistanceTables();
    }
  }
  public float Length
  {
    get
    {
      if (!_lengthValid)
        CalculateLength();
      return _length;
    }
  }
  public int NumPoints
  {
    get { return _points.Count; }
  }

  public void EditorInitialize ()
  {
    // initialize non-serialized data
    InitializeDistanceTables();
  }

  void Reset ()
  {
    _points = new List<Vector3>();
    _distanceTables = new List<DistanceTable>();
  }

  void Awake ()
  {
    InitializeDistanceTables();
  }

  public Vector3 GetPoint (int index)
  {
    return _points[index];
  }

  public void SetPoint (int index, Vector3 point)
  {
    if (_points[index] != point)
    {
      _points[index] = point;
      InvalidateDistanceTables(index);
    }
  }

  public void AddPoint (Vector3 point)
  {
    _points.Add(point);
    _distanceTables.Add(new DistanceTable());
    InvalidateDistanceTables(_points.Count - 1);
  }

  public void InsertPoint (int index, Vector3 point)
  {
    _points.Insert(index, point);
    _distanceTables.Insert(index, new DistanceTable());
    InvalidateDistanceTables(index);
  }

  public void RemovePoint (int index)
  {
    _points.RemoveAt(index);
    _distanceTables.RemoveAt(index);
    if (index != 0)
      InvalidateDistanceTables(index - 1, false);
  }

  private void CalculateLength ()
  {
    _length = 0f;

    if (_points.Count < 2)
      return;

    for (int i = 0; i < _points.Count - 1; ++i)
    {
      DistanceTable dt = _distanceTables[i];
      if (!dt.IsValid)
        dt.Initialize(this, i);
      _length += dt.Length;
    }

    _lengthValid = true;
  }

  public Vector3 GetPositionAtDistance(float distance)
  {
    if (_points.Count < 1)
      return Vector3.zero;
    if (_points.Count < 2)
      return _points[0];

    float totalLength = 0f;

    for (int i = 0; i < _points.Count; ++i)
    {
      float sectionLength = _distanceTables[i].Length;
      if (totalLength + sectionLength > distance)
      {
        float t = _distanceTables[i].GetT(distance - totalLength);
        Vector3[] interpolateInput = GetInterpolateInput(i);
        return HermiteInterpolate(interpolateInput, t);
      }

      totalLength += sectionLength;
    }

    return _points[_points.Count - 1];
  }

  public Vector3 GetTangentAtDistance(float distance)
  {
    if (_points.Count < 2)
      return Vector3.forward;

    float totalLength = 0f;

    for (int i = 0; i < _points.Count; ++i)
    {
      float sectionLength = _distanceTables[i].Length;
      if (totalLength + sectionLength > distance)
      {
        float t = _distanceTables[i].GetT(distance - totalLength);
        Vector3[] interpolateInput = GetInterpolateInput(i);
        return HermiteTangent(interpolateInput, t);
      }

      totalLength += sectionLength;
    }

    return (_points[_points.Count-1] - _points[_points.Count-2]).normalized;
  }

  public void GetPositionAndTangentAtDistance(float distance, out Vector3 position, out Vector3 tangent)
  {
    position = Vector3.zero;
    tangent = Vector3.forward;

    if (_points.Count < 2)
      return;

    float totalLength = 0f;

    for (int i = 0; i < _points.Count; ++i)
    {
      float sectionLength = _distanceTables[i].Length;
      if (totalLength + sectionLength > distance)
      {
        float t = _distanceTables[i].GetT(distance - totalLength);
        Vector3[] interpolateInput = GetInterpolateInput(i);
        position = HermiteInterpolate(interpolateInput, t);
        tangent = HermiteTangent(interpolateInput, t);
        return;
      }

      totalLength += sectionLength;
    }

    // distance should be the same as curve length
    position = _points[_points.Count - 1];
    tangent = (_points[_points.Count-1] - _points[_points.Count-2]).normalized;
  }

  // save a small amount of uneccessary calculation for each point along a segment
  // p0, p1 : start / end point
  // tangent0, tangent1 : start / end tangent
  public Vector3[] GetInterpolateInput (int segmentIndex)
  {
    int i = segmentIndex;
    Vector3 p0 = _points[i];
    Vector3 p1 = _points[i + 1];
    Vector3 tangent0, tangent1;
    tangent0 = (i == 0) ? p1 - p0 : p1 - _points[i - 1];
    tangent1 = (i == _points.Count - 2) ? p1 - p0 : _points[i + 2] - p0;

    float tension = (1f - _tension) * 0.5f;
    tangent0 *= tension;
    tangent1 *= tension;

    return new Vector3[] {p0, p1, tangent0, tangent1};
  }

  // t : 0-1 position along curve
  // interpolate input should have same format as input to above (p0, p1, tangent0, tangent1)
  public Vector3 HermiteInterpolate (Vector3[] interpolateInput, float t)
  {
    float t2 = t * t;
    float t3 = t * t2;

    Vector3 point;
    point  = interpolateInput[0] * ( 2f * t3 - 3f * t2 + 1f);
    point += interpolateInput[1] * (-2f * t3 + 3f * t2);
    point += interpolateInput[2] * (t3 - 2f * t2 + t);
    point += interpolateInput[3] * (t3 - t2);

    return point;   
  }

  // using the first derivative of the formula to get a point, we can get the tangent
  public Vector3 HermiteTangent (Vector3[] interpolateInput, float t)
  {
    float t2 = t * t;

    Vector3 tangent;
    tangent  = interpolateInput[0] * ( 6f * t2 - 6f * t);
    tangent += interpolateInput[1] * (-6f * t2 + 6f * t);
    tangent += interpolateInput[2] * (3f * t2 - 4f * t + 1f);
    tangent += interpolateInput[3] * (3f * t2 - 2f * t);

    return tangent.normalized;     
  }

  private void InitializeDistanceTables ()
  {
    _distanceTables = new List<DistanceTable>(_points.Count);
    for (int i = 0; i < _points.Count; ++i)
    {
      _distanceTables.Add(new DistanceTable());
    }

    CalculateLength(); // this will update the distance tables
  }

  private void InvalidateDistanceTables (int segmentIndex, bool invalidatePrev = true)
  {
    _distanceTables[segmentIndex].Invalidate();
    // invalidate previous connected segment
    if ( invalidatePrev && segmentIndex > 0)
      _distanceTables[segmentIndex - 1].Invalidate();

    _lengthValid = false;
  }

  private void InvalidateAllDistanceTables ()
  {
    foreach (DistanceTable dt in _distanceTables)
    {
      dt.Invalidate();
    }

    _lengthValid = false;
  }

  void OnDrawGizmos ()
  {
    if (_points.Count < 2)
      return;

    float increment = 1f / 10;

    Gizmos.color = Color.white;
    for (int i = 0; i < _points.Count - 1; ++i)
    {
      Vector3[] interpolateInput = GetInterpolateInput(i);

      Vector3 a = interpolateInput[0];
      for (float t = increment; t < 1f; t += increment)
      {
        Vector3 b = HermiteInterpolate(interpolateInput, t);
        Gizmos.DrawLine(a, b);
        a = b;
      }
      Gizmos.DrawLine(a, interpolateInput[1]);
    }
  }
}
