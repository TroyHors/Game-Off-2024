using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeleportPoint {
    public string ID;          // 唯一 ID
    public Vector3 position;   // 对应的坐标
}

public class TeleportManager : MonoBehaviour {
    public static TeleportManager Instance;
    public bool available;
    public int maxTeleportCount = 5;    // 最大传送次数
    public int currentTeleportCount;

    [Header( "Teleport Points" )]
    public List<TeleportPoint> teleportPoints = new List<TeleportPoint>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy( gameObject );
        }
        currentTeleportCount = maxTeleportCount; // 初始化传送次数
    }


    // 根据 ID 获取对应的传送点坐标
    public Vector3? GetTeleportPosition( string id ) {
        foreach (var point in teleportPoints) {
            if (point.ID == id) {
                return point.position;
            }
        }
        Debug.LogWarning( $"Teleport Point with ID '{id}' not found." );
        return null; // 如果找不到，返回 null
    }
}