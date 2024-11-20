using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RecipeEvent", menuName = "EventSO/RecipeEvent")]
public class RecipeEventSO : ScriptableObject
{
  public UnityAction<Recipe> OnEventRaised;

  public void RaiseEvent(Recipe recipe)
  {
    OnEventRaised?.Invoke(recipe);
  }
}