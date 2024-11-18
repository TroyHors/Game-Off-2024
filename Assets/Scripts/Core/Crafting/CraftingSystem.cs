// CraftingSystem.cs
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour {
    public List<Recipe> recipes; // 所有合成配方

    public bool Craft( List<Item> inputItems , out Item result ) {
        foreach (Recipe recipe in recipes) {
            if (IsMatch( recipe.requiredItems , inputItems )) {
                result = recipe.resultItem;
                return true;
            }
        }
        result = null;
        return false;
    }

    private bool IsMatch( List<Item> required , List<Item> input ) {
        if (required.Count != input.Count)
            return false;

        // 创建物品名称的字典
        Dictionary<string , int> requiredDict = new Dictionary<string , int>();
        foreach (Item item in required) {
            if (requiredDict.ContainsKey( item.itemName ))
                requiredDict[ item.itemName ]++;
            else
                requiredDict[ item.itemName ] = 1;
        }

        foreach (Item item in input) {
            if (requiredDict.ContainsKey( item.itemName )) {
                requiredDict[ item.itemName ]--;
                if (requiredDict[ item.itemName ] < 0)
                    return false;
            } else {
                return false;
            }
        }

        foreach (var pair in requiredDict) {
            if (pair.Value != 0)
                return false;
        }

        return true;
    }
}
