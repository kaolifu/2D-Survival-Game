using UnityEngine;
using UnityEngine.Rendering;

public class WeaponSortingOrderController : MonoBehaviour
{
  public GameObject weaponPosition;

  public void SetSortingOrder(int order)
  {
    SortingGroup sortingGroup = weaponPosition.GetComponentInChildren<SortingGroup>();
    if (sortingGroup != null)
    {
      sortingGroup.sortingOrder = order;
    }
  }
}