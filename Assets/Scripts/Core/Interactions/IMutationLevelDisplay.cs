using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMutationLevelDisplay : MonoBehaviour, IInteractable {
    [Header( "UI Elements" )]
    public GameObject displayPanel; // 用于显示图片的面板
    public Image mutationImage; // 用于显示 Mutation 等级图片的 UI 元素

    [Header( "Mutation Level Images" )]
    public List<Sprite> mutationLevelImages; // 对应每个 Mutation 等级的图片

    public void Interact() {
        // 获取玩家的当前 Mutation 等级
        int mutationLevelIndex = PlayerController.Instance.GetCurrentMutationLevelIndex();

        if (mutationLevelIndex < mutationLevelImages.Count) {
            // 显示对应的图片
            mutationImage.sprite = mutationLevelImages[ mutationLevelIndex ];
            displayPanel.SetActive( true ); // 打开显示面板
        } else {
            Debug.LogWarning( "No image found for the current mutation level." );
        }
    }

    private void Update() {
        // 按下 Escape 关闭面板
        if (displayPanel.activeSelf && Input.GetKeyDown( KeyCode.Escape )) {
            displayPanel.SetActive( false );
        }
    }
}
