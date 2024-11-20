using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D _rb;
  private Animator _anim;
  private InputControl _inputControl;
  private Transform _buildingPos;
  private WeaponController _weaponController;
  [SerializeField] private Vector2 _inputDirection;

  [Header("Move")] public float moveSpeed;
  [Header("Stats")] private bool isMoving;
  public bool canMove = true;
  [SerializeField] private bool _isBuildingModel;
  private GameObject _currentBuilding;

  [Header("监听")] public BuildingEventSO buildingEvent;

  [Header("广播")] public VoidEventSO buildingOverEvent;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponentInChildren<Animator>();
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

    buildingEvent.OnEventRaised += OnBuildingEventRaised;
  }


  private void OnDisable()
  {
    _inputControl.Gameplay.Attack.started -= OnAttackEvent;
    _inputControl.Gameplay.Confirm.started -= OnConfirmEvent;
    _inputControl.Gameplay.Cancle.started -= OnCancelEvent;

    buildingEvent.OnEventRaised -= OnBuildingEventRaised;
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
      transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
  }

  private void OnAttackEvent(InputAction.CallbackContext obj)
  {
    _anim.SetTrigger("Slash");
    Debug.Log(_weaponController.GetCurrentWeaponType());
  }

  private void OnBuildingEventRaised(GameObject building)
  {
    _isBuildingModel = true;

    _currentBuilding = Instantiate(building, _buildingPos);
    _currentBuilding.GetComponent<Collider2D>().enabled = false;
    _currentBuilding.transform.Find("Renderer").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
  }

  private void OnConfirmEvent(InputAction.CallbackContext obj)
  {
    if (_isBuildingModel)
    {
      if (_currentBuilding != null)
      {
        _currentBuilding.GetComponent<Collider2D>().enabled = true;
        _currentBuilding.transform.Find("Renderer").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        GameObject environment = GameObject.Find("Environment");
        _currentBuilding.transform.parent = environment.transform;

        InventoryManager.Instance.RemoveItem(_currentBuilding.GetComponent<SceneryItemNew>().itemSO, 1);

        buildingOverEvent.RaiseEvent();
        _isBuildingModel = false;
      }
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
}