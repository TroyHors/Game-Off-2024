using UnityEngine;

public class AreaController: MonoBehaviour {

    [Header( "Detection Settings" )]
    public CircleCollider2D detectionRadius; // 圆形碰撞箱
    public float detectionRange = 5f; // 圆形碰撞范围
    public float relocateInterval = 5f; // 碰撞箱重新定位的间隔
    public Vector2 mapBounds = new Vector2( 50 , 50 ); // 地图范围
    public Vector2 safeZoneCenter = new Vector2( 10 , 10 ); // 安全区中心
    public float safeZoneRadius = 10f; // 安全区半径

    private void Start() {
        detectionRadius.radius = detectionRange; // 初始化碰撞范围
        InvokeRepeating( nameof( RelocateDetectionArea ) , relocateInterval , relocateInterval ); // 定时移动碰撞箱
    }

    

    private void OnTriggerEnter2D( Collider2D collision ) {
        if (collision.CompareTag( "Player" )) {
            GrandpaController.Instance.StartChasing(); // 开始追逐
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.CompareTag( "Player" )) {
            GrandpaController.Instance.StopChasing(); // 停止追逐
        }
    }

  

    private void RelocateDetectionArea() {
        Vector2 newPosition;
        do {
            newPosition = new Vector2(
                Random.Range( -mapBounds.x , mapBounds.x ) ,
                Random.Range( -mapBounds.y , mapBounds.y )
            );
        } while (Vector2.Distance( newPosition , safeZoneCenter ) < safeZoneRadius + detectionRange);

        transform.position = newPosition; // 更新碰撞箱位置
    }
}
