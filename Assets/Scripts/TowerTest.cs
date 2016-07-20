using UnityEngine;
using System.Collections;

public class TowerTest : Tower
{
  public float _rotationSpeed = 60f; // degrees per second
  public GameObject _towerTop;

  private Sensor _sensor = null;
  private GameObject _target;

  void Awake ()
  {
    _sensor = gameObject.GetComponentInChildren<Sensor>();
  }

  void TrackTarget ()
  {
    Vector3 targetDir = _target.transform.position - transform.position;
    targetDir.y = 0;
    Quaternion targetRotation = Quaternion.LookRotation(targetDir);
    _towerTop.transform.rotation = Quaternion.RotateTowards(_towerTop.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
  }

  void Update()
  {
    if (_target == null)
    {
      _target = _sensor.GetNearestTarget();
    }
    else
    {
      if (_sensor.IsTracked(_target))
        TrackTarget();
      else
        _target = null;
    }
  }
}
