using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 메모: AI가 생성해준 커스텀 에딧 코드 ( 커스텀 에디터 한번도 사용 안해봄
// 코드 분석하면서 공부할 예정 => 주석으로 분석해둠

[CustomEditor(typeof(SkillData))] // SkillData에 대한 CustomEditor
public class SkillCustomEdit : Editor
{
    SerializedProperty effects;

    private void OnEnable()
    {
        effects = serializedObject.FindProperty("effects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // effects를 제외한 기본 변수들
        DrawPropertiesExcluding(serializedObject, "effects");

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

        for (int i = 0; i < effects.arraySize; i++)
        {
            SerializedProperty element =
                effects.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            // Effect 이름 + 삭제 버튼
            EditorGUILayout.BeginHorizontal();

            string effectName = GetEffectName(element);

            EditorGUILayout.LabelField(
                effectName,
                EditorStyles.boldLabel
            );

            if (GUILayout.Button("X", GUILayout.Width(25))) /// 나무위키 처럼 버튼 width도 조절 가능 ㄷ
            {
                effects.DeleteArrayElementAtIndex(i);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                break;
            }

            EditorGUILayout.EndHorizontal();

            // Element 0 없이 실제 내용만 표시
            DrawEffectFields(element);

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(5);

        if (GUILayout.Button("Add Effect"))
        {
            ShowEffectMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string GetEffectName(SerializedProperty property) 
    {
        string fullName = property.managedReferenceFullTypename;

        if (string.IsNullOrEmpty(fullName))
            return "Empty Effect";

        int space = fullName.LastIndexOf(' ');

        if (space >= 0)
            fullName = fullName.Substring(space + 1);

        // DamageEffect → Damage Effect
        return ObjectNames.NicifyVariableName(fullName);
    }

    private void DrawEffectFields(SerializedProperty element)
    {
        SerializedProperty iterator = element.Copy();
        SerializedProperty end = iterator.GetEndProperty();

        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) &&
               !SerializedProperty.EqualContents(iterator, end))
        {
            EditorGUILayout.PropertyField(iterator, true);
            enterChildren = false;
        }
    }

    private void ShowEffectMenu()
    {
        GenericMenu menu = new GenericMenu();

        menu.AddItem(
            new GUIContent("Damage Effect"),
            false,
            () => AddEffect(typeof(DamageEffect))
        );

        menu.ShowAsContext();
    }

    private void AddEffect(System.Type type)
    {
        int index = effects.arraySize;

        effects.InsertArrayElementAtIndex(index);

        SerializedProperty element =
            effects.GetArrayElementAtIndex(index);

        element.managedReferenceValue =
            System.Activator.CreateInstance(type);

        serializedObject.ApplyModifiedProperties();
    }
}
