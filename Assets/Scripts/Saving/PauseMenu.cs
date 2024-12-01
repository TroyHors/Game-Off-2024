using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public GameObject menuPanel; // 菜单界面
    public Image statusImage; // 显示玩家 Mutation 状态的图片
    public List<Sprite> mutationStateImages; // 存放五种 Mutation 状态的图片

    private bool isPaused = false;

    private void Update() {
        if (Input.GetKeyDown( KeyCode.Escape )) {
            ToggleMenu();
        }
    }

    private void ToggleMenu() {
        isPaused = !isPaused;
        menuPanel.SetActive( isPaused );
        Time.timeScale = isPaused ? 0 : 1; // 暂停/恢复游戏

        if (isPaused) {
            UpdateStatusImage(); // 更新状态图片
        }
    }

    public void SaveGame() {
        GameData data = new GameData {
            playerPosition = PlayerController.Instance.transform.position ,
            dayCount = GameManager.Instance.dayCount ,
            inventoryItems = InventoryManager.Instance.inventoryData.items ,
            storyStatus = new Dictionary<string , bool>()
        };

        foreach (var storyNode in GameManager.Instance.opening.storyNodes) {
            data.storyStatus[ storyNode.ID ] = false; // 初始化为未完成
        }

        SaveManager.SaveGame( data );
    }

    public void ResetGame() {
        SaveManager.ResetSave();
        // 重置游戏状态
        GameManager.Instance.dayCount = 0;
        PlayerController.Instance.transform.position = Vector3.zero;

        foreach (var item in InventoryManager.Instance.inventoryData.items) {
            item.itemData = null;
            item.amount = 0;
        }

        foreach (var storyNode in GameManager.Instance.opening.storyNodes) {
            GameManager.Instance.opening.storyIndex[ storyNode.ID ] = storyNode; // 重置剧情
        }

        // 刷新 UI
        InventoryManager.Instance.inventoryUI.RefreshUI();
        GameManager.Instance.UpdateDateUI();
        Debug.Log( "Game reset." );
    }

    public void ResumeGame() {
        ToggleMenu();
    }

    private void UpdateStatusImage() {
        if (statusImage == null || mutationStateImages == null || mutationStateImages.Count == 0) {
            Debug.LogWarning( "Status image or mutation state images not set." );
            return;
        }

        int currentMutationLevel = GetCurrentMutationLevel();
        if (currentMutationLevel >= 0 && currentMutationLevel < mutationStateImages.Count) {
            statusImage.sprite = mutationStateImages[ currentMutationLevel ];
        } else {
            Debug.LogWarning( "Mutation level out of range or images not set." );
        }
    }

    private int GetCurrentMutationLevel() {
        int currentMutation = PlayerController.Instance.currentMutation;
        List<int> thresholds = PlayerController.Instance.mutationThresholds;

        for (int i = thresholds.Count - 1 ; i >= 0 ; i--) {
            if (currentMutation >= thresholds[ i ]) {
                return i; // 返回当前 Mutation 等级索引
            }
        }
        return 0; // 默认第一级
    }
}
