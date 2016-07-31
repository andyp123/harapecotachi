using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoroutineTest : MonoBehaviour
{
  float _time = 0f;
  Text _text = null;
  IEnumerator _coroutine = null;

  void Awake ()
  {
    _text = gameObject.GetComponent<Text>();
    if (_text != null)
    {
      _coroutine = IncrementTime();
      StartCoroutine(_coroutine);
    }
  }

  IEnumerator IncrementTime()
  {
    while (true)
    {
      _text.text = _time.ToString();
      yield return new WaitForSeconds(1f);
      _time += 1f;
    }
  }

  public void Toggle ()
  {
    if (_text != null)
    {
      if (_coroutine == null)
      {
        _coroutine = IncrementTime();
        StartCoroutine(_coroutine);
      }
      else
      {
        StopCoroutine(_coroutine);
        _coroutine = null;
      }
    }
  }
}
