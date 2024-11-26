using System.Collections.Generic;
using UnityEngine;

public class RecipeUI : MonoBehaviour {
    public InventoryData_SO ingredientInventoryData; // 配方材料 Inventory
    public InventoryData_SO resultInventoryData; // 结果物品 Inventory

    public void DisplayRecipeDetails( Recipe recipe ) {
        // 清空数据
        ClearInventory( ingredientInventoryData );
        ClearInventory( resultInventoryData );

        // 添加材料
        foreach (var ingredient in recipe.ingredients) {
            ingredientInventoryData.AddItem( ingredient.itemData , ingredient.requiredAmount );
        }

        // 添加结果物品
        resultInventoryData.AddItem( recipe.resultItem , recipe.resultAmount );

        // 刷新 UI
        InventoryManager.Instance.ingredientInventoryUI.RefreshUI();
        InventoryManager.Instance.resultInventoryUI.RefreshUI();
    }

    private void ClearInventory( InventoryData_SO inventoryData ) {
        foreach (var item in inventoryData.items) {
            item.itemData = null;
            item.amount = 0;
        }
    }
}
