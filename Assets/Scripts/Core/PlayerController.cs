// PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance; // 单例实例

    public string tilemapTag = "Tilemap"; // Tilemap 的 Tag，用于动态获取
    public float moveDelay = 0.2f; // 移动的延迟，防止连续移动过快
    public Tilemap tilemap; // 引用Tilemap组件
    public int moveDistance = 1;

    private float nextMoveTime = 0f; // 用于控制移动间隔

    public GameObject inventoryUI;
    private IInteractable nearbyInteractable = null; // 当前附近的可交互物品

    // 饱食度相关
    [Header( "Hunger Settings" )]
    public int maxHunger = 100; // 饱食度最大值
    public int currentHunger; // 当前饱食度
    public int maxHBonus = 0;
    public int addHBonus = 0;
    [Range( 0 , 100 )] public int chance = 0;
    // Mutation 相关
    [Header( "Mutation Settings" )]
    public int currentMutation = 0; // 当前 Mutation 值
    public List<int> mutationThresholds = new List<int> { 0 , 100 , 200 , 400 , 500 }; // 每一级的分界点

    [Header( "UI Elements" )]
    public Slider hungerBar; // 饱食度条
    public Slider mutationBar; // Mutation 
    public GameObject PlayerUI;
    public GameObject panel;           
    public GameObject Teleports;
    public GameObject Minimap;
    void Start() {
        if (tilemap == null) {
            Debug.LogError( "Tilemap reference is missing!" );
        }
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy( gameObject ); // 避免重复实例
        }

        FindTilemapByTag();

        currentHunger = maxHunger; // 初始化饱食度为最大值
        currentMutation = 0; // 初始化 Mutation 为零

        // 初始化 UI
        if (hungerBar != null) {
            hungerBar.maxValue = maxHunger;
            hungerBar.value = currentHunger;
        }

        if (mutationBar != null) {
            mutationBar.maxValue = mutationThresholds[ mutationThresholds.Count - 1 ];
            mutationBar.value = currentMutation;
        }
    }

    void Update() {
        if (tilemap == null) {
            FindTilemapByTag();
        }

        HandleMovement(); // 处理玩家的离散移动
        HandleInteraction(); // 处理交互
        if (Input.GetKeyDown( KeyCode.B )) {
            Minimap.SetActive( inventoryUI.activeSelf );
            inventoryUI.SetActive( !inventoryUI.activeSelf );

        }
        if( Input.GetKeyDown( KeyCode.M )) {
            if (TeleportManager.Instance.available) {
                Teleports.SetActive( !panel.activeSelf );
                PlayerUI.SetActive(panel.activeSelf );
            }
            panel.SetActive( !panel.activeSelf );
        }
        UpdateHungerBar();
        UpdateMutationBar();
    }

    private void FindTilemapByTag() {
        GameObject tilemapObject = GameObject.FindWithTag( tilemapTag );
        if (tilemapObject != null) {
            tilemap = tilemapObject.GetComponent<Tilemap>();
            if (tilemap == null) {
                Debug.LogError( "Tilemap component not found on object with tag: " + tilemapTag );
            }
        } else {
            Debug.LogError( "Tilemap object not found with tag: " + tilemapTag );
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
    private int GetCurrentMutationLevelIndex() {
        for (int i = mutationThresholds.Count - 1 ; i >= 0 ; i--) {
            if (currentMutation >= mutationThresholds[ i ]) {
                return i;
            }
        }
        return 0; // 默认第一级
    }

        public void UpdateMutation( int amount ) {
        int currentLevelIndex = GetCurrentMutationLevelIndex();

        currentMutation += amount;

        if (currentMutation < mutationThresholds[ currentLevelIndex ]) {
            currentMutation = mutationThresholds[ currentLevelIndex ]; // 保证 Mutation 不低于当前等级分界点
        } else if (currentMutation > mutationThresholds[ mutationThresholds.Count - 1 ]) {
            currentMutation = mutationThresholds[ mutationThresholds.Count - 1 ]; // 不超过最大值
        }

        Debug.Log( $"Current Mutation: {currentMutation}" );
    }

    public void UpdateHunger(int amount) {
        if(amount > 0 && currentHunger == maxHunger+maxHBonus) {
            return;
        }
        if (currentHunger + amount <= 0) {
            OnHungerDepleted();
        } else {
            if (amount < 0) {
                bool update = Random.Range( 0 , 100 ) >= chance;
                if (update) {
                    currentHunger = currentHunger + amount;
                } else {
                    return;
                }
            } else {
                if (currentHunger + amount + addHBonus < maxHunger + maxHBonus) {
                    currentHunger = currentHunger + amount + addHBonus;
                } else {
                    currentHunger = maxHunger;
                }
            }
        }
       
    }

    // 饱食度为零时的逻辑
    private void OnHungerDepleted() {
        Debug.Log( "Hunger is depleted! Add your logic here." );
        // 留空的方法，用于未来添加具体逻辑
    }

    private void UpdateHungerBar() {
        if (hungerBar != null) {
            hungerBar.maxValue = maxHunger;
            hungerBar.value = currentHunger;
        }
    }

    private void UpdateMutationBar() {
        if (mutationBar != null) {
            mutationBar.maxValue = mutationThresholds[ mutationThresholds.Count - 1 ];
            mutationBar.value = currentMutation;
        }
    }
    public void TeleportTo( string teleportID ) {
        // 获取目标传送点的位置
        Vector3? targetPosition = TeleportManager.Instance.GetTeleportPosition( teleportID );

        if (targetPosition.HasValue) {
            if(targetPosition != transform.position) {
                // 如果找到对应位置，设置玩家坐标
                if (TeleportManager.Instance.currentTeleportCount > 0) {
                    transform.position = targetPosition.Value;
                    TeleportManager.Instance.currentTeleportCount--;
                } else {
                    Debug.Log( "Teleport failed: No more teleports available." );
                }
            } else {
                Debug.Log( "Teleport failed: Same Position." );
            }
        } else {
            Debug.LogWarning( "Teleport failed: Invalid ID." );
        }
    }
    
}