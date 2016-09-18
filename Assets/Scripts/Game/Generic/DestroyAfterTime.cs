using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
  public float _destroyTime = 1f;
  public GameObject _target;

  void Start ()
  {
    StartCoroutine(DestroyTarget());
  }

  IEnumerator DestroyTarget ()
  {
    yield return new WaitForSeconds(_destroyTime);

    if (_target)
      Destroy(_target);
    else
      Destroy(gameObject);
  }
}
