using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Fishing : MonoBehaviour, IInteractable {
    [Header( "分类及物品列表" )]
    public List<ItemData_SO> categoryA = new List<ItemData_SO>();
    public List<ItemData_SO> categoryB = new List<ItemData_SO>();
    public List<ItemData_SO> categoryC = new List<ItemData_SO>();

    [Header( "类别选择概率 (%)" )]
    [Range( 0 , 100 )] public float probabilityA = 33.3f;
    [Range( 0 , 100 )] public float probabilityB = 33.3f;
    [Range( 0 , 100 )] public float probabilityC = 33.3f;

    [Header( "物品特殊选中概率 (%)" )]
    [Range( 0 , 100 )] public float specialItemProbability = 10.0f;

    public void Interact() {
        SelectAndAddToBackpack();
    }
    public void SelectAndAddToBackpack() {
        // 确定选中的类别
        float randomCategory = UnityEngine.Random.Range( 0f , 100f );
        List<ItemData_SO> selectedCategory = null;

        if (randomCategory < probabilityA) {
            selectedCategory = categoryA;
        } else if (randomCategory < probabilityA + probabilityB) {
            selectedCategory = categoryB;
        } else {
            selectedCategory = categoryC;
        }

        if (selectedCategory == null || selectedCategory.Count == 0) {
            Debug.LogWarning( "选中的分类为空或没有物品！" );
            return;
        }

        // 在选中的类别中随机选出一个物品
        float randomItem = UnityEngine.Random.Range( 0f , 100f );
        ItemData_SO selectedItem = null;

        foreach (var item in selectedCategory) {
            if (UnityEngine.Random.Range( 0f , 100f ) < specialItemProbability) {
                selectedItem = item; // 特殊概率选中
                break;
            }
        }

        // 如果没有特殊物品被选中，则从普通物品中随机选择
        if (selectedItem == null) {
            selectedItem = selectedCategory[ UnityEngine.Random.Range( 0 , selectedCategory.Count ) ];
        }

        // 将选中的物品添加到背包
        InventoryManager.Instance.inventoryData.AddItem( selectedItem , 1 );
        Debug.Log( $"选中物品：{selectedItem.itemName}，添加到背包中！" );
    }

}
