using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
  public string id;
  public string itemName;
  public string itemDescription;
  public bool collectable;
  public GameObject prefab;

  public Sprite icon;

  public UsageType usageType;

  public WeaponType weaponType;
  public int damage;

  public InteractType interactType;
  public int gatherPoint;
  public ItemData contentItem;
  public int contentItemCount;

  public int healthValue;
  public int sleepValue;
  public int foodValue;
  public int waterValue;
}

public enum UsageType
{
  None,
  CanEquip,
  CanBuild,
  CanUse,
}

public enum WeaponType
{
  None,
  Sword,
  Axe,
  Pickaxe,
}

public enum InteractType
{
  None,
  Tree,
  Stone,
}