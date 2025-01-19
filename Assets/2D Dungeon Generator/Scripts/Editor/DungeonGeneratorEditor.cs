using UnityEngine;

namespace DG.Gameplay
{

#if UNITY_EDITOR

    using UnityEditor;

    [CustomEditor(typeof(DungeonGenerator), true)]
    public class DungeonGeneratorEditor : Editor
    {
        DungeonGenerator dungeonGenerator;

        private void Awake()
        {
            dungeonGenerator = (DungeonGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Label("Click here to visit wiki!", EditorStyles.linkLabel);
            Rect rect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

            Event currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseDown && rect.Contains(currentEvent.mousePosition))
            {
                Help.BrowseURL("https://github.com/dennis112999/2D-Procedural-Dungeon?tab=readme-ov-file");
            }

            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Dungeon"))
            {
                dungeonGenerator.GenerateDungeon();
            }

            if (GUILayout.Button("Clear Dungeon"))
            {
                dungeonGenerator.ClearDungeon();
            }

            EditorGUILayout.EndHorizontal();
        }
    }

#endif

}