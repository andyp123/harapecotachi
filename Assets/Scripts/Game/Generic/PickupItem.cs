using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SimplePhysics))]
public class PickupItem : MonoBehaviour
{
  public enum ItemType
  {
    Money
  }

  public ItemType _itemType = ItemType.Money;
  public float _attractionRadius = 1f;
  public float _attractionForce = 5f;
  public float _rotationSpeed = 50f;
  
  GameObject _target = null;
  SimplePhysics _physics = null;
  float _sqrAttractionRadius;


  void Awake ()
  {
    transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    _physics = gameObject.GetComponent<SimplePhysics>();
    _sqrAttractionRadius = _attractionRadius * _attractionRadius;
  }

  void Update ()
  {
    transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

    if (_target == null)
    {
      Player player = Game.Instance.GameManager.GetNearestPlayer(transform.position, _attractionRadius);
      if (player != null)
        _target = player.gameObject;
    }
    else
    {
      Vector3 dir = _target.transform.position - transform.position;
      float sqrDistance = dir.sqrMagnitude;
      if (sqrDistance < _sqrAttractionRadius)
        _physics.AddForce(dir * _attractionForce * sqrDistance / _sqrAttractionRadius);
    }
  }

  public void Collect()
  {
    Destroy(this.gameObject);
  }
}
