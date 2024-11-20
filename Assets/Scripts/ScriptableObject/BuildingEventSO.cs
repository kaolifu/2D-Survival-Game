using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BuildingEvent", menuName = "EventSO/BuildingEvent")]
public class BuildingEventSO : ScriptableObject
{
  public UnityAction<GameObject> OnEventRaised;

  public void RaiseEvent(GameObject target)
  {
    OnEventRaised?.Invoke(target);
  }
}