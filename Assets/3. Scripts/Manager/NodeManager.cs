using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public BasicIcon pre_node;
    public Transform parent_node;

    List<BasicIcon> nodeList;

    // Start is called before the first frame update
    void Start()
    {
        RoomManager.Instance.OnNodeSetting += SetNode;
        RoomManager.Instance.OnNodePass += NodeComplete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNode(int index)
    {
        for (int i = 0; i < parent_node.childCount; i++)
        {
            Destroy(parent_node.GetChild(i));
        }
        nodeList.Clear();

        for(int i = 0; i < index; i++)
        {
            BasicIcon icon = Instantiate(pre_node, parent_node);
            nodeList.Add(icon);
        }
    }

    void NodeComplete(int index)
    {
        nodeList[index].spriteImage.color = new Color32(148, 148, 195, 255);
    }
}
