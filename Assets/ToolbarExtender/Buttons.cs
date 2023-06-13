using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace UnityToolbarExtender.Examples
{
#if UNITY_EDITOR
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
                fixedWidth = 100

            };
        }
    }


    [InitializeOnLoad]
    public class ScenePrefResetButton
    {
        static ScenePrefResetButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("Reset", "Clear All Playerprefs"), ToolbarStyles.commandButtonStyle))
            {
                //PlayerPrefs.DeleteAll();
                //SaveManager.JsonDelete();
            }

            if (GUILayout.Button(new GUIContent("Splash", "Splash"), ToolbarStyles.commandButtonStyle))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene("Assets/Scenes/Splash.unity");
            }
            if (GUILayout.Button(new GUIContent("Lobby", "Lobby"), ToolbarStyles.commandButtonStyle))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity");
            }

            if (GUILayout.Button(new GUIContent("Game", "Game"), ToolbarStyles.commandButtonStyle))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
            }


        }
    }
#endif
}
