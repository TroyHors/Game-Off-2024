using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    ItemUI currentItemUI;
    SlotHolder currentHolder;
    SlotHolder targetHolder;

    void Awake() {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }

    public void OnBeginDrag( PointerEventData eventData ) {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();
        InventoryManager.Instance.currentDrag.originalHolder = GetComponentInParent<SlotHolder>();
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform) transform.parent;

        // 记录原始数据
        transform.SetParent( InventoryManager.Instance.dragCanvas.transform , true );
    }

    public void OnDrag( PointerEventData eventData ) {
        // 跟随鼠标位置移动
        transform.position = eventData.position;
    }

    public void OnEndDrag( PointerEventData eventData ) {
        if (EventSystem.current.IsPointerOverGameObject()) {
            if (InventoryManager.Instance.CheckInInventoryUI( eventData.position ) || 
                InventoryManager.Instance.CheckInCraftingUI( eventData.position ) ||
                InventoryManager.Instance.CheckInblendingUI(eventData.position) ||
                InventoryManager.Instance.CheckInequipmentUI(eventData.position)){ 

                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>()) {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                    Debug.Log( targetHolder );
                } else {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }

                switch (targetHolder.slotType) {
                    case SlotType.BAG:
                    SwapItem();
                    break;
                    case SlotType.CRAFT:
                    SwapItem();
                    break;
                    case SlotType.RESULT_C:
                    break;
                    case SlotType.BLEND:
                    SwapItem();
                    break;
                    case SlotType.RESULT_B:
                    break;
                    case SlotType.EQUIPMENT:
                    if (currentItemUI.Bag.items[ currentItemUI.Index ].itemData.itemType == ItemType.Eq) {
                        SwapItem();
                    }
                    break;
                }

                currentHolder.UpdateItem();
                targetHolder.UpdateItem();
            }
        }

        transform.SetParent( InventoryManager.Instance.currentDrag.originalParent );
        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;
    }

    public void SwapItem() {
        if (targetHolder == currentHolder) return;
        var targetItem = targetHolder.itemUI.Bag.items[ targetHolder.itemUI.Index ];
        var tempItem = currentHolder.itemUI.Bag.items[ currentHolder.itemUI.Index ];

        bool isSameItem = tempItem.itemData == targetItem.itemData;

        if (isSameItem && targetItem.itemData.stackable) {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        } else {
            currentHolder.itemUI.Bag.items[ currentHolder.itemUI.Index ] = targetItem;
            targetHolder.itemUI.Bag.items[ targetHolder.itemUI.Index ] = tempItem;
        }
    }


}
