using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TargetInfo
{
  public GameObject target;
  public float sqrDistance;

  public TargetInfo ()
  {
    target = null;
    sqrDistance = -1f;
  }

  public TargetInfo (GameObject target, float sqrDistance)
  {
    this.target = target;
    this.sqrDistance = sqrDistance;
  }
}


[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
  protected List<TargetInfo> _targets;

  protected virtual void SetColliderSize () {}

  void Awake ()
  {
    SetColliderSize();
    _targets = new List<TargetInfo>(10);
  }

  void Update ()
  {
    // remove invalid targets (destroyed etc.) and update distance on valid ones
    for (int i = 0; i < _targets.Count; ++i)
    {
      TargetInfo t = _targets[i];
      if (t.target == null)
      {
        _targets.RemoveAt(i);
        --i;
      }
      else
        t.sqrDistance = (t.target.transform.position - transform.position).sqrMagnitude;
    }

    // sort the list of targets by distance from the sensor center
    _targets.Sort( (a, b) => {
      return a.sqrDistance.CompareTo(b.sqrDistance);
      });
  }

  void OnTriggerEnter (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    if (!IsTracked(go))
    {
      float sqrDistance = (go.transform.position - transform.position).sqrMagnitude;
      int index = 0;
      foreach (TargetInfo t in _targets)
      {
        if (t.sqrDistance > sqrDistance)
          break;
        index++;
      }

      _targets.Insert(index, new TargetInfo(go, sqrDistance));
    }
  }

  void OnTriggerExit (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    int index = GetIndex(go);
    if (index >= 0)
      _targets.RemoveAt(index);
  }

  int GetIndex (GameObject go)
  {
    int instanceID = go.GetInstanceID();

    for (int i = 0; i < _targets.Count; ++i)
    {
      if (_targets[i].target.GetInstanceID() == instanceID)
        return i;
    }

    return -1;
  }

  public TargetInfo GetNearestTarget ()
  {
    return (_targets.Count > 0) ? _targets[0] : null;
  }

  public TargetInfo[] GetNearestTargets (int maxTargets = -1)
  {
    int arrayLength = (maxTargets != -1 && maxTargets < _targets.Count) ? maxTargets : _targets.Count;
    TargetInfo[] targets = new TargetInfo[arrayLength];

    for (int i = 0; i < arrayLength; ++i)
    {
      targets[i] = _targets[i];
    }

    return targets;
  }

  public bool IsTracked (GameObject go)
  {
    int instanceID = go.GetInstanceID();

    foreach (TargetInfo t in _targets)
    {
      if (t.target.GetInstanceID() == instanceID)
        return true;
    }

    return false;
  }
}