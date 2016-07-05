using UnityEngine;
using System.Collections;

public class TowerTest : Tower
{
  public float _rotationSpeed = 60f; // degrees per second
  public GameObject _target;
  public GameObject _towerTop;

  public void TrackTarget()
  {
    if (_target != null)
    {
      Vector3 targetDir = _target.transform.position - transform.position;
      targetDir.y = 0;
      Quaternion targetRotation = Quaternion.LookRotation(targetDir);
      _towerTop.transform.rotation = Quaternion.RotateTowards(_towerTop.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
  }

  void Update()
  {
    TrackTarget();
  }
}
