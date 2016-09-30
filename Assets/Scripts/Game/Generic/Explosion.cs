﻿using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Sensor))]
public class Explosion : MonoBehaviour
{
  public float _radius = 1f;
  public float _damage = 2f;
  public bool _scaleDamageByDistance = true;
  public bool _spawnOnGround = false;
  public GameObject _explosionPrefab;

  //float _radius = 1f;
  //Sensor _sensor;

  void Awake ()
  {
    // _sensor = gameObject.GetComponent<Sensor>();
    // SphereCollider collider = gameObject.GetComponent<SphereCollider>();
    // _radius = collider.radius; // TODO: will I need other types of collider?

    if (_explosionPrefab != null)
    {
      Vector3 pos = transform.position;
      if (_spawnOnGround)
      {
        pos.y = 0f;
        transform.position = pos;
      }
      GameObject.Instantiate(_explosionPrefab, pos, Quaternion.identity);
    }

    Explode();
    // StartCoroutine(Explode());
  }

  void Explode ()
  {
    int layerMask = 1 << LayerMask.NameToLayer("MonsterCollider");
    Collider[] hitTargets = Physics.OverlapSphere(transform.position, _radius, layerMask, QueryTriggerInteraction.Collide);

    // apply damage to them
    foreach (Collider t in hitTargets)
    {
      GameObject go = t.gameObject.FindRoot();
      Damage damage = go.GetComponent<Damage>(); 
      if (damage != null)
      {
        float damageScale = 1f;
        if (_scaleDamageByDistance)
          damageScale = 1f - ((transform.position - go.transform.position).sqrMagnitude / (_radius * _radius));

        DamageInfo info = new DamageInfo();
        info.baseDamage = _damage * damageScale;
        info.knockbackForce = info.baseDamage * Random.Range(1f, 2.5f);
        info.knockbackDir = (go.transform.position - transform.position).normalized;
        info.damageType = DamageType.Explosive;

        damage.ApplyDamage(info);
      }
    }

    Destroy(this.gameObject);    
  }

  // IEnumerator Explode ()
  // {
  //   // TODO: this allows the sensor time to gather targets
  //   yield return new WaitForSeconds(0.05f);

  //   // get nearest n enemies
  //   TargetInfo[] nearestTargets = _sensor.GetNearestTargets();

  //   // apply damage to them
  //   foreach (TargetInfo t in nearestTargets)
  //   {
  //     if (t.target == null) // TODO: the target can be null sometimes, which causes an error. This needs solving
  //       continue;
  //     GameObject go = t.target;
  //     Damage damage = go.GetComponent<Damage>(); 
  //     if (damage != null)
  //     {
  //       float damageScale = (_scaleDamageByDistance) ? 1f - (t.sqrDistance / (_radius * _radius)) : 1f;

  //       DamageInfo info = new DamageInfo();
  //       info.baseDamage = _damage * damageScale;
  //       info.knockbackForce = info.baseDamage * Random.Range(2f, 5f);
  //       info.knockbackDir = (go.transform.position - transform.position).normalized;
  //       info.damageType = DamageType.Explosive;

  //       damage.ApplyDamage(info);
  //     }
  //   }

  //   Destroy(this.gameObject);
  // }

  void OnDrawGizmos ()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, _radius);
  }
}
