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
    item.itemDescription = EditorGUILayout.TextArea(item.itemDescription, GUILayout.Height(100));
    item.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", item.prefab, typeof(GameObject), false);
    item.collectable = EditorGUILayout.Toggle("Collectable", item.collectable);

    if (item.collectable)
    {
      EditorGUILayout.Space();
      EditorGUILayout.LabelField("背包", EditorStyles.boldLabel);
      item.icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.icon, typeof(Sprite), false);

      item.usageType = (UsageType)EditorGUILayout.EnumPopup("Usage Type", item.usageType);

      if (item.usageType == UsageType.CanEquip)
      {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("武器", EditorStyles.boldLabel);
        item.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", item.weaponType);
        item.damage = EditorGUILayout.IntField("Damage", item.damage);
      }
      else if (item.usageType == UsageType.CanUse)
      {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("可使用物品", EditorStyles.boldLabel);
        item.healthValue = EditorGUILayout.IntField("Health", item.healthValue);
        item.sleepValue = EditorGUILayout.IntField("Sleep", item.sleepValue);
        item.foodValue = EditorGUILayout.IntField("Food", item.foodValue);
        item.waterValue = EditorGUILayout.IntField("Water", item.waterValue);
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