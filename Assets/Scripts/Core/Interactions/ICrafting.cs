// CraftingTable.cs
using UnityEngine;

public class ICrafting : MonoBehaviour, IInteractable {
    public GameObject craftingUI; // 合成界面UI
    public GameObject inventoryUI;
    public GameObject playerUI;

    public void Interact() {
        if (craftingUI != null) {
            bool isActive = craftingUI.activeSelf;
            craftingUI.SetActive( !isActive );
            inventoryUI.SetActive( !isActive );
            playerUI.SetActive( isActive );

            // 暂停或恢复游戏
            Time.timeScale = isActive ? 1f : 0f;
        } else {
            Debug.LogError( "CraftingUI is not assigned in CraftingTable." );
        }
    }

}
