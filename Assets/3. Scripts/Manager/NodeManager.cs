using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData
{
    public List<BasicIcon> nodeList = new();
    public int nowNodeNum;
}

public class NodeManager : MonoBehaviour
{
    public BasicIcon pre_node;
    public Transform parent_node;

    List<BasicIcon> nodeList = new();
    int nowNodeNum;

    // Start is called before the first frame update
    void Start()
    {
        RoomManager.Instance.GetNodeData += ReturnData;

        RoomManager.Instance.OnNodeSetting += SetNode;
        RoomManager.Instance.OnNodePass += NodeComplete;
        SchoolManager.instance.OnNextFloor += ResetData;
    }

    private void OnDisable()
    {
        RoomManager.Instance.OnNodeSetting -= SetNode;
        RoomManager.Instance.OnNodePass -= NodeComplete;
        SchoolManager.instance.OnNextFloor -= ResetData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    NodeData ReturnData()
    {
        NodeData data = new NodeData
        {
            nodeList = nodeList,
            nowNodeNum = nowNodeNum
        };

        return data;
    }

    void SetNode(int index)
    {
        for (int i = 0; i < parent_node.childCount; i++)
        {
            Destroy(parent_node.GetChild(i).gameObject);
        }
        nodeList.Clear();

        for (int i = 0; i < index; i++)
        {
            BasicIcon icon = Instantiate(pre_node, parent_node);
            nodeList.Add(icon);
        }
    }

    void NodeComplete()
    {
        nodeList[nowNodeNum].spriteImage.color = new Color32(148, 148, 195, 255);
        nowNodeNum++;
    }

    void ResetData()
    {
        nowNodeNum = 0;
    }
}
