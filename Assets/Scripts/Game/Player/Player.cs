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

    Game.Instance.GUIManager.SetGUITextValue("VAL_TOWER_TYPE", Localization.GetLocalizedText("LOC_ARROW"));
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


    if (Input.GetButtonDown("Fire2"))
    {
      CycleTowerType();
    }
    if (Input.GetButtonDown("Fire1"))
    {
      TryBuild();
    }
  }

  void CycleTowerType ()
  {
    _towerType = (_towerType == "ARROW_TOWER") ? "BOMB_TOWER" : "ARROW_TOWER";
    string displayName = (_towerType == "ARROW_TOWER") ? Localization.GetLocalizedText("LOC_ARROW") : Localization.GetLocalizedText("LOC_BOMB");
    Game.Instance.GUIManager.SetGUITextValue("VAL_TOWER_TYPE", displayName);
  }

  void TryBuild ()
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

  void OnTriggerEnter (Collider other)
  {
    GameObject go = other.gameObject.FindRoot();
    PickupItem pickup = go.GetComponent<PickupItem>();
    if (pickup != null)
    {
      if (pickup._itemType == PickupItem.ItemType.Money)
      {
        GameData.GetIntData("VAL_MONEY").Value += 1;
      }
      pickup.Collect();
    }
  }
}
