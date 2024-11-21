using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSlotController : MonoBehaviour
{
  public ItemEntry itemEntry;

  [Header("广播")] public ItemEventSO buildEvent;
  public ItemEventSO equipItemEvent;

  public void OnItemSlotClicked()
  {
    switch (itemEntry.itemData.usageType)
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
    }
  }

  private void EquipItem()
  {
    var currentItemObj = itemEntry.itemData.prefab;

    equipItemEvent.RaiseEvent(currentItemObj);

    InventoryManager.Instance.RemoveItem(itemEntry.itemData, 1);
  }

  private void BuildItem()
  {
    var currentBuildItem = itemEntry.itemData.prefab;
    
    buildEvent.RaiseEvent(currentBuildItem);
  }
}