using UnityEngine;

public class StoryTrigger : MonoBehaviour, IInteractable {
    public StoryData_SO storyData;

    public void Interact() {
        if (storyData != null) {
            StoryManager.Instance.StartStory( storyData );
        }
    }


}
