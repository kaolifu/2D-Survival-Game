using System;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("health")] public int currentHealth;
  public int maxHealth;

  [Header("damage")] public float damage;
  public float impactForce;

  private CharacterController _characterController;

  private void Awake()
  {
    _characterController = GetComponent<CharacterController>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Damage"))
    {
      var attacker = other.GetComponentInParent<Character>();
      TakeDamage(attacker);
    }
  }

  private void TakeDamage(Character attacker)
  {
    currentHealth -= (int)attacker.damage;
    _characterController.SetAnimTrigger("Hit");

    var direction = (Vector2)(transform.position - attacker.transform.position).normalized;
    _characterController.AddForce(direction * attacker.impactForce);
  }

  public void ChangeSpritesRed()
  {
    var renderers = GetComponentsInChildren<SpriteRenderer>();
    foreach (var renderer in renderers)
    {
      renderer.color = Color.red;
    }
  }

  public void ChangeSpritesWhite()
  {
    var renderers = GetComponentsInChildren<SpriteRenderer>();
    foreach (var renderer in renderers)
    {
      renderer.color = Color.white;
    }
  }
}