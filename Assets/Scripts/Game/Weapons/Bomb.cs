using UnityEngine;
using System.Collections;

// TODO: this all needs restructuring, but I just want it to work NOW!
public class Bomb : MonoBehaviour
{
  public GameObject _explosionPrefab;

  void Explode ()
  {
    if (_explosionPrefab != null)
      GameObject.Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    Destroy (this.gameObject); 
  }

  void OnTriggerEnter ()
  {
    Explode();
  }
}
