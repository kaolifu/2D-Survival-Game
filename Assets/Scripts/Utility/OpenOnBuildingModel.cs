using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOnBuildingModel : MonoBehaviour
{
  [Header("监听")] public BuildingEventSO buildingEvent;
  public VoidEventSO buildingOverEvent;
  public GameObject[] openObjs;

  private void OnEnable()
  {
    buildingEvent.OnEventRaised += OpenObjs;
    buildingOverEvent.OnEventRaised += CloseObjs;
  }


  private void OnDisable()
  {
    buildingEvent.OnEventRaised -= OpenObjs;
    buildingOverEvent.OnEventRaised -= CloseObjs;
  }


  private void OpenObjs(GameObject arg0)
  {
    foreach (var obj in openObjs)
    {
      obj.SetActive(true);
    }
  }

  private void CloseObjs()
  {
    foreach (var obj in openObjs)
    {
      obj.SetActive(false);
    }
  }
}