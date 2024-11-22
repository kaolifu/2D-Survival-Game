using System;
using UnityEngine;

public class Meat : MonoBehaviour, IUseItem
{
  public void UseItem()
  {
    var itemData = GetComponent<ItemController>().itemData;
    if (itemData == null)
    {
      itemData = GetComponent<ItemController>().templateData;
    }

    var player = FindObjectOfType<Player>();

    player.IncreasePlayerStats(itemData.healthValue, itemData.sleepValue, itemData.foodValue, itemData.waterValue);
  }
}