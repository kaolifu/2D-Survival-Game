using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeSlotController : MonoBehaviour
{
  public Recipe recipe;

  [Header("广播")] public RecipeEventSO recipeEvent;

  public void OnMouseDown()
  {
    CraftManager.Instance.currentRecipe = recipe;
    recipeEvent.RaiseEvent(recipe);
  }
}