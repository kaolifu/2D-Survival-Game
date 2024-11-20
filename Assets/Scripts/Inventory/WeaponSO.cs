using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Item/Weapon")]
public class WeaponSO : ItemSO
{
  public WeaponType weaponType;
}

public enum WeaponType
{
  None,
  Axe,
  Sword
}