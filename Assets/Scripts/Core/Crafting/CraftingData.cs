using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New CraftingData" , menuName = "Interact/Crafting Data" )]
public class CraftingData_SO : InventoryData_SO {
    public static CraftingData_SO Instance;
    public InventoryData_SO resultSlot; // 合成结果的栏位数据
    [Range( 0 , 100 )] public int noConsumptionChance = 0; // 不消耗物品的概率（0-100）

    private void Awake() {
        Instance = this;
    }
    // 遍历所有配方，找到第一个符合的配方
    public Recipe FindMatchingRecipe( List<Recipe> recipes ) {
        foreach (var recipe in recipes) {
            if (CanCraft( recipe )) {
                return recipe; // 返回第一个符合的配方
            }
        }
        Debug.Log( 1 );
        return null; // 如果没有配方匹配，返回 null
    }

    // 检查是否能根据给定配方合成
    public bool CanCraft( Recipe recipe ) {
        foreach (var ingredient in recipe.ingredients) {
            bool found = false;
            foreach (var item in items) {
                if (item.itemData == ingredient.itemData && item.amount >= ingredient.requiredAmount) {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false; // 有任意一种材料不满足
        }

        return true; // 所有材料都满足要求
    }

    // 执行合成逻辑
    public void Craft( Recipe recipe ) {
        foreach (var resultItem in resultSlot.items) {
            if (resultItem.itemData != null) // 结果栏已有物品
            {
                if (!recipe.resultItem.stackable) return;
            }
        }
        if (!CanCraft( recipe )) {
            return;
        }
        if (CanCraft( recipe )) {
            // 消耗材料
            bool consumeItem = Random.Range( 0 , 100 ) >= noConsumptionChance;
            if (consumeItem) {
                foreach (var ingredient in recipe.ingredients) {
                    foreach (var item in items) {
                        if (item.itemData == ingredient.itemData) {
                            item.amount -= ingredient.requiredAmount;
                            if (item.amount <= 0)
                                item.itemData = null; // 清空材料数据
                            break;
                        }
                    }
                }
            } else {
                Debug.Log("was not consumed due to no-consumption chance!" );
            }

                // 添加合成结果到结果栏
                resultSlot.AddItem( recipe.resultItem , recipe.resultAmount );
            InventoryManager.Instance.craftingUI.RefreshUI(); // 更新合成区域的UI
            InventoryManager.Instance.inventoryUI.RefreshUI(); // 更新背包的UI
            InventoryManager.Instance.blendingUI.RefreshUI();
            InventoryManager.Instance.resultUI_C.RefreshUI();
            InventoryManager.Instance.resultUI_B.RefreshUI();
            Debug.Log( "合成成功: " );
        }
    }
}
