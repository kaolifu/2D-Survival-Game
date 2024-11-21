using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
  public static CraftManager Instance;

  public List<Recipe> recipes = new List<Recipe>();

  public Transform recipeContainer;
  public GameObject recipeSlot;

  public Recipe currentRecipe;

  public UnityEvent onCraftFailed;
  public UnityEvent onCraftSuccess;

  [Header("广播")] public RecipeEventSO recipeEventSO;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void ListRecipes()
  {
    foreach (Transform child in recipeContainer)
    {
      Destroy(child.gameObject);
    }

    foreach (var recipe in recipes)
    {
      var recipeSlotObj = Instantiate(recipeSlot, recipeContainer);
      var recipeName = recipeSlotObj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
      var recipeIcon = recipeSlotObj.transform.Find("Icon").GetComponent<Image>();

      recipeName.text = recipe.recipeName;
      recipeIcon.sprite = recipe.icon;

      recipeSlotObj.GetComponent<RecipeSlotController>().recipe = recipe;
    }

    currentRecipe = recipes[0];
    recipeEventSO.RaiseEvent(currentRecipe);
  }

  public void Craft()
  {
    // 设置临时变量查看当前Recipe一共需要多少个元素，
    var ingredientCount = currentRecipe.items.Length;
    var holdIngredientCount = 0;

    foreach (var itemEntry in currentRecipe.items)
    {
      // 当前Recipe中的每个元素都和背包中的同个元素比较数量，如果背包中的元素数量大于需求数量，则holdIngredientCount+1
      var result = InventoryManager.Instance.items.Find(i => i.itemData == itemEntry.itemData);
      if (result != null && result.amount >= itemEntry.amount)
      {
        holdIngredientCount++;
      }
    }

    // holdIngredientCount如果大于需求，这表示材料足够制作
    if (holdIngredientCount >= ingredientCount)
    {
      foreach (var itemEntry in currentRecipe.items)
      {
        InventoryManager.Instance.RemoveItem(itemEntry.itemData, itemEntry.amount);
      }

      InventoryManager.Instance.AddItem(currentRecipe.achievement.itemData, currentRecipe.achievement.amount);

      onCraftSuccess?.Invoke();
    }
    else
    {
      onCraftFailed?.Invoke();
    }
  }
}