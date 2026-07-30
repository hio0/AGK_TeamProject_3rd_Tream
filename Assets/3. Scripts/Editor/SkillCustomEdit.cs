using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public static class SkillCustomEdit
{
    [MenuItem("Game/Create Skill")]
    public static void CreateSkill()
    {
        GenericMenu menu = new GenericMenu();

        Type[] skillTypes = TypeCache.GetTypesDerivedFrom<Skill>()
            .Where(t => !t.IsAbstract)
            .OrderBy(t => t.Name)
            .ToArray();

        foreach (Type type in skillTypes)
        {
            menu.AddItem(
                new GUIContent(type.Name),
                false,
                () => CreateSkillAsset(type)
            );
        }

        menu.ShowAsContext();
    }

    private static void CreateSkillAsset(Type type)
    {
        string folder = "Assets/Data/Skills";

        if (!AssetDatabase.IsValidFolder("Assets/Data"))
            AssetDatabase.CreateFolder("Assets", "Data");

        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets/Data", "Skills");

        Skill skill = (Skill)ScriptableObject.CreateInstance(type);

        string path = AssetDatabase.GenerateUniqueAssetPath(
            $"{folder}/{type.Name}.asset"
        );

        AssetDatabase.CreateAsset(skill, path);
        AssetDatabase.SaveAssets();

        Selection.activeObject = skill;
        EditorGUIUtility.PingObject(skill);
    }
}