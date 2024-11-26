using System;
using UnityEngine;

public class Character : MonoBehaviour
{
  [Header("health")] public int currentHealth;
  public int maxHealth;

  [Header("damage")] public float damage;
  public float impactForce;
  private bool _isInvulnerable;
  public float invulnerableDuration;
  private float _invulnerableTimer;

  private CharacterController _characterController;

  public bool isHit;

  private void Awake()
  {
    _characterController = GetComponent<CharacterController>();
  }

  private void Update()
  {
    if (_isInvulnerable)
    {
      _invulnerableTimer += Time.deltaTime;
      {
        if (_invulnerableTimer >= invulnerableDuration)
        {
          _isInvulnerable = false;
        }
      }
    }
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
    if (_isInvulnerable) return;
    currentHealth -= (int)attacker.damage;
    _characterController.SetAnimTrigger("Hit");

    var direction = (Vector2)(transform.position - attacker.transform.position).normalized;
    _characterController.AddForce(direction * attacker.impactForce);

    _isInvulnerable = true;
    _invulnerableTimer = 0;

    isHit = true;
    Invoke(nameof(SetIsHitFalse), 1);
  }

  private void SetIsHitFalse()
  {
    isHit = false;
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