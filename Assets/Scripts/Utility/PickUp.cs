using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUp : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    var item = other.GetComponent<ItemController>();
    if (item != null)
    {
      InventoryManager.Instance.AddItem(item.itemData, 1);

      Destroy(other.gameObject);
    }
  }
}