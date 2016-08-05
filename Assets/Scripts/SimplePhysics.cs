using UnityEngine;
using System.Collections;

public class SimplePhysics : MonoBehaviour
{
  public float _friction = 0.9f;
  public float _mass = 1f;

  Vector3 _velocity;
  Vector3 _axisMask = new Vector3(1f, 0f, 1f);

  void Update ()
  {
    _velocity -= _velocity * _friction * Time.deltaTime;
    transform.position += new Vector3(_velocity.x * _axisMask.x, _velocity.y * _axisMask.y, _velocity.z * _axisMask.z) * Time.deltaTime;
  }

  public void AddForce (Vector3 force)
  {
    _velocity += force;
  }
}
