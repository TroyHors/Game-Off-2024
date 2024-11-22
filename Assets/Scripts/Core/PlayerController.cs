// PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance; // 单例实例

    public float moveDelay = 0.2f; // 移动的延迟，防止连续移动过快
    public Tilemap tilemap; // 引用Tilemap组件
    public int moveDistance = 1;

    private float nextMoveTime = 0f; // 用于控制移动间隔

    public GameObject inventoryUI;
    private bool ispoen = false;
    private IInteractable nearbyInteractable = null; // 当前附近的可交互物品

    void Start() {
        if (tilemap == null) {
            Debug.LogError( "Tilemap reference is missing!" );
        }
        if (Instance == null) {
            Instance = this; // 设置单例
        } else {
            Debug.LogError( "Multiple PlayerController instances detected!" );
        }
    }

    void Update() {
        HandleMovement(); // 处理玩家的离散移动
        HandleInteraction(); // 处理交互
        if (Input.GetKeyDown( KeyCode.B )) {
            ispoen = !ispoen;
            inventoryUI.SetActive( ispoen );
        }
    }

    private void HandleMovement() {
        // 检查当前时间是否超过下次允许移动的时间
        if (Time.time < nextMoveTime) return;

        // 获取玩家当前瓦片坐标
        Vector3Int direction = Vector3Int.zero;

        // 检查按键并设置移动方向，支持长按
        if (Input.GetKey( KeyCode.W )) direction = new Vector3Int( 0 , moveDistance , 0 ); // 向上
        else if (Input.GetKey( KeyCode.S )) direction = new Vector3Int( 0 , -moveDistance , 0 ); // 向下
        else if (Input.GetKey( KeyCode.A )) direction = new Vector3Int( -moveDistance , 0 , 0 ); // 向左
        else if (Input.GetKey( KeyCode.D )) direction = new Vector3Int( moveDistance , 0 , 0 ); // 向右

        // 仅当检测到有效的移动方向时才进行移动
        if (direction != Vector3Int.zero) {
            // 获取玩家当前位置并转换为瓦片坐标
            Vector3 worldPosition = transform.position;
            Vector3Int currentPlayerTilePosition = tilemap.WorldToCell( worldPosition );

            // 计算新的瓦片位置
            Vector3Int newPlayerTilePosition = currentPlayerTilePosition + direction;

            // 将新的瓦片位置转换为世界坐标并更新玩家位置
            Vector3 newWorldPosition = tilemap.GetCellCenterWorld( newPlayerTilePosition );
            transform.position = newWorldPosition;

            // 在完成位置移动后更新移动间隔时间
            nextMoveTime = Time.time + moveDelay;
        }
    }

    private void HandleInteraction() {
        if (Input.GetKeyDown( KeyCode.E )) {
            if (nearbyInteractable != null) {
                nearbyInteractable.Interact();
            } else {
                // 如果不在交互区域，可以添加其他交互逻辑
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null) {
            nearbyInteractable = interactable;
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable.Equals( nearbyInteractable )) {
            nearbyInteractable = null;
        }
    }
}
