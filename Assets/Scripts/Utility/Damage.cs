using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorForestScenery;

public class Damage : MonoBehaviour
{
  private new Collider2D collider;

  private void OnEnable()
  {
    collider = GetComponent<Collider2D>();

    collider.enabled = false;
    collider.enabled = true; // 重新启用以触发检测
  }


  // private void OnTriggerEnter2D(Collider2D other)
  // {
  //   if (other.CompareTag("InteractItem"))
  //   {
  //     other.gameObject.GetComponent<InteractItemController>().OnCollected();
  //   }
  // }
}