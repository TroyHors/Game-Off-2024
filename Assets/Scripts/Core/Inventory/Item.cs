// Item.cs
using UnityEngine;

[CreateAssetMenu( fileName = "NewItem" , menuName = "Inventory/Item" )]
public class Item : ScriptableObject {
    [Header( "基本信息" )]
    public string itemName;      // 物品名称
    public Sprite icon;          // 物品图标
    public string description;   // 物品描述

    [Header( "属性" )]
    public int maxStack = 1;     // 最大堆叠数量

    // 您可以根据需要添加更多属性，例如：
    // public float weight;
    // public ItemType type;
    // public GameObject prefab;
}
