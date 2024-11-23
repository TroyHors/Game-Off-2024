using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class IFishing : MonoBehaviour, IInteractable {
    public GameObject FishingUI; // 合成界面UI
    public GameObject inventoryUI;
    public void Interact() {
        if (FishingUI != null) {
            bool isActive = FishingUI.activeSelf;
            FishingUI.SetActive( !isActive );
            inventoryUI.SetActive( !isActive );
            // 暂停或恢复游戏
            Time.timeScale = isActive ? 1f : 0f;
        } else {
            Debug.LogError( "CraftingUI is not assigned in CraftingTable." );
        }
    }

}
