using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {
    public Button craftButton;
    public List<Recipe> allRecipes; // 所有配方列表
    public CraftingData_SO craftingData;
    
    public void TryCraft() {
        // 寻找符合的配方
        Recipe matchingRecipe = craftingData.FindMatchingRecipe( allRecipes );

        if (matchingRecipe != null) {
            craftingData.Craft( matchingRecipe );
        } else {
            return;
        }
    }
    void Start() {
        craftButton.onClick.AddListener( () => TryCraft() );
        InventoryManager.Instance.resultUI_C.RefreshUI();
        InventoryManager.Instance.resultUI_B.RefreshUI();

    }
}
