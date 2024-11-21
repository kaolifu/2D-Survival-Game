using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
  public GameObject axe;
  public GameObject sword;
  public WeaponController weaponController;

  // public void ChangeWeapon()
  // {
  //   if (weaponController.transform.childCount == 0)
  //   {
  //     weaponController.EquipWeapon(axe);
  //   }
  //   else if (weaponController.GetCurrentWeaponType() == WeaponType.Axe)
  //   {
  //     weaponController.EquipWeapon(sword);
  //   }
  //   else if (weaponController.GetCurrentWeaponType() == WeaponType.Sword)
  //   {
  //     weaponController.RemoveWeapon();
  //   }
  // }
}