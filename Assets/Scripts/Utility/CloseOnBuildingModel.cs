using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CloseOnBuildingModel : MonoBehaviour
{
  [FormerlySerializedAs("BuildingEvent")] [Header("监听")] public ItemEventSO itemEvent;

  private void OnEnable()
  {
    itemEvent.OnEventRaised += CloseSelf;
  }

  private void OnDisable()
  {
    itemEvent.OnEventRaised -= CloseSelf;
  }

  private void CloseSelf(GameObject arg0)
  {
    gameObject.SetActive(false);
  }
}