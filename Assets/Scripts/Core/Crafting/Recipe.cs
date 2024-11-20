using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Recipe" , menuName = "Crafting/Recipe" )]
public class Recipe : ScriptableObject {
    [System.Serializable]
    public class Ingredient {
        public ItemData_SO itemData;
        public int requiredAmount;
    }

    public List<Ingredient> ingredients = new List<Ingredient>();
    public ItemData_SO resultItem;
    public int resultAmount;
}
