using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingList", menuName = "List/BuildingList", order = 1)]
public class BuildingListSO : ScriptableObject
{
  public List<GameObject> BuildingList = new List<GameObject>();
}