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
                } else {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }

                switch (targetHolder.slotType) {
                    case SlotType.BAG:
                    HandleBonusEffect( currentHolder , targetHolder , SlotType.BAG );
                    SwapItem();
                    break;

                    case SlotType.CRAFT:
                    HandleBonusEffect( currentHolder , targetHolder , SlotType.CRAFT );
                    SwapItem();
                    break;

                    case SlotType.RESULT_C:
                    HandleBonusEffect( currentHolder , targetHolder , SlotType.RESULT_C );
                    break;

                    case SlotType.BLEND:
                    HandleBonusEffect( currentHolder , targetHolder , SlotType.BLEND );
                    SwapItem();
                    break;

                    case SlotType.RESULT_B:
                    HandleBonusEffect( currentHolder , targetHolder , SlotType.RESULT_B );
                    break;

                    case SlotType.EQUIPMENT:
                    if (currentItemUI.Bag.items[ currentItemUI.Index ].itemData.itemType == ItemType.Eq) {
                        HandleBonusEffect( currentHolder , targetHolder , SlotType.EQUIPMENT );
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

    private void HandleBonusEffect( SlotHolder currentHolder , SlotHolder targetHolder , SlotType targetSlotType ) {
        var currentItem = currentHolder.itemUI.Bag.items[ currentHolder.itemUI.Index ];

        // 如果当前物品没有特殊效果，直接返回
        if (currentItem.itemData == null || currentItem.itemData.bonusEffect == null) {
            Debug.Log( 1 );
            return;
        }

        // 如果移出 SlotType.EQUIPMENT，则移除效果
        if (currentHolder.slotType == SlotType.EQUIPMENT && targetSlotType != SlotType.EQUIPMENT) {
            currentItem.itemData.RemoveBonusEffect();
            Debug.Log( $"移除 {currentItem.itemData.itemName} 的特殊效果" );
        }

        // 如果移入 SlotType.EQUIPMENT，则触发效果
        if (targetSlotType == SlotType.EQUIPMENT) {
            currentItem.itemData.ApplyBonusEffect();
            Debug.Log( $"触发 {currentItem.itemData.itemName} 的特殊效果" );
        }
    }

}
