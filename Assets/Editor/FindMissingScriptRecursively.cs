using UnityEditor;
using UnityEngine;

public class FindMissingScriptsRecursively : EditorWindow
{
    [MenuItem("Window/Find Missing Scripts Recursively")]
    public static void ShowWindow()
    {
        GetWindow(typeof(FindMissingScriptsRecursively));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Selected GameObjects"))
        {
            FindInSelected();
        }
    }

    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        int go_count = 0, components_count = 0, missing_count = 0;
        foreach (GameObject g in go)
        {
            go_count++;
            Component[] components = g.GetComponentsInChildren<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    Debug.Log(g.name + " has an empty script attached in position: " + i, g);
                }
            }
        }

        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
    }
}
