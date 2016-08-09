using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  public float _moveSpeed = 5f;
  public Sensor _objectPlacementSensor;
  public string _towerType = "ARROW_TOWER"; // TODO: add menu for selecting this

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
    TargetInfo targetInfo = _objectPlacementSensor.GetNearestTarget();
    GameObject nearest = (targetInfo != null) ? targetInfo.target : null;
    if (nearest != null)
    {
      ObjectPlacementNode node = nearest.GetComponent<ObjectPlacementNode>();
      if (node != null && !node._occupied)
      {
        // build tower at node
        Transform nt = node.transform;
        Game.Instance.AssetManager.InstantiatePrefab(_towerType, nt.position, nt.rotation);
        node._occupied = true;
      }
      else
      {
        Debug.Log("can't build tower at node.");
      }
    }
  }
}
