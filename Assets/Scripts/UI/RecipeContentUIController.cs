using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeContentUIController : MonoBehaviour
{
  private Transform ingredient1;
  private TextMeshProUGUI ingredient1Name;
  private Image ingredient1Icon;
  private TextMeshProUGUI ingredient1Amount;

  private Transform ingredient2;
  private TextMeshProUGUI ingredient2Name;
  private Image ingredient2Icon;
  private TextMeshProUGUI ingredient2Amount;

  private Transform achievement;
  private TextMeshProUGUI achievementName;
  private Image achievementIcon;
  private TextMeshProUGUI achievementAmount;

  [Header("监听")] public RecipeEventSO recipeEvent;

  private void Awake()
  {
    ingredient1 = transform.GetChild(1);
    ingredient1Name = ingredient1.Find("Name").GetComponent<TextMeshProUGUI>();
    ingredient1Icon = ingredient1.Find("Icon").GetComponent<Image>();
    ingredient1Amount = ingredient1.Find("Amount").GetComponentInChildren<TextMeshProUGUI>();

    ingredient2 = transform.GetChild(2);
    ingredient2Name = ingredient2.Find("Name").GetComponent<TextMeshProUGUI>();
    ingredient2Icon = ingredient2.Find("Icon").GetComponent<Image>();
    ingredient2Amount = ingredient2.Find("Amount").GetComponentInChildren<TextMeshProUGUI>();

    achievement = transform.GetChild(3);
    achievementName = achievement.Find("Name").GetComponent<TextMeshProUGUI>();
    achievementIcon = achievement.Find("Icon").GetComponent<Image>();
    achievementAmount = achievement.Find("Amount").GetComponentInChildren<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    recipeEvent.OnEventRaised += OnRecipeClickedEvent;
  }

  private void OnDisable()
  {
    recipeEvent.OnEventRaised -= OnRecipeClickedEvent;
  }

  private void OnRecipeClickedEvent(Recipe recipe)
  {
    ingredient1Name.text = recipe.items[0].itemData.itemName;
    ingredient1Icon.sprite = recipe.items[0].itemData.icon;
    ingredient1Amount.text = recipe.items[0].amount.ToString();

    ingredient2Name.text = recipe.items[1].itemData.itemName;
    ingredient2Icon.sprite = recipe.items[1].itemData.icon;
    ingredient2Amount.text = recipe.items[1].amount.ToString();

    achievementName.text = recipe.achievement.itemData.itemName;
    achievementIcon.sprite = recipe.achievement.itemData.icon;
    achievementAmount.text = recipe.achievement.amount.ToString();
  }
}