using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryOption {
    public string text;          // 选项文字
    public string targetID;      // 目标节点 ID
}

[System.Serializable]
public class StoryNode {
    public string ID;                  // 当前节点 ID
    public string targetID;            // 下一节点 ID（线性推进）
    public Sprite backgroundImage;     // 背景图片
    public Sprite characterImage;      // 角色图片
    public bool isCutscene;            // 是否为 Cutscene 节点
    [TextArea] public string text;     // 文本内容

    public List<StoryOption> options = new List<StoryOption>(); // 分支选项
}

[CreateAssetMenu( fileName = "New Story" , menuName = "Story/Story Data" )]
public class StoryData_SO : ScriptableObject {
    public List<StoryNode> storyNodes = new List<StoryNode>();
    public Dictionary<string , StoryNode> storyIndex = new Dictionary<string , StoryNode>();

#if UNITY_EDITOR
    private void OnValidate() {
        storyIndex.Clear();
        foreach (var node in storyNodes) {
            if (!storyIndex.ContainsKey( node.ID )) {
                storyIndex.Add( node.ID , node );
            }
        }
    }
#endif
}
