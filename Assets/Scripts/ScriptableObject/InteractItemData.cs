using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractItemData", menuName = "Items/InteractItemData")]
public class InteractItemData : ScriptableObject
{
  public int collectPoints;
  public InteractItemType itemType;
}

public enum InteractItemType
{
  Tree,
  Stone
}