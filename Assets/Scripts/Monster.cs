using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Monster : MonoBehaviour
{
  // TODO: remove this as soon as there is a proper alternative
  protected static int _killed = 0;
}
