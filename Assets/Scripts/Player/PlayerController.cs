using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : CharacterController
{
  private InputControl _inputControl;
  private Transform _buildingPos;
  private WeaponController _weaponController;

  private Vector2 _inputDirection;

  [Header("Stats")] private bool isMoving;
  private bool _isBuildingModel;
  private GameObject _currentBuilding;

  [Header("监听")] public ItemEventSO itemEvent;
  public ItemEventSO EquipItemEvent;

  [Header("广播")] public VoidEventSO buildingOverEvent;

  protected override void Awake()
  {
    base.Awake();
    _buildingPos = transform.Find("BuildingPos");
    _weaponController = GetComponentInChildren<WeaponController>();

    _inputControl = new InputControl();
    _inputControl.Enable();
  }

  private void OnEnable()
  {
    _inputControl.Gameplay.Attack.started += OnAttackEvent;
    _inputControl.Gameplay.Confirm.started += OnConfirmEvent;
    _inputControl.Gameplay.Cancle.started += OnCancelEvent;

    itemEvent.OnEventRaised += OnBuildingEventRaised;
    EquipItemEvent.OnEventRaised += OnEquipItemEventRaised;
  }


  private void OnDisable()
  {
    _inputControl.Gameplay.Attack.started -= OnAttackEvent;
    _inputControl.Gameplay.Confirm.started -= OnConfirmEvent;
    _inputControl.Gameplay.Cancle.started -= OnCancelEvent;

    itemEvent.OnEventRaised -= OnBuildingEventRaised;
    EquipItemEvent.OnEventRaised -= OnEquipItemEventRaised;
  }


  private void Update()
  {
    _inputDirection = _inputControl.Gameplay.Move.ReadValue<Vector2>();

    SetAnimation();
  }

  private void FixedUpdate()
  {
    Move();
  }

  private void SetAnimation()
  {
    if (_inputDirection.sqrMagnitude > 0)
    {
      isMoving = true;
    }
    else
    {
      isMoving = false;
    }

    _anim.SetBool("IsMove", isMoving);
  }

  private void Move()
  {
    if (canMove)
    {
      _rb.MovePosition(_rb.position + _inputDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    if ((_inputDirection.x < 0 && transform.localScale.x > 0) || (_inputDirection.x > 0 && transform.localScale.x < 0))
    {
      Flip();
    }
  }

  private void OnAttackEvent(InputAction.CallbackContext obj)
  {
    _anim.SetTrigger("Slash");
  }

  /// <summary>
  /// 将待建造物体放到玩家身上
  /// </summary>
  /// <param name="building"></param>
  private void OnBuildingEventRaised(GameObject building)
  {
    _isBuildingModel = true;

    _currentBuilding = Instantiate(building, _buildingPos);
    _currentBuilding.GetComponent<BoxCollider2D>().enabled = false;
    _currentBuilding.GetComponent<CircleCollider2D>().enabled = false;
    _currentBuilding.GetComponent<ItemController>().itemData.collectable = false;

    ChangeBuildingAlpha(0.2f);
  }

  /// <summary>
  /// 玩家按下确定键后在玩家指定的位置安放建筑物
  /// </summary>
  /// <param name="obj"></param>
  private void OnConfirmEvent(InputAction.CallbackContext obj)
  {
    if (_isBuildingModel)
    {
      if (_currentBuilding != null)
      {
        _currentBuilding.GetComponent<BoxCollider2D>().enabled = true;
        ChangeBuildingAlpha(1.0f);
        GameObject environment = GameObject.Find("Environment");
        _currentBuilding.transform.parent = environment.transform;

        InventoryManager.Instance.RemoveItemByName(_currentBuilding.GetComponent<ItemController>().itemData.itemName,
          1);

        _currentBuilding.GetComponent<ItemController>().SetIsScenery(true);

        buildingOverEvent.RaiseEvent();
        _isBuildingModel = false;
      }
    }
  }

  private void ChangeBuildingAlpha(float alpha)
  {
    var srs = _buildingPos.GetComponentsInChildren<SpriteRenderer>();
    foreach (var sr in srs)
    {
      sr.color = new Color(1, 1, 1, alpha);
    }
  }

  private void OnCancelEvent(InputAction.CallbackContext obj)
  {
    if (_isBuildingModel)
    {
      if (_currentBuilding != null)
      {
        Destroy(_currentBuilding.gameObject);
        _isBuildingModel = false;
      }
    }
  }

  private void OnEquipItemEventRaised(GameObject item)
  {
    _weaponController.EquipWeapon(item);
  }
}