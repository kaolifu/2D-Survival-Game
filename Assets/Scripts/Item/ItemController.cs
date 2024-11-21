using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
  public ItemData templateData;
  public ItemData itemData;
  public int amount;

  private Animator _animator;

  private void Awake()
  {
    itemData = Instantiate(templateData);
    _animator = GetComponent<Animator>();
  }

  private void Update()
  {
    _animator.SetBool("Collectable", itemData.collectable);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (itemData.collectable)
    {
      if (other.CompareTag("Player"))
      {
        InventoryManager.Instance.AddItem(itemData, amount);

        Destroy(gameObject);
      }
    }
  }

  public void SetIsScenery(bool isScenery)
  {
    _animator.SetBool("IsScenery", isScenery);
  }
}