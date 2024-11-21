using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "itemEvent", menuName = "EventSO/itemEvent")]
public class ItemEventSO : ScriptableObject
{
  public UnityAction<GameObject> OnEventRaised;

  public void RaiseEvent(GameObject target)
  {
    OnEventRaised?.Invoke(target);
  }
}