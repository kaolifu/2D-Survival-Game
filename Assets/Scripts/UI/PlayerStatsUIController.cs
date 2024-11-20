using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerStatsUIController : MonoBehaviour
{
  // public GameObject healthHolder;
  // public GameObject sleepHolder;
  // public GameObject foodHolder;
  // public GameObject waterHolder;
  private List<GameObject> _holders = new List<GameObject>();
  private List<Image> _fills = new List<Image>();

  private Player _player;

  private void Awake()
  {
    _player = FindObjectOfType<Player>();

    foreach (Transform child in transform)
    {
      _holders.Add(child.gameObject);
    }

    var holdersCount = _holders.Count;
    for (int i = 0; i < holdersCount; i++)
    {
      _fills.Add(_holders[i].transform.Find("Fill").GetComponent<Image>());
    }
  }

  private void Update()
  {
    _fills[0].fillAmount = (float)_player.currentHealth / _player.maxHealth;
    _fills[1].fillAmount = (float)_player.sleepPoint / 100;
    _fills[2].fillAmount = (float)_player.foodPoint / 100;
    _fills[3].fillAmount = (float)_player.waterPoint / 100;
  }
}