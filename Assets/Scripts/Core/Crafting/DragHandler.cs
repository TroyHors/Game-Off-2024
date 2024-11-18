// DragHandler.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public CraftingUI craftingUI;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    void Awake() {
        canvas = FindObjectOfType<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag( PointerEventData eventData ) {
        originalPosition = rectTransform.position;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag( PointerEventData eventData ) {
        rectTransform.position += (Vector3) eventData.delta;
    }

    public void OnEndDrag( PointerEventData eventData ) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // 检查是否放在合成区域
        RectTransform craftingArea = craftingUI.craftingArea.GetComponent<RectTransform>();
        if (RectTransformUtility.RectangleContainsScreenPoint( craftingArea , eventData.position , eventData.pressEventCamera )) {
            // 将物品放入合成区域
            Item item = GetComponent<InventorySlot>().GetItem();
            craftingUI.PlaceItem( item );
            // 从背包中移除物品
            Inventory.instance.RemoveItem( item );
            // 销毁背包槽位
            Destroy( gameObject );
        } else {
            // 返回原位
            rectTransform.position = originalPosition;
        }
    }
}
