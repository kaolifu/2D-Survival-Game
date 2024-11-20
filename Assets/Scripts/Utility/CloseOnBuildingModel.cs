using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOnBuildingModel : MonoBehaviour
{
  [Header("监听")] public BuildingEventSO BuildingEvent;

  private void OnEnable()
  {
    BuildingEvent.OnEventRaised += CloseSelf;
  }

  private void OnDisable()
  {
    BuildingEvent.OnEventRaised -= CloseSelf;
  }

  private void CloseSelf(GameObject arg0)
  {
    gameObject.SetActive(false);
  }
}