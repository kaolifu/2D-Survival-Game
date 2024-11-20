using UnityEngine;

public class Player : Character
{
  public int sleepPoint;
  public int sleepDecreaseSpeed;
  public int foodPoint;
  public int foodDecreaseSpeed;
  public int waterPoint;
  public int waterDecreaseSpeed;

  private void Start()
  {
    InvokeRepeating(nameof(DecreasePlayerStats), 1.0f, 1.0f);
  }

  private void DecreasePlayerStats()
  {
    sleepPoint = Mathf.Clamp(sleepPoint - sleepDecreaseSpeed, 0, 100);
    foodPoint = Mathf.Clamp(sleepPoint - foodDecreaseSpeed, 0, 100);
    waterPoint = Mathf.Clamp(sleepPoint - waterDecreaseSpeed, 0, 100);
  }
}