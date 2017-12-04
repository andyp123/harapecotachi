using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
  public float _radius = 1f;
  public float _damage = 2f;
  public bool _scaleDamageByDistance = true;
  public bool _spawnOnGround = false;
  public GameObject _explosionPrefab;

  void Start ()
  {
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
  }

  void Explode ()
  {
    Vector3 pos = transform.position;
    if (_spawnOnGround) pos = new Vector3(pos.x, 0f, pos.z);

    int layerMask = 1 << LayerMask.NameToLayer("MonsterCollider");
    Collider[] hitTargets = Physics.OverlapSphere(pos, _radius, layerMask, QueryTriggerInteraction.Collide);

    // apply damage to them
    foreach (Collider t in hitTargets)
    {
      GameObject go = t.gameObject.FindRoot();
      Damage damage = go.GetComponent<Damage>(); 
      if (damage != null)
      {
        float damageScale = 1f;
        if (_scaleDamageByDistance)
          damageScale = 1f - ((pos - go.transform.position).sqrMagnitude / (_radius * _radius));

        DamageInfo info = new DamageInfo();
        info.baseDamage = _damage * damageScale;
        info.knockbackForce = info.baseDamage * Random.Range(1f, 2.5f);
        info.knockbackDir = (go.transform.position - pos).normalized;
        info.damageType = DamageType.Explosive;

        damage.ApplyDamage(info);
      }
    }

    Destroy(this.gameObject);    
  }

  void OnDrawGizmos ()
  {
    Vector3 pos = transform.position;
    if (_spawnOnGround) pos = new Vector3(pos.x, 0f, pos.z);

    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(pos, _radius);
  }
}
