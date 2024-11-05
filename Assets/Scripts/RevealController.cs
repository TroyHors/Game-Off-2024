using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealController : MonoBehaviour {
    public Tilemap tilemap;        // Tilemap 引用
    public Transform player;       // 玩家对象
    public int revealLimit = 3;    // 最大揭示次数
    public int revealRadius = 1;   // 揭示的半径，控制显示的瓦片区域大小

    private bool isTilemapVisible = false;
    private Vector3Int[] previousTiles;  // 上一次显示的瓦片位置
    private int currentRevealCount = 0;  // 当前揭示次数

    void Start() {
        if (tilemap != null) {
            HideAllTiles(); // 初始时隐藏所有瓦片
        } else {
            Debug.LogError( "Tilemap reference is missing!" );
        }
    }

    public void HideAllTiles() {
        // 仅更改颜色的 alpha 值，而不是删除瓦片
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin) {
            if (tilemap.HasTile( pos )) {
                tilemap.SetTileFlags( pos , TileFlags.None );
                tilemap.SetColor( pos , new Color( 1 , 1 , 1 , 0 ) ); // 将所有瓦片设置为透明
            }
        }
        isTilemapVisible = false;
    }

    public void ToggleTilemapVisibility() {
        // 检查揭示次数是否达到上限
        if (currentRevealCount >= revealLimit) {
            Debug.Log( "揭示次数已用尽！" );
            return;
        }

        isTilemapVisible = !isTilemapVisible;

        if (isTilemapVisible) {
            ShowTilesAroundPlayer();
            currentRevealCount++; // 每次揭示成功后增加揭示次数
        } else {
            HideAllTiles();
        }
    }

    public void ShowTilesAroundPlayer() {
        if (player == null) {
            Debug.LogError( "Player reference is missing!" );
            return;
        }

        // 获取玩家所在的瓦片位置
        Vector3Int playerTilePosition = WorldToTilePosition( player.position );

        // 根据 revealRadius 定义显示区域
        int areaSize = ( 2 * revealRadius + 1 ) * ( 2 * revealRadius + 1 );
        Vector3Int[] currentTiles = new Vector3Int[ areaSize ];
        int index = 0;

        // 计算以玩家为中心的瓦片坐标，根据 revealRadius 设置范围
        for (int x = -revealRadius ; x <= revealRadius ; x++) {
            for (int y = -revealRadius ; y <= revealRadius ; y++) {
                currentTiles[ index ] = new Vector3Int( playerTilePosition.x + x , playerTilePosition.y + y , playerTilePosition.z );
                index++;
            }
        }

        // 隐藏上一次的瓦片
        if (previousTiles != null) {
            foreach (Vector3Int pos in previousTiles) {
                if (tilemap.HasTile( pos )) {
                    tilemap.SetTileFlags( pos , TileFlags.None );
                    tilemap.SetColor( pos , new Color( 1 , 1 , 1 , 0 ) ); // 隐藏瓦片
                }
            }
        }

        // 显示当前半径范围内的瓦片区域
        foreach (Vector3Int pos in currentTiles) {
            if (tilemap.HasTile( pos )) {
                tilemap.SetTileFlags( pos , TileFlags.None );
                tilemap.SetColor( pos , new Color( 1 , 1 , 1 , 1 ) ); // 将瓦片设置为可见
            }
        }

        // 更新记录的瓦片位置
        previousTiles = currentTiles;
    }

    // 获取玩家在 Tilemap 中的瓦片坐标
    public Vector3Int GetPlayerTilePosition() {
        return tilemap.WorldToCell( player.position );
    }

    // 将瓦片坐标转换为世界坐标
    public Vector3 GetWorldPosition( Vector3Int tilePosition ) {
        return tilemap.GetCellCenterWorld( tilePosition );
    }

    // 将世界坐标转换为瓦片坐标
    public Vector3Int WorldToTilePosition( Vector3 worldPosition ) {
        return tilemap.WorldToCell( worldPosition );
    }
}
