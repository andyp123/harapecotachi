using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CapsuleCollider))]
public class Sensor : MonoBehaviour
{
  [SerializeField]
  private float _sensorRadius = 1f;
  private float _sensorHeight = 10f;
  private Dictionary<int, GameObject> _targets;

  public float SensorRadius
  {
    get { return _sensorRadius; }
    set {
      if (value > 0f)
      {
        _sensorRadius = value;
        SetColliderSize();
      }
    }
  }

  void Awake ()
  {
    _targets = new Dictionary<int, GameObject>();

    SetColliderSize();
  }

  void Update ()
  {
    CleanInvalidTargets();
  }

  // this sets the capsule collider's size so that it can be used more like a cylinder collider
  protected void SetColliderSize ()
  {
    CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
    cc.center = new Vector3(0f, _sensorHeight * 0.5f, 0f);
    cc.height = _sensorHeight + _sensorRadius * 2f;
    cc.radius = _sensorRadius;
  }

  void OnTriggerEnter (Collider other)
  {
    Debug.Log("trigger enter");

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
    float nearestDistanceSqr = _sensorRadius * _sensorRadius + 100f;

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