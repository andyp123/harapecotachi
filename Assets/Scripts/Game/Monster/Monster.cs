using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Monster : MonoBehaviour
{
  public abstract Vector3 GetPositionAfterTime(float seconds);
}
