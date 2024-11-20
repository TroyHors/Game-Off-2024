using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Item item; // 当前格子对应的物品
    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Start() {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag( PointerEventData eventData ) {
        originalParent = transform.parent;
        transform.SetParent( canvas.transform );
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag( PointerEventData eventData ) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag( PointerEventData eventData ) {
        transform.SetParent( originalParent );
        canvasGroup.blocksRaycasts = true;

        // 获取放置目标
        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        if (target != null && target.GetComponent<Draggable>() != null) {
            // 交换物品
            Draggable targetDraggable = target.GetComponent<Draggable>();
            SwapItems( targetDraggable );
        }
    }

    private void SwapItems( Draggable targetDraggable ) {
        // 交换物品数据
        Item temp = targetDraggable.item;
        targetDraggable.item = this.item;
        this.item = temp;

        // 更新UI
        InventoryUI inventoryUI = Inventory.instance.inventoryUI;
        inventoryUI.UpdateUI();
    }
}
