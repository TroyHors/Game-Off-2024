using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveDelay = 0.2f; // 移动的延迟，防止连续输入

    private float nextMoveTime = 0f; // 用于控制移动间隔

    void Start() {
      
    }

    void Update() {
        HandleMovement(); // 处理玩家的离散移动
    }

    private void HandleMovement() {
        // 检查当前时间是否超过下次允许移动的时间
        if (Time.time < nextMoveTime) return;

        // 获取玩家当前瓦片坐标
        Vector3Int direction = Vector3Int.zero;

        // 检查按键并设置移动方向
        if (Input.GetKeyDown( KeyCode.W )) direction = new Vector3Int( 0 , 1 , -1 );     // 向上
        if (Input.GetKeyDown( KeyCode.S )) direction = new Vector3Int( 0 , -1 , -1 );    // 向下
        if (Input.GetKeyDown( KeyCode.A )) direction = new Vector3Int( -1 , 0 , -1 );    // 向左
        if (Input.GetKeyDown( KeyCode.D )) direction = new Vector3Int( 1 , 0 , -1 );     // 向右

        if (direction != Vector3Int.zero) {
           
            // 更新移动延迟时间
            nextMoveTime = Time.time + moveDelay;


        }
    }
}
