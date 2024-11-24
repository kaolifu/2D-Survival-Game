using System;

[Serializable]
public class BaseState
{
  public EnemyController enemyController;
  public Enemy enemy;

  public virtual void OnEnter(EnemyController controller, Enemy enemy)
  {
    enemyController = controller;
    this.enemy = enemy;
  }

  public virtual void OnUpdate()
  {
  }

  public virtual void OnFixedUpdate()
  {
    
  }

  public virtual void OnExit()
  {
  }
}