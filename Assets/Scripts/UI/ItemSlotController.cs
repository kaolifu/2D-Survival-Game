using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSlotController : MonoBehaviour
{
  public ItemEntry itemEntry;

  [Header("广播")] public ItemDataEventSO itemDataEvent;

  public void OnSlotClick()
  {
    itemDataEvent?.RaiseEvent(itemEntry.itemData);
  }
}