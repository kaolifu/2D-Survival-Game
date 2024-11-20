using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VectorForestScenery;
using Random = UnityEngine.Random;

public class InteractItemController : SceneryItemNew
{
  private SceneryItem _sceneryItem;
  public InteractItemData sampleData;

  private InteractItemData _data;

  public GameObject collectableItem;
  public int collectableItemCount;

  private void Awake()
  {
    _sceneryItem = GetComponent<SceneryItem>();

    _data = Instantiate(sampleData);
  }

  public void OnCollected()
  {
    _sceneryItem.Shake();

    _data.collectPoints--;
    if (_data.collectPoints <= 0)
    {
      switch (_data.itemType)
      {
        case InteractItemType.Tree:
          _sceneryItem.Fall();
          break;
      }

      Invoke(nameof(SpawnCollectableItem), 2.0f);
      Destroy(gameObject, 2.0f);
    }
  }

  private void SpawnCollectableItem()
  {
    for (int i = 0; i < collectableItemCount; i++)
    {
      Vector2 spawnPosition = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) +
                              (Vector2)transform.position;

      Instantiate(collectableItem, spawnPosition, collectableItem.transform.rotation);
    }
  }
}