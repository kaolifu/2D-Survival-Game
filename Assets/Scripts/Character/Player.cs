using UnityEngine;

public class Player : Character
{
  [Header("Survival Stats")]
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

  public void IncreasePlayerStats(int health, int sleep, int food, int water)
  {
    currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
    sleepPoint = Mathf.Clamp(sleepPoint + sleep, 0, 100);
    foodPoint = Mathf.Clamp(foodPoint + food, 0, 100);
    waterPoint = Mathf.Clamp(waterPoint + water, 0, 100);
  }
}