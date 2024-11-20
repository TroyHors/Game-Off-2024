using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Image icon; // 显示物品图标的Image组件
    public Image highlightImage; // 用于高亮显示的Image组件
    public Item item; // 当前格子中的物品
    public int slotIndex; // 背包中的索引

    private Transform originalParent; // 原始父物体，用于在拖动时保持引用
    private Canvas canvas; // 用于设置拖动时物体在最上层
    private CanvasGroup canvasGroup;

    void Start() {
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (highlightImage != null) {
            highlightImage.enabled = false;
        }
    }

    public void Setup( Item newItem , int index ) {
        item = newItem;
        slotIndex = index;
        if (item != null && item.itemIcon != null) {
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        } else {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    // 开始拖动
    public void OnBeginDrag( PointerEventData eventData ) {
        if (item == null) return;

        originalParent = transform.parent;
        transform.SetParent( canvas.transform ); // 将拖动的物品提升到Canvas顶层
        canvasGroup.blocksRaycasts = false; // 允许穿透
    }

    // 拖动中
    public void OnDrag( PointerEventData eventData ) {
        if (item == null) return;

        transform.position = eventData.position;

        // 动态高亮悬停的格子
        InventorySlot currentHoverSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<InventorySlot>();
        ClearAllHighlights();
        if (currentHoverSlot != null && currentHoverSlot != this) {
            currentHoverSlot.Highlight( true );
        }
    }

    // 结束拖动
    public void OnEndDrag( PointerEventData eventData ) {
        canvasGroup.blocksRaycasts = true;

        // 尝试找到放置的目标格子
        InventorySlot targetSlot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<InventorySlot>();

        if (targetSlot != null && targetSlot != this) {
            // 交换物品
            Inventory.instance.SwapItems( this.slotIndex , targetSlot.slotIndex );
            targetSlot.Highlight( false );
        } else {
            // 如果没有放置到有效的格子，返回原位
            transform.SetParent( originalParent );
            transform.localPosition = Vector3.zero;
        }

        // 清除所有高亮
        ClearAllHighlights();
    }

    public void Highlight( bool isValid ) {
        if (highlightImage != null) {
            highlightImage.enabled = isValid;
        }
    }

    private void ClearAllHighlights() {
        InventorySlot[] allSlots = FindObjectsOfType<InventorySlot>();
        foreach (InventorySlot slot in allSlots) {
            slot.Highlight( false );
        }
    }
}
