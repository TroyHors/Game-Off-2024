// Recipe.cs
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu( fileName = "NewRecipe" , menuName = "Crafting/Recipe" )]
public class Recipe : ScriptableObject {
    public List<Item> requiredItems; // 所需物品列表
    public Item resultItem; // 合成结果物品
}
