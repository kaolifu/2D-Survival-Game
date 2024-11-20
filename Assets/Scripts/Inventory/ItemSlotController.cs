using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotController : MonoBehaviour
{
  public ItemEntry itemEntry;

  [Header("广播")] public BuildingEventSO buildingEvent;

  public void OnItemSlotClicked()
  {
    switch (itemEntry.item.usageType)
    {
      case ItemUsageType.None:
        Debug.Log("不可使用");
        break;
      case ItemUsageType.CanBuild:
        BuildItem();
        break;
    }
  }

  private void BuildItem()
  {
    var currentBuildItem =
      InventoryManager.Instance.buildingListSO.BuildingList.Find(item => item.name == itemEntry.item.name);

    buildingEvent.RaiseEvent(currentBuildItem);
  }
}