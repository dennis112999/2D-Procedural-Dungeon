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