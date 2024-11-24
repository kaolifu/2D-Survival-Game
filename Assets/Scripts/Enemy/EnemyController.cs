using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
  private Enemy _enemy;

  private BaseState _currentState;
  public IdleState idleState;
  public PatrolState patrolState;
  public ChaseState chaseState;

  private int faceDir = 1;
  private bool isFoundPlayer;

  [Header("State")] public float idleDuration = 2.0f;
  public float patrolRadius;

  [Header("FindPlayer")] public Vector2 viewRange;
  public Vector2 viewOffset;

  protected override void Awake()
  {
    base.Awake();
    _enemy = GetComponent<Enemy>();

    idleState = new IdleState();
    patrolState = new PatrolState();
    chaseState = new ChaseState();

    ChangeState(idleState);
  }

  private void Start()
  {
  }

  private void Update()
  {
    _currentState.OnUpdate();

    FindPlayer();
  }

  private void FixedUpdate()
  {
    _currentState.OnFixedUpdate();
  }

  public void ChangeState(BaseState newState)
  {
    _currentState?.OnExit();

    _currentState = newState;
    _currentState.OnEnter(this, _enemy);
  }

  public void MoveTo(Vector2 destination)
  {
    if (!canMove) return;

    // 计算当前位置与目标位置的方向
    Vector2 currentPosition = _rb.position;
    Vector2 direction = (destination - currentPosition).normalized;

    // 计算本帧移动的距离
    float distanceThisFrame = moveSpeed * Time.fixedDeltaTime;

    // 检查是否到达目标
    if (Vector2.Distance(currentPosition, destination) <= distanceThisFrame)
    {
      // 如果距离小于一步，则直接设置刚体到目标位置
      _rb.MovePosition(destination);
    }
    else
    {
      // 否则，朝着目标位置移动
      Vector2 newPosition = currentPosition + direction * distanceThisFrame;
      _rb.MovePosition(newPosition);
    }
  }

  private void FindPlayer()
  {
    faceDir = transform.localScale.x < 0 ? -1 : 1;

    var hit = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(viewOffset.x * faceDir, viewOffset.y),
      new Vector2(viewRange.x, viewRange.y), 0f,
      LayerMask.GetMask("Player"));

    if (hit != null && hit.CompareTag("Player"))
    {
      isFoundPlayer = true;

      if (_currentState != chaseState)
      {
        ChangeState(chaseState);
      }
    }
  }


  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, patrolRadius);

    Gizmos.color = Color.red;
    Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(viewOffset.x * faceDir, viewOffset.y),
      new Vector2(viewRange.x, viewRange.y));
  }
}