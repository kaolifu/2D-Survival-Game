using UnityEngine;

public class PatrolState : BaseState
{
  private Vector2 _targetDestination;
  private Vector2 _originalPosition;

  public override void OnEnter(EnemyController controller, Enemy enemy)
  {
    base.OnEnter(controller, enemy);

    enemyController.moveSpeed = enemyController.patrolSpeed;

    if (_originalPosition == Vector2.zero)
    {
      _originalPosition = controller.transform.position;
    }

    enemyController.SetAnimBool("IsMoving", true);

    GetRandomDestination();
  }

  public override void OnUpdate()
  {
    base.OnUpdate();
    if (Vector2.Distance(enemy.transform.position, _targetDestination) <= 0.2)
    {
      enemyController.ChangeState(enemyController.idleState);
    }

    if (enemyController.FindPlayer() || enemy.isHit)
    {
      enemyController.ChangeState(enemyController.chaseState);
    }
  }

  public override void OnFixedUpdate()
  {
    base.OnFixedUpdate();
    enemyController.MoveTo(_targetDestination);

    if (_targetDestination.x < enemyController.transform.position.x && enemyController.transform.localScale.x > 0)
    {
      enemyController.Flip();
    }
    else if (_targetDestination.x > enemyController.transform.position.x && enemyController.transform.localScale.x < 0)
    {
      enemyController.Flip();
    }
  }

  public override void OnExit()
  {
    base.OnExit();

    enemyController.SetAnimBool("IsMoving", false);
  }

  private void GetRandomDestination()
  {
    var radius = enemyController.patrolRadius;
    _targetDestination = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius)) + _originalPosition;
  }
}