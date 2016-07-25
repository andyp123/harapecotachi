using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleSensor : Sensor
{
  [SerializeField]
  private float _sensorRadius = 1f;
  private float _sensorHeight = 8f;

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

  // this sets the capsule collider's size so that it can be used more like a cylinder collider
  protected override void SetColliderSize ()
  {
    CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
    cc.center = new Vector3(0f, _sensorHeight * 0.5f, 0f);
    cc.height = _sensorHeight + _sensorRadius * 2f;
    cc.radius = _sensorRadius;
  }
}