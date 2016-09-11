using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  public float _moveSpeed = 5f;
  public Sensor _objectPlacementSensor;
  public string _towerType = "ARROW_TOWER"; // TODO: add menu for selecting this

  GameData.Data<string> _towerTypeData;

  string _inputXAxis;
  string _inputZAxis;
  string _inputInteract;
  string _inputInventory;

  Animator _animator;

  public void SetPlayerID (int id)
  {
    _inputXAxis = string.Format("P{0}_MoveX", id);
    _inputZAxis = string.Format("P{0}_MoveZ", id);
    _inputInteract = string.Format("P{0}_Interact", id);
    _inputInventory = string.Format("P{0}_Inventory", id);
  }

  void Awake ()
  {
    if (_objectPlacementSensor == null)
      Debug.LogError("No Object Placement Node Sensor on player");

    _towerTypeData = GameData.GetStringData("TOWER_TYPE");

    _towerTypeData.Value = Localization.GetLocalizedText("LOC_ARROW");

    _animator = gameObject.GetComponent<Animator>();
  }
  
  void Update ()
  {
    float moveX = Input.GetAxis(_inputXAxis);
    float moveZ = Input.GetAxis(_inputZAxis);
    Vector3 moveDir = new Vector3(moveX, 0f, moveZ);
    if (moveDir.sqrMagnitude > 1f)
      moveDir.Normalize();
    transform.Translate(moveDir * _moveSpeed * Time.deltaTime, Space.World);
    if (moveDir.sqrMagnitude > 0f)
    {
      transform.rotation = Quaternion.LookRotation(moveDir);
      _animator.Play("Walk");
    }
    else
    {
      _animator.Play("Idle");
    }

    if (Input.GetButtonDown(_inputInventory))
    {
      CycleTowerType();
    }
    if (Input.GetButtonDown(_inputInteract))
    {
      TryBuild();
    }
  }

  void CycleTowerType ()
  {
    if (_towerType == "ARROW_TOWER")
    {
      _towerType = "BOMB_TOWER";
      _towerTypeData.Value = Localization.GetLocalizedText("LOC_BOMB");
    }
    else
    {
      _towerType = "ARROW_TOWER";
      _towerTypeData.Value = Localization.GetLocalizedText("LOC_ARROW");
    }
  }

  void TryBuild ()
  {
    int towerCost = 3; // TODO: this should be retrieved from tower data or something

    GameData.Data<int> money = GameData.GetIntData("MONEY");
    if (money.Value < towerCost)
    {
      Debug.Log("Not enough money to build tower.");
      return;
    }

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
        node.PlaceObject();
        money.Value -= towerCost;
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
        GameData.GetIntData("MONEY").Value += 1;
      }
      pickup.Collect();
    }
  }
}
