using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
  public static InventoryManager Instance;

  public List<ItemEntry> items = new List<ItemEntry>();

  public BuildingListSO buildingListSO;

  public Transform itemContent;
  public GameObject itemSlotPrefab;

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

  public void AddItem(ItemSO item, int amount)
  {
    var existingItem = items.Find(i => i.item == item);
    if (existingItem != null)
    {
      existingItem.amount += amount;
    }
    else
    {
      var newItem = new ItemEntry(item);
      newItem.amount = amount;
      items.Add(newItem);
    }
  }

  public void RemoveItem(ItemSO item, int amount)
  {
    var result = items.Find(i => i.item == item);
    result.amount = Mathf.Max(result.amount - amount, 0);

    if (result.amount <= 0)
    {
      items.Remove(result);
    }
  }

  public void RemoveItemByName(string itemName, int amount)
  {
    var result = items.Find(i => i.item.itemName == itemName);
    result.amount = Mathf.Max(result.amount - amount, 0);
  }

  public void ListItems()
  {
    foreach (Transform child in itemContent)
    {
      Destroy(child.gameObject);
    }

    foreach (var itemEntry in items)
    {
      var itemSlot = Instantiate(itemSlotPrefab, itemContent);

      itemSlot.GetComponent<ItemSlotController>().itemEntry = itemEntry;

      var itemSlotName = itemSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
      var itemSlotIcon = itemSlot.transform.Find("Icon").GetComponent<Image>();
      var itemSlotAmount = itemSlot.transform.Find("Amount").GetComponentInChildren<TextMeshProUGUI>();

      itemSlotName.text = itemEntry.item.itemName;
      itemSlotIcon.sprite = itemEntry.item.icon;
      itemSlotAmount.text = itemEntry.amount.ToString();
    }
  }
}