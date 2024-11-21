using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gather : MonoBehaviour
{
  private ItemData _itemData;
  private Animator _animator;
  private bool _isGathered;

  private WeaponController _weaponController;

  private void Awake()
  {
    _weaponController = FindObjectOfType<WeaponController>();
    _animator = GetComponent<Animator>();
  }

  private void Start()
  {
    _itemData = GetComponent<ItemController>().itemData;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Damage"))
    {
      if (_itemData.interactType == InteractType.Tree && _weaponController.GetCurrentWeaponType() == WeaponType.Axe)
      {
        BeGathered();
      }
      else if (_itemData.interactType == InteractType.Stone &&
               _weaponController.GetCurrentWeaponType() == WeaponType.Pickaxe)
      {
        BeGathered();
      }
    }
  }

  private void BeGathered()
  {
    if (!_isGathered)
    {
      _animator.SetTrigger("Shake");

      _itemData.gatherPoint -= _weaponController.GetCurrentWeaponDamage();
    }

    if (_itemData.gatherPoint <= 0)
    {
      _isGathered = true;
      _animator.SetBool("IsGathered", _isGathered);

      Invoke(nameof(SpawnContentItems), 1.5f);
      Destroy(gameObject, 1.5f);
    }
  }

  private void SpawnContentItems()
  {
    for (int i = 0; i < _itemData.contentItemCount; i++)
    {
      Vector2 spawnPos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) +
                         (Vector2)transform.position;
      Instantiate(_itemData.contentItem.prefab, spawnPos, _itemData.contentItem.prefab.transform.rotation);
    }
  }
}