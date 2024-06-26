using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IslandManager))]
public class IslandManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IslandManager islandManager = (IslandManager)target;
        if (GUILayout.Button("Generate Random Island"))
        {
            islandManager.GenerateRandomIsland();
        }
    }
}
