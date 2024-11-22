using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseItemButton : MonoBehaviour
{
  [Header("监听")] public ItemDataEventSO itemSlotClickedEvent;

  [SerializeField] private ItemData _currentItemData;

  [Header("广播")] public ItemEventSO buildEvent;
  public ItemEventSO equipItemEvent;

  private Button _button;
  private TextMeshProUGUI _text;

  private void Awake()
  {
    _button = GetComponent<Button>();
    _text = GetComponentInChildren<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    itemSlotClickedEvent.OnEventRaised += OnItemSlotClickedEvent;

    if (_currentItemData == null)
    {
      GetComponent<Button>().interactable = false;
    }
  }

  private void OnDisable()
  {
    itemSlotClickedEvent.OnEventRaised -= OnItemSlotClickedEvent;
  }

  private void OnItemSlotClickedEvent(ItemData itemData)
  {
    _currentItemData = itemData;

    ChangeTextByUsageType();
  }

  private void ChangeTextByUsageType()
  {
    if (_currentItemData != null)
    {
      switch (_currentItemData.usageType)
      {
        case UsageType.None:
          _button.interactable = false;
          _text.text = "不可使用";
          break;
        case UsageType.CanUse:
          _button.interactable = true;
          _text.text = "使用";
          break;
        case UsageType.CanEquip:
          _button.interactable = true;
          _text.text = "装备";
          break;
        case UsageType.CanBuild:
          _button.interactable = true;
          _text.text = "建造";
          break;
      }
    }
  }

  public void OnButtonClick()
  {
    if (_currentItemData == null)
    {
      GetComponent<Button>().interactable = false;
    }

    switch (_currentItemData.usageType)
    {
      case UsageType.None:
        Debug.Log("不可使用");
        break;
      case UsageType.CanBuild:
        BuildItem();
        break;
      case UsageType.CanEquip:
        EquipItem();
        break;
      case UsageType.CanUse:
        UseItem();
        break;
    }
  }

  private void UseItem()
  {
    _currentItemData.prefab.GetComponent<IUseItem>().UseItem();

    InventoryManager.Instance.RemoveItem(_currentItemData, 1);
  }

  private void EquipItem()
  {
    var currentItemObj = _currentItemData.prefab;

    equipItemEvent.RaiseEvent(currentItemObj);

    InventoryManager.Instance.RemoveItem(_currentItemData, 1);
  }

  private void BuildItem()
  {
    var currentBuildItem = _currentItemData.prefab;

    buildEvent.RaiseEvent(currentBuildItem);
  }
}