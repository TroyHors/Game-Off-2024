using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New FishingData" , menuName = "Interact/Fishing Data" )]
public class FishingData_SO : InventoryData_SO {
    public InventoryData_SO baitsArea; // 放置的 Baits

    [Header( "分类及物品列表" )]
    public List<ItemData_SO> categoryLv1 = new List<ItemData_SO>();
    public List<ItemData_SO> categoryLv2 = new List<ItemData_SO>();
    public List<ItemData_SO> categoryLv3 = new List<ItemData_SO>();

    public void Fishing() {
        // 检查是否有 Bait
        var baitItem = baitsArea.items.Find( item => item.itemData != null );
        if (baitItem == null || baitItem.itemData.itemType != ItemType.Bait) {
            Debug.LogWarning( "没有有效的 Bait，无法进行钓鱼！" );
            return;
        }

        // 获取 Bait 的等级和目标鱼
        int baitLevel = baitItem.itemData.baitsLevel;
        ItemData_SO targetFish = baitItem.itemData.targetFish;

        // 确定钓鱼概率
        float lv1Probability = 0f, lv2Probability = 0f, lv3Probability = 0f;

        if (baitLevel == 1) {
            lv1Probability = 80f;
            lv2Probability = 18f;
            lv3Probability = 2f;
        } else if (baitLevel == 2) {
            lv1Probability = 20f;
            lv2Probability = 70f;
            lv3Probability = 10f;
        } else if (baitLevel == 3) {
            lv1Probability = 10f;
            lv2Probability = 30f;
            lv3Probability = 60f;
        }

        // 随机选择等级
        float randomCategory = UnityEngine.Random.Range( 0f , 100f );
        List<ItemData_SO> selectedCategory = null;

        if (randomCategory < lv1Probability) {
            selectedCategory = categoryLv1;
        } else if (randomCategory < lv1Probability + lv2Probability) {
            selectedCategory = categoryLv2;
        } else if(randomCategory < lv1Probability+lv2Probability+lv3Probability){
            selectedCategory = categoryLv3;
        }

        if (selectedCategory == null || selectedCategory.Count == 0) {
            Debug.LogWarning( "选中的分类为空或没有物品！" );
            return;
        }

        // 修正概率：目标鱼 90%，其余鱼均分 10%
        List<ItemData_SO> remainingFish = new List<ItemData_SO>( selectedCategory );
        if (targetFish != null && selectedCategory.Contains( targetFish )) {
            remainingFish.Remove( targetFish );
        }

        float targetFishProbability = 90f;
        float otherFishProbability = ( remainingFish.Count > 0 ) ? 10f / remainingFish.Count : 0f;

        // 按概率随机选鱼
        float randomFish = UnityEngine.Random.Range( 0f , 100f );
        ItemData_SO selectedItem = null;

        if (randomFish < targetFishProbability && targetFish != null) {
            selectedItem = targetFish;
        } else {
            float cumulativeProbability = targetFishProbability;
            foreach (var fish in remainingFish) {
                cumulativeProbability += otherFishProbability;
                if (randomFish < cumulativeProbability) {
                    selectedItem = fish;
                    break;
                }
            }
        }

        // 如果未选中任何鱼，回退到默认随机
        if (selectedItem == null) {
            selectedItem = selectedCategory[ UnityEngine.Random.Range( 0 , selectedCategory.Count ) ];
        }

        // 将选中的物品添加到背包
        InventoryManager.Instance.inventoryData.AddItem( selectedItem , 1 );
        PlayerController.Instance.UpdateHunger( -7 );
        baitItem.amount -= 1;
        if (baitItem.amount <= 0) {
            baitItem.itemData = null; 
        }
        InventoryManager.Instance.fishingUI.RefreshUI();
        InventoryManager.Instance.inventoryUI.RefreshUI();
        Debug.Log( $"钓鱼成功！获得了 {selectedItem.itemName}" );
    }
}