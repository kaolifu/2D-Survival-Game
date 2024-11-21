using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
  public static InventoryManager Instance;

  public List<ItemEntry> items = new List<ItemEntry>();

  [Header("广播")] public VoidEventSO OnInventoryChanged;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void AddItem(ItemData itemData, int amount)
  {
    var existingItemData = items.Find(i => i.itemData.itemName == itemData.itemName);
    if (existingItemData != null)
    {
      existingItemData.amount += amount;
    }
    else
    {
      var newItemData = new ItemEntry();
      newItemData.itemData = itemData;
      newItemData.amount = amount;
      items.Add(newItemData);
    }

    OnInventoryChanged.RaiseEvent();
  }

  public void RemoveItem(ItemData itemData, int amount)
  {
    var result = items.Find(i => i.itemData == itemData);
    result.amount = Mathf.Max(result.amount - amount, 0);

    if (result.amount <= 0)
    {
      items.Remove(result);
    }

    OnInventoryChanged.RaiseEvent();
  }

  public void RemoveItemByName(string itemName, int amount)
  {
    var result = items.Find(i => i.itemData.itemName == itemName);
    result.amount = Mathf.Max(result.amount - amount, 0);

    if (result.amount <= 0)
    {
      items.Remove(result);
    }

    OnInventoryChanged.RaiseEvent();
  }
}

[System.Serializable]
public class ItemEntry
{
  public ItemData itemData;
  public int amount;
}