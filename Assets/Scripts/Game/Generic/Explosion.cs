using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Sensor))]
public class Explosion : MonoBehaviour
{
  public float _damage = 2f;
  public bool _scaleDamageByDistance = true;

  float _radius = 1f;

  Sensor _sensor;

  void Awake ()
  {
    _sensor = gameObject.GetComponent<Sensor>();
    SphereCollider collider = gameObject.GetComponent<SphereCollider>();
    _radius = collider.radius; // TODO: will I need other types of collider?

    StartCoroutine(Explode());
  }

  IEnumerator Explode ()
  {
    yield return new WaitForSeconds(0.05f);

    // get nearest n enemies (might want to change the sensor for a sphere cast instead?)
    TargetInfo[] nearestTargets = _sensor.GetNearestTargets();

    // apply damage to them
    foreach (TargetInfo t in nearestTargets)
    {
      GameObject go = t.target;
      Damage damage = go.GetComponent<Damage>();
      if (damage != null)
      {
        float damageScale = (_scaleDamageByDistance) ? 1f - (t.sqrDistance / (_radius * _radius)) : 1f;
        Vector3 dir = (go.transform.position - transform.position).normalized;
        damage.ApplyDamage(_damage * damageScale, dir);
      }
    }

    Destroy(this.gameObject);
  }

  void OnDrawGizmos ()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, _radius);
  }
}
