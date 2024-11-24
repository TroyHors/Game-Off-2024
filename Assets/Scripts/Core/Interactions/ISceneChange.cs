using UnityEngine;
using UnityEngine.SceneManagement;

public class iSceneChange : MonoBehaviour, IInteractable {
    [Header( "Scene Change Settings" )]
    public string targetSceneName; // 目标场景的名称

    [Header( "Target Position Settings" )]
    public Vector3 targetPosition; // 切换场景后玩家的目标位置

    public void Interact() {
        if (!string.IsNullOrEmpty( targetSceneName )) {

            // 将目标位置传递给场景管理器
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene( targetSceneName );
            if (IDayChange.Instance != null) {
                IDayChange.Instance.isSlept = false;
            }
        } else {
            Debug.LogWarning( "Target scene name is empty. Please set it in the Inspector." );
        }
    }

    // 场景加载完成后设置玩家位置
    // iSceneChange.cs
    private void OnSceneLoaded( Scene scene , LoadSceneMode mode ) {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 移除事件监听

        // 获取玩家对象并设置位置
        GameObject player = GameObject.FindWithTag( "Player" );
        if (player != null) {
            player.transform.position = targetPosition;
        } else {
            Debug.LogWarning( "Player object not found in the new scene." );
        }

        // 查找目标场景中的 Bed
        GameObject bedObject = GameObject.Find( "Bed" );
        if (bedObject != null) {
            IDayChange bed = bedObject.GetComponent<IDayChange>();
            if (bed != null) {
                bed.isSlept = false;
            }
        } else {
            Debug.LogWarning( "Bed object not found in the new scene." );
        }
    }

}
