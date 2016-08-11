using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour
{
  float _attractionRadius = 1f;
  float _rotationSpeed = 50f;
  GameObject _target = null;

  void Awake ()
  {
    transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
  }

  void Update ()
  {
    transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

    if (_target == null)
      _target = Game.Instance.GameManager.GetNearestPlayer(transform.position, _attractionRadius);
    else
    {
      float sqrDistance = (_target.transform.position - transform.position).sqrMagnitude;
      if (sqrDistance < _attractionRadius * _attractionRadius)
      {

      }
    }
  }
}
