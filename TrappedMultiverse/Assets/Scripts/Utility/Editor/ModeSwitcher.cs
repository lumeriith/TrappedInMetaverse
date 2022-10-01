using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;


static class ToolbarStyles
{
    public static readonly GUIStyle commandButtonStyle;

    static ToolbarStyles()
    {
        commandButtonStyle = new GUIStyle("Command")
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Bold
        };
    }
}

[InitializeOnLoad]
public class SceneSwitchLeftButton
{
    static SceneSwitchLeftButton()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        EditorApplication.playModeStateChanged += change =>
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                SwitchToMode();
            }
        };
    }

    private static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();
        if (Application.isPlaying) return;
        if(GUILayout.Button(new GUIContent("All", "Start Scene 1"), ToolbarStyles.commandButtonStyle))
        {
            SwitchToMode();
        }
        
        if(GUILayout.Button(new GUIContent("Zoo", "Start Scene 1"), ToolbarStyles.commandButtonStyle))
        {
            SwitchToMode(GameMode.Zoom);
        }

        if(GUILayout.Button(new GUIContent("Gun", "Start Scene 2"), ToolbarStyles.commandButtonStyle))
        {
            SwitchToMode(GameMode.Gun);
        }
        
        if(GUILayout.Button(new GUIContent("Mar", "Start Scene 3"), ToolbarStyles.commandButtonStyle))
        {
            SwitchToMode(GameMode.Mario);
        }
        
        // if(GUILayout.Button(new GUIContent("SPLIT", "Start Scene 3"), ToolbarStyles.commandButtonStyle))
        // {
        //     var root = Selection.activeGameObject;
        //     Undo.RecordObject(root, "Split");
        //     var model = Object.Instantiate(root, root.transform.position, root.transform.rotation, root.transform);
        //     model.name = "Gun Model";
        //     model.AddComponent<GunModeObject>();
        //     Undo.RecordObject(model, "Split");
        //     Object.DestroyImmediate(model.GetComponent<Collider>());
        //     Object.DestroyImmediate(root.GetComponent<Renderer>());
        // }
        // if(GUILayout.Button(new GUIContent("SMar", "Start Scene 3"), ToolbarStyles.commandButtonStyle))
        // {
        //     var root = Selection.activeGameObject;
        //     Undo.RecordObject(root, "Split");
        //     root.name = "Mario Model";
        //     root.AddComponent<MarioModeObject>();
        //     if (root.TryGetComponent(out GunModeObject gunobj)) Object.DestroyImmediate(gunobj);
        //     if (root.TryGetComponent(out Collider col)) Object.DestroyImmediate(col);
        // }

    }

    private static void SwitchToMode(GameMode mode)
    {
        var zooms = Object.FindObjectsOfType<ZoomModeObject>(true);
        var guns = Object.FindObjectsOfType<GunModeObject>(true);
        var mars = Object.FindObjectsOfType<MarioModeObject>(true);
        foreach (var z in zooms) z.gameObject.SetActive(mode == GameMode.Zoom);
        foreach (var g in guns) g.gameObject.SetActive(mode == GameMode.Gun);
        foreach (var m in mars) m.gameObject.SetActive(mode == GameMode.Mario);
    }
    
    private static void SwitchToMode()
    {
        var zooms = Object.FindObjectsOfType<ZoomModeObject>(true);
        var guns = Object.FindObjectsOfType<GunModeObject>(true);
        var mars = Object.FindObjectsOfType<MarioModeObject>(true);
        foreach (var z in zooms) z.gameObject.SetActive(true);
        foreach (var g in guns) g.gameObject.SetActive(true);
        foreach (var m in mars) m.gameObject.SetActive(true);
    }
}