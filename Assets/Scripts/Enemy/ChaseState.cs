using UnityEngine;

public class ChaseState : BaseState
{
  public Player player;

  private Transform _target;
  private float _chaseTimer;

  public override void OnEnter(EnemyController controller, Enemy enemy)
  {
    base.OnEnter(controller, enemy);
    player = controller.player;
    _target = player.transform;

    enemyController.moveSpeed = enemyController.chaseSpeed;

    enemyController.SetAnimBool("IsMoving", true);

    _chaseTimer = 0;
  }

  public override void OnUpdate()
  {
    base.OnUpdate();

    // 退出chase状态
    if (!enemyController.FindPlayer())
    {
      _chaseTimer += Time.deltaTime;
      if (_chaseTimer >= enemyController.chaseDuration)
      {
        enemyController.ChangeState(enemyController.idleState);
      }
    }

    if (enemyController.canAttack)
    {
      Attack();
    }
  }

  public override void OnFixedUpdate()
  {
    base.OnFixedUpdate();

    enemyController.MoveTo(_target.position);

    if (_target.position.x < enemyController.transform.position.x && enemyController.transform.localScale.x > 0)
    {
      enemyController.Flip();
    }
    else if (_target.position.x > enemyController.transform.position.x && enemyController.transform.localScale.x < 0)
    {
      enemyController.Flip();
    }
  }

  public override void OnExit()
  {
    base.OnExit();

    enemyController.SetAnimBool("IsMoving", false);

    enemyController.damageZone.SetActive(false);
  }

  private void Attack()
  {
    if (enemyController.isAttacking || !enemyController.canAttack) return;

    enemyController.damageZone.SetActive(true);

    enemyController.SetAnimTrigger("Attack");

    var direction = _target.position - enemy.transform.position;
    enemyController.MoveToOnce(direction, 8.0f, 0.3f);
  }
}