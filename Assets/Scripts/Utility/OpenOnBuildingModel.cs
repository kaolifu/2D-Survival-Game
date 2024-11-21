using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OpenOnBuildingModel : MonoBehaviour
{
  [FormerlySerializedAs("buildingEvent")] [Header("监听")] public ItemEventSO itemEvent;
  public VoidEventSO buildingOverEvent;
  public GameObject[] openObjs;

  private void OnEnable()
  {
    itemEvent.OnEventRaised += OpenObjs;
    buildingOverEvent.OnEventRaised += CloseObjs;
  }


  private void OnDisable()
  {
    itemEvent.OnEventRaised -= OpenObjs;
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