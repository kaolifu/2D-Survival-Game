using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
  private TextMeshProUGUI nameText;
  private Image iconImage;
  private TextMeshProUGUI descriptionText;

  private ItemData _currentItemData;

  [Header("监听")] public ItemDataEventSO itemSlotClickedEvent;

  private void Awake()
  {
    nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    iconImage = transform.Find("Icon").GetComponent<Image>();
    descriptionText = transform.Find("Description").GetComponent<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    itemSlotClickedEvent.OnEventRaised += UpdateItemInfo;

    InitializeOpen();
  }


  private void OnDisable()
  {
    itemSlotClickedEvent.OnEventRaised -= UpdateItemInfo;
  }

  private void InitializeOpen()
  {
    if (_currentItemData == null)
    {
      HideChildren();
    }
    else
    {
      UpdateItemInfo(_currentItemData);
    }
  }

  private void UpdateItemInfo(ItemData itemData)
  {
    _currentItemData = itemData;

    AppearChildren();

    nameText.text = itemData.itemName;
    iconImage.sprite = itemData.icon;
    descriptionText.text = itemData.itemDescription;
  }

  private void HideChildren()
  {
    nameText.gameObject.SetActive(false);
    iconImage.gameObject.SetActive(false);
    descriptionText.gameObject.SetActive(false);
  }

  private void AppearChildren()
  {
    nameText.gameObject.SetActive(true);
    iconImage.gameObject.SetActive(true);
    descriptionText.gameObject.SetActive(true);
  }
}