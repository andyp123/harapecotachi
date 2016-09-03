using UnityEngine;
using System.Collections;

public class SetRotation : MonoBehaviour
{
  void Start ()
  {
    Vector3 rot = transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(rot.x, Random.Range(0f, 360f), rot.z);
  }
}
