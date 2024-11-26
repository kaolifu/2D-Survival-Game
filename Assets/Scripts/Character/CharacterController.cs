using System;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
  protected Rigidbody2D _rb;
  protected Animator _anim;
  [Header("Move")] public float moveSpeed;
  [HideInInspector] public bool canMove = true;

  protected virtual void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _anim = GetComponent<Animator>();
  }

  public void SetAnimBool(string animName, bool booleanValue)
  {
    _anim.SetBool(animName, booleanValue);
  }

  public void SetAnimTrigger(string animName)
  {
    _anim.SetTrigger(animName);
  }

  public void AddForce(Vector2 force)
  {
    canMove = false;
    _rb.AddForce(force, ForceMode2D.Impulse);
  }

  public void Flip()
  {
    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
  }
}