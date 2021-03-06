﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TransformRoot))]
public abstract class Tower : MonoBehaviour
{
  public float _range = 1f;
  public GameObject _rangeIndicator;

  protected float _nextAttackEnableTime = 0f;
  protected bool _attackIsAimed = false;

  protected GameObject _target;

  protected virtual bool ReadyToAttack ()
  {
    if (_target != null && Time.time >= _nextAttackEnableTime && _attackIsAimed)
      return true;

    return false;
  }
}
