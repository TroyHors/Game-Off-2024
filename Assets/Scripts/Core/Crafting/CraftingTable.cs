// CraftingTable.cs
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable {
    public GameObject craftingUI; // 合成界面UI

    public void Interact() {
        // 显示合成界面
        craftingUI.SetActive( true );
        // 暂停游戏或锁定玩家控制（可选）
        Time.timeScale = 0f;
    }

    // 关闭合成界面的方法
    public void CloseCraftingUI() {
        craftingUI.SetActive( false );
        Time.timeScale = 1f;
    }
}
