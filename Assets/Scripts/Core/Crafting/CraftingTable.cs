// CraftingTable.cs
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable {
    public GameObject craftingUI; // 合成界面UI

    public void Interact() {
        if (craftingUI != null) {
            bool isActive = craftingUI.activeSelf;
            craftingUI.SetActive( !isActive );
            // 暂停或恢复游戏
            Time.timeScale = isActive ? 1f : 0f;
        } else {
            Debug.LogError( "CraftingUI is not assigned in CraftingTable." );
        }
    }
}
