using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
  public WeaponType GetCurrentWeaponType()
  {
    if (transform.childCount == 0)
    {
      return WeaponType.None;
    }
    else
    {
      var currentWeapon = transform.GetChild(0);
      return currentWeapon.GetComponent<Weapon>().weaponData.weaponType;
    }
  }

  public void EquipWeapon(GameObject weaponObj)
  {
    RemoveWeapon();

    Instantiate(weaponObj, transform);
  }

  public void RemoveWeapon()
  {
    foreach (Transform child in transform)
    {
      Destroy(child.gameObject);
    }
  }
}