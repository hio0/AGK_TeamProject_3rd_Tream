using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationData
{
    public Character targetChar;
    public Character relationChar;
}

public class RelationManager : MonoBehaviour
{
    public Action<RelationData, int> OnRelationChanged;
    public Dictionary<RelationData, int> relationshipList = new();
    public List<Relation> relationList = new();

    // Start is called before the first frame update
    void Start()
    {
        OnRelationChanged += SetRelationShip;
    }

    private void OnDisable()
    {
        OnRelationChanged -= SetRelationShip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetRelationShip(RelationData data, int changed)
    {
        if (relationshipList.ContainsKey(data))
        {
            relationshipList[data] += changed;
        }
        else
        {
            relationshipList.Add(data, changed);
        }

        if (relationshipList[data] >= 100)
        {
            relationshipList[data] = 100;
        }
        else if (relationshipList[data] <= -100)
        {
            relationshipList[data] = -100;
        }

        GetRelationShip(data);
    }

    void GetRelationShip(RelationData target)
    {
        int myDoki = relationshipList[target];
        int targetDoki = 10;

        RelationData targetRel = new RelationData
        {
            targetChar = target.relationChar,
            relationChar = target.targetChar
        };
        if (relationshipList.ContainsKey(targetRel))
        {
            targetDoki = relationshipList[targetRel];
        }

        int relation = myDoki + targetDoki;
        int r = UnityEngine.Random.Range(1, 101);

        if (r <= Mathf.Abs(relation))
        {
            if (relation > 0 && myDoki > 0 && targetDoki > 0)
            {

            }
            else if(relation <= 0)
            {

            }
        }
    }
}
