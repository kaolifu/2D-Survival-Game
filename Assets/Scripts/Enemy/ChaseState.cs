using UnityEngine;

public class ChaseState : BaseState
{
  public override void OnEnter(EnemyController controller, Enemy enemy)
  {
    base.OnEnter(controller, enemy);
    
    Debug.Log("Chase State");
  }

  public override void OnUpdate()
  {
    base.OnUpdate();
  }

  public override void OnFixedUpdate()
  {
    base.OnFixedUpdate();
  }

  public override void OnExit()
  {
    base.OnExit();
  }
}