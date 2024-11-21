using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
  private ItemData _currentWeaponData;

  public WeaponType GetCurrentWeaponType()
  {
    if (transform.childCount == 0)
    {
      return WeaponType.None;
    }
    else
    {
      _currentWeaponData = GetComponentInChildren<ItemController>().itemData;
      return _currentWeaponData.weaponType;
    }
  }

  public int GetCurrentWeaponDamage()
  {
    if (transform.childCount == 0)
    {
      return 0;
    }
    else
    {
      _currentWeaponData = GetComponentInChildren<ItemController>().itemData;
      return _currentWeaponData.damage;
    }
  }

  public void EquipWeapon(GameObject weaponObj)
  {
    RemoveWeapon();

    var currentWeapon = Instantiate(weaponObj, transform);

    //关闭武器的拾取状态
    currentWeapon.GetComponent<ItemController>().itemData.collectable = false;

    //关闭拾取范围
    if (currentWeapon.GetComponent<Collider2D>())
    {
      currentWeapon.GetComponent<Collider2D>().enabled = false;
    }
  }

  public void RemoveWeapon()
  {
    if (transform.childCount > 0)
    {
      var currentItem = transform.GetChild(0).GetComponent<ItemController>().itemData;
      InventoryManager.Instance.AddItem(currentItem, 1);
    }

    foreach (Transform child in transform)
    {
      Destroy(child.gameObject);
    }
  }
}