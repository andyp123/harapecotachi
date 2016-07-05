using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  public float _moveSpeed = 5f;

  void Start ()
  {
  }
  
  void Update ()
  {
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");
    Vector3 moveDir = new Vector3(moveX, 0f, moveZ);
    if (moveDir.sqrMagnitude > 1f)
      moveDir.Normalize();
    transform.Translate(moveDir * _moveSpeed * Time.deltaTime, Space.World);
    if (moveDir.sqrMagnitude > 0f)
    {
      transform.rotation = Quaternion.LookRotation(moveDir);
    }
  }
}
