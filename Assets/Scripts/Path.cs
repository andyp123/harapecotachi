using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

  // save a small amount of uneccessary calculation for each point along a segment
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
  // p0, p1 : start / end point
  // tangent0, tangent1 : start / end tangent
  public Vector3 HermiteInterpolate (Vector3 p0, Vector3 p1, Vector3 tangent0, Vector3 tangent1, float t)
  { 
    float t2 = t * t;
    float t3 = t * t2;
  
    Vector3 point;
    point  = p0 * ( 2f * t3 - 3f * t2 + 1f);
    point += p1 * (-2f * t3 + 3f * t2);
    point += tangent0 * (t3 - 2f * t2 + t);
    point += tangent1 * (t3 - t2);

    return point;
  }

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
}
