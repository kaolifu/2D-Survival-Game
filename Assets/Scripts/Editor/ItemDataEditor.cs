using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
  public override void OnInspectorGUI()
  {
    ItemData item = (ItemData)target;

    EditorGUILayout.LabelField("基本信息", EditorStyles.boldLabel);
    item.id = EditorGUILayout.TextField("ID", item.id);
    item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
    item.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", item.prefab, typeof(GameObject), false);
    item.collectable = EditorGUILayout.Toggle("Collectable", item.collectable);

    if (item.collectable)
    {
      EditorGUILayout.Space();
      EditorGUILayout.LabelField("背包", EditorStyles.boldLabel);
      item.icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.icon, typeof(Sprite), false);

      item.usageType = (UsageType)EditorGUILayout.EnumPopup("Usage Type", item.usageType);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("武器", EditorStyles.boldLabel);
      if (item.usageType == UsageType.CanEquip)
      {
        item.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", item.weaponType);
        item.damage = EditorGUILayout.IntField("Damage", item.damage);
      }
    }
    else
    {
      EditorGUILayout.Space();
      EditorGUILayout.LabelField("可采集场景物品", EditorStyles.boldLabel);
      item.interactType = (InteractType)EditorGUILayout.EnumPopup("Interact Type", item.interactType);
      item.gatherPoint = EditorGUILayout.IntField("Gather Point", item.gatherPoint);
      item.contentItem =
        (ItemData)EditorGUILayout.ObjectField("Content Item", item.contentItem, typeof(ItemData), false);
      item.contentItemCount = EditorGUILayout.IntField("Content Item Count", item.contentItemCount);
    }

    if (GUI.changed)
    {
      EditorUtility.SetDirty(item);
    }
  }
}