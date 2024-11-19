// InventorySlot.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Image icon;
    public Text quantityText;
    public Button removeButton;

    private Item currentItem;
    private int quantity;

    private Transform originalParent;
    private CanvasGroup canvasGroup;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void AddItem( Item newItem , int qty ) {
        currentItem = newItem;
        quantity = qty;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
        removeButton.interactable = true;
    }

    public void ClearSlot() {
        currentItem = null;
        quantity = 0;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
        removeButton.interactable = false;
    }

    public void RemoveItem() {
        if (currentItem != null) {
            Inventory.instance.RemoveItem( currentItem );
        }
    }

    public Item GetItem() {
        return currentItem;
    }

    // 拖动事件处理
    public void OnBeginDrag( PointerEventData eventData ) {
        if (currentItem == null)
            return;

        originalParent = transform.parent;
        transform.SetParent( originalParent.parent.parent.parent.parent ); // 移动到 Canvas 下以确保在拖动时显示在最上层
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag( PointerEventData eventData ) {
        if (currentItem == null)
            return;

        transform.position += (Vector3) eventData.delta;
    }

    public void OnEndDrag( PointerEventData eventData ) {
        canvasGroup.blocksRaycasts = true;
        GameObject dropObject = eventData.pointerCurrentRaycast.gameObject;

        if (dropObject == null) {
            // 返回原位
            transform.SetParent( originalParent );
            transform.position = originalParent.position;
            return;
        }

        IDropHandlerCustom dropHandler = dropObject.GetComponent<IDropHandlerCustom>();
        if (dropHandler != null) {
            dropHandler.OnDropItem( this );
        } else {
            // 返回原位
            transform.SetParent( originalParent );
            transform.position = originalParent.position;
        }
    }
}
