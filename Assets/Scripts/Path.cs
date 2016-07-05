using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Path : MonoBehaviour
{
  [HideInInspector][SerializeField]
  private List<Vector3> _points;
  private float _length = 0f;
  private bool _lengthInvalid = false;

  void Reset()
  {
    _points = new List<Vector3>();
  }

  public float Length
  {
    get
    {
      if (_lengthInvalid)
        CalculateLength();
      return _length;
    }
  }
  public int NumPoints
  {
    get { return _points.Count; }
  }

  public Vector3 GetPoint(int index)
  {
    return _points[index];
  }

  public void SetPoint(int index, Vector3 point)
  {
    _points[index] = point;
    _lengthInvalid = true;
  }

  public void AddPoint(Vector3 point)
  {
    _points.Add(point);
    _lengthInvalid = true;
  }

  public void InsertPoint(int index, Vector3 point)
  {
    _points.Insert(index, point);
    _lengthInvalid = true;
  }

  public void RemovePoint(int index)
  {
    _points.RemoveAt(index);
    _lengthInvalid = true;
  }

  private void CalculateLength()
  {
    _length = 0f;

    if (_points.Count < 2)
      return;

    Vector3 p0 = _points[0];
    for(int i = 1; i < _points.Count; ++i)
    {
      Vector3 p1 = _points[i];
      _length += (p1 - p0).magnitude;
    }
  }

}
