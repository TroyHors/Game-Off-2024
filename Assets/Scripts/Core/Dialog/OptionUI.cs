using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour {
    public TextMeshProUGUI optionText;
    private Button optionButton;
    private string targetNodeID;

    private void Awake() {
        optionButton = GetComponent<Button>();
        optionButton.onClick.AddListener( OnOptionClicked );
    }

    public void UpdateOption( StoryNode node , StoryOption option ) {
        optionText.text = option.text;
        targetNodeID = option.targetID;
    }

    private void OnOptionClicked() {
        if (string.IsNullOrEmpty( targetNodeID )) {
            StoryManager.Instance.EndStory();
        } else if (StoryManager.Instance.currentStoryData.storyIndex.ContainsKey( targetNodeID )) {
            StoryManager.Instance.currentNodeID = targetNodeID;
            StoryManager.Instance.DisplayNode( StoryManager.Instance.currentStoryData.storyIndex[ targetNodeID ] );
        } else {
            StoryManager.Instance.EndStory();
        }
    }
}
