using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IShowRecipe : MonoBehaviour, IInteractable {
    public static IShowRecipe Instance;
    [Header( "Recipe Inventories" )]
    public InventoryData_SO recipeInventoryData;  // 用于显示所有配方
    public InventoryData_SO ingredientInventoryData; // 显示选中的配方材料
    public InventoryData_SO resultInventoryData; // 显示选中的结果物品
    public List<Recipe> recipeList; // 挂载的配方列表

    [Header( "UI References" )]
    public GameObject recipePanel; // 配方界面
    public GameObject PlayerUI;

    private void Awake() {
        Instance = this;
    }
    public void Interact() {
        bool isActive = recipePanel.activeSelf;
        recipePanel.SetActive( !isActive );
        PlayerUI.SetActive( isActive );
        LoadRecipesToInventory();
    }

    private void LoadRecipesToInventory() {
        Debug.Log( 1 );
        // 清空 RecipeInventory 数据
        foreach (var item in recipeInventoryData.items) {
            item.itemData = null;
            item.amount = 0;
        }

        // 添加配方数据到 RecipeInventory
        foreach (var recipe in recipeList) {
            recipeInventoryData.AddItem( recipe.resultItem , 1 );
        }

        InventoryManager.Instance.recipeInventoryUI.RefreshUI(); // 更新界面
    }
}

