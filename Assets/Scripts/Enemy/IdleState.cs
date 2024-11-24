using UnityEngine;

public class IdleState : BaseState
{
  private float _idleTimer;

  public override void OnEnter(EnemyController controller, Enemy enemy)
  {
    base.OnEnter(controller, enemy);
    
    _idleTimer = 0;
  }

  public override void OnUpdate()
  {
    base.OnUpdate();
    _idleTimer += Time.deltaTime;
    if (_idleTimer >= enemyController.idleDuration)
    {
      enemyController.ChangeState(enemyController.patrolState);
    }
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}