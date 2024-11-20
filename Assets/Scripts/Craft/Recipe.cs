using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Craft/Recipe")]
public class Recipe : ScriptableObject
{
  public string id;
  public string recipeName;
  public Sprite icon;
  public ItemEntry[] items;
  public ItemEntry achievement;
}