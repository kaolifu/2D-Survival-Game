using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class ItemData : ScriptableObject
{
  public string id;
  public string itemName;
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
}

public enum UsageType
{
  None,
  CanEquip,
  CanBuild,
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