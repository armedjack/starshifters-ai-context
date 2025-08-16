// Editor/CopyGuidUtility.cs
// Контекстное меню "Copy GUID" в Project Window для любого ассета.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class CopyGuidUtility
{
    [MenuItem("Assets/Copy GUID", false, 19)]
    private static void CopyGuid()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
            return;

        string guid = AssetDatabase.AssetPathToGUID(path);
        EditorGUIUtility.systemCopyBuffer = guid;
        Debug.Log($"GUID скопирован: {guid}");
    }

    // Пункт доступен только если выбран объект
    [MenuItem("Assets/Copy GUID", true)]
    private static bool CopyGuid_Validate()
    {
        return Selection.activeObject != null && !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(Selection.activeObject));
    }
}
#endif
