using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemDataEvent", menuName = "EventSO/ItemDataEvent")]
public class ItemDataEventSO : ScriptableObject
{
  public UnityAction<ItemData> OnEventRaised;

  public void RaiseEvent(ItemData item)
  {
    OnEventRaised?.Invoke(item);
  }
}