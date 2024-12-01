using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; // 单例实例
    public TextMeshProUGUI dayText;
    public int dayCount = 1;
    public StoryData_SO opening;

    private void Awake() {
        if (Instance == null) {
            Instance = this;

        } else {
            Destroy( gameObject ); // 避免重复实例
        }
        dayCount = 0;
    }

    private void Start() {
        // 尝试加载游戏进度
        var savedData = SaveManager.LoadGame();
        if (savedData != null) {
            // 加载游戏数据
            dayCount = savedData.dayCount;
            PlayerController.Instance.transform.position = savedData.playerPosition;
            InventoryManager.Instance.inventoryData.items = savedData.inventoryItems;

            foreach (var storyStatus in savedData.storyStatus) {
                opening.storyIndex[ storyStatus.Key ].isCutscene = !storyStatus.Value; // 恢复剧情状态
            }
        } else {
            // 初始化为默认状态
            dayCount = 0;
            foreach (var item in InventoryManager.Instance.inventoryData.items) {
                item.itemData = null;
                item.amount = 0;
            }
            StoryManager.Instance.StartStory( opening );
            SceneManager.LoadScene( "House" );
            GameObject player = GameObject.FindWithTag( "Player" );
            if (player != null) {
                player.transform.position = new Vector3( 2 , 3 , 0 );
            }

            UpdateDateUI(); // 初始化 UI
        }
    }


    private void Update() {
        UpdateDateUI();
        int currentDaycount = 0;
        if (dayCount != currentDaycount) {
            currentDaycount = dayCount;
            GameObject pick = GameObject.Find( "Pick" );
            if (pick != null) {
                IPicking picking = pick.GetComponent<IPicking>();
                if (picking != null) {
                    picking.isPicking = false;
                }
            }
        }
    }

    public void UpdateDateUI() {
        if (dayText != null) {
            dayText.text = $"Day: {dayCount}";
        }
    }
}
