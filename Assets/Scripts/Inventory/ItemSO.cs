using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
  public string id;
  public string itemName;
  public int value;
  public Sprite icon;
  public ItemUsageType usageType;
}

public enum ItemUsageType
{
  None,
  CanUse,
  CanBuild,
  CanEquip
}