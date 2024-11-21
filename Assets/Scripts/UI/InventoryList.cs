using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryList : MonoBehaviour
{
  private Transform itemContent;
  public GameObject itemSlotPrefab;

  [Header("监听")] public VoidEventSO OnInventoryChanged;

  private void Awake()
  {
    itemContent = transform.Find("Viewport/Content");
  }

  private void OnEnable()
  {
    OnInventoryChanged.OnEventRaised += ListItems;

    ListItems();
  }

  private void OnDisable()
  {
    OnInventoryChanged.OnEventRaised -= ListItems;
  }

  public void ListItems()
  {
    foreach (Transform child in itemContent)
    {
      Destroy(child.gameObject);
    }

    var items = InventoryManager.Instance.items;
    foreach (var itemEntry in items)
    {
      var itemSlot = Instantiate(itemSlotPrefab, itemContent);

      itemSlot.GetComponent<ItemSlotController>().itemEntry = itemEntry;

      var itemSlotName = itemSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
      var itemSlotIcon = itemSlot.transform.Find("Icon").GetComponent<Image>();
      var itemSlotAmount = itemSlot.transform.Find("Amount").GetComponentInChildren<TextMeshProUGUI>();


      itemSlotName.text = itemEntry.itemData.itemName;
      itemSlotIcon.sprite = itemEntry.itemData.icon;
      itemSlotAmount.text = itemEntry.amount.ToString();
    }
  }
}