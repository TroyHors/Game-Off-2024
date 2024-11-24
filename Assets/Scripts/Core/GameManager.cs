using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; // 单例实例
    public TextMeshProUGUI dayText;
    public int dayCount = 1;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad( gameObject ); // 保持跨场景不销毁
        } else {
            Destroy( gameObject ); // 避免重复实例
        }
        dayCount = 1;
    }

    private void Start() {
        UpdateDateUI(); // 初始化 UI
    }

    private void Update() {
        UpdateDateUI();
    }

    private void UpdateDateUI() {
        if (dayText != null) {
            dayText.text = $"Day: {dayCount}";
        }
    }
}
