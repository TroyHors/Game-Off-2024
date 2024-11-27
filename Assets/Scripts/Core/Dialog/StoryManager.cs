using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : Singleton<StoryManager> {
    [Header( "UI Elements" )]
    public GameObject storyPanel;          // 主界面面板（对话）
    public GameObject backgroundPanel;
    public GameObject playerUI;            // 玩家 UI
    public Image backgroundImage;          // 背景图片
    public Image characterImage;           // 对话头像
    public TextMeshProUGUI storyText;      // 对话/剧情文本
    public RectTransform optionPanel;      // 选项面板
    public OptionUI optionPrefab;          // 选项预制体

    [Header( "Default Background" )]
    public Sprite defaultBackgroundImage;  // 默认背景图片

    [Header( "Data" )]
    public StoryData_SO currentStoryData;
    public string currentNodeID;

    private bool isStoryActive = false;

    private void Update() {
        // 鼠标点击推进剧情
        if (isStoryActive && Input.GetMouseButtonDown( 0 )) {
            AdvanceStory();
        }
    }

    public void StartStory( StoryData_SO storyData ) {
        currentStoryData = storyData;
        if (storyData.storyNodes.Count > 0) {
            currentNodeID = storyData.storyNodes[ 0 ].ID; // 从第一个节点开始
            DisplayNode( currentStoryData.storyIndex[ currentNodeID ] );
        }
    }

    public void DisplayNode( StoryNode node ) {
        isStoryActive = true;

        // 根据节点类型调整显示
        if (node.isCutscene) {
            // Cutscene: 仅显示背景图片，隐藏对话面板
            if (node.backgroundImage != null) {
                backgroundImage.sprite = node.backgroundImage;
            } else {
                backgroundImage.sprite = defaultBackgroundImage;
            }
            storyPanel.SetActive( false );
            backgroundPanel.SetActive( true );
        } else {
            // 对话：显示背景图片和角色图片
            if (node.backgroundImage != null) {
                backgroundImage.sprite = node.backgroundImage;
            } else {
                backgroundImage.sprite = defaultBackgroundImage;
            }

            if (node.characterImage != null) {
                characterImage.sprite = node.characterImage;
                characterImage.enabled = true;
            } else {
                characterImage.enabled = false;
            }

            storyText.text = node.text;
            storyPanel.SetActive( true );
            backgroundPanel.SetActive ( true );
        }

        playerUI.SetActive( false );
        CreateOptions( node );
    }

    private void AdvanceStory() {
        if (currentStoryData == null || string.IsNullOrEmpty( currentNodeID )) {
            EndStory();
            return;
        }

        var currentNode = currentStoryData.storyIndex[ currentNodeID ];

        // 如果有选项，则等待玩家选择
        if (currentNode.options.Count > 0) {
            return;
        }

        // 如果有目标节点，跳转到目标节点
        if (!string.IsNullOrEmpty( currentNode.targetID ) && currentStoryData.storyIndex.ContainsKey( currentNode.targetID )) {
            currentNodeID = currentNode.targetID;
            DisplayNode( currentStoryData.storyIndex[ currentNodeID ] );
        } else {
            EndStory();
        }
    }

    private void CreateOptions( StoryNode node ) {
        // 清空已有选项
        foreach (Transform child in optionPanel) {
            Destroy( child.gameObject );
        }

        // 创建新选项
        foreach (var option in node.options) {
            var optionUI = Instantiate( optionPrefab , optionPanel );
            optionUI.UpdateOption( node , option );
        }
    }

    public void EndStory() {
        isStoryActive = false;
        storyPanel.SetActive( false );
        backgroundPanel?.SetActive( false );
        playerUI.SetActive( true );
    }
}
