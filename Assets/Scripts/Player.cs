using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  public float _moveSpeed = 5f;
  public Sensor _objectPlacementSensor;

  void Awake ()
  {
    if (_objectPlacementSensor == null)
      Debug.LogError("No Object Placement Node Sensor on player");
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

    if (Input.GetButtonDown("Fire1"))
    {
      TryBuild();
    }
  }

  void TryBuild()
  {
    Debug.Log("trying to build");
    GameObject nearest = _objectPlacementSensor.GetNearestTarget();
    if (nearest != null)
    {
      ObjectPlacementNode node = nearest.GetComponent<ObjectPlacementNode>();
      if (node != null && !node._occupied)
      {
        // build tower at node
        Debug.Log("building!");
        Transform nt = node.transform;
        GameManager.Instance.InstantiatePrefab("TEST_TOWER_1", nt.position, nt.rotation);
        node._occupied = true;
      }
    }
  }
}
