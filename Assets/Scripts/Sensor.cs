using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour
{
  private Dictionary<int, GameObject> _targets;


  protected virtual void SetColliderSize () {} // only used by CapsuleSensor

  void Awake ()
  {
    _targets = new Dictionary<int, GameObject>();
    SetColliderSize();
  }

  void Update ()
  {
    CleanInvalidTargets();
  }

  void OnTriggerEnter (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    int uid = go.GetInstanceID();
    _targets.Add(uid, go);
  }

  void OnTriggerExit (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    int uid = go.GetInstanceID();
    _targets.Remove(uid);
  }

  // required because gameobjects that are destroyed will not cause OnTriggerExit to fire
  void CleanInvalidTargets ()
  {
    List<int> keysToRemove = new List<int>();

    foreach (var key in _targets.Keys)
    {
      GameObject target = _targets[key];
      if (target == null)
        keysToRemove.Add(key);
    }
    foreach (int key in keysToRemove)
      _targets.Remove(key);
  }

  public GameObject GetNearestTarget ()
  {
    GameObject nearestTarget = null;
    float nearestDistanceSqr = 9999999f;

    foreach (var key in _targets.Keys)
    {
      GameObject target = _targets[key];
      if (target == null) continue; // annoying, but it's possible the object could have been destroyed
      float distanceSqr = (target.transform.position - transform.position).sqrMagnitude;
      if (distanceSqr < nearestDistanceSqr)
      {
        nearestDistanceSqr = distanceSqr;
        nearestTarget = target;
      }
    }

    return nearestTarget;
  }

  public bool IsTracked (GameObject go)
  {
    return _targets.ContainsKey(go.GetInstanceID());
  }
}