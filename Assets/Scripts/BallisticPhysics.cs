using UnityEngine;
using System.Collections;

// very simple class for applying ballistic physics
public class BallisticPhysics : MonoBehaviour
{
  public float _gravity = 9.8f; // may want custom values per shot

  Vector3 _velocity;

  void Update ()
  {
    _velocity -= Vector3.up * _gravity * Time.deltaTime;
    transform.position += _velocity * Time.deltaTime;
  }

  public void AddForce (Vector3 force)
  {
    _velocity += force;
  }
}
