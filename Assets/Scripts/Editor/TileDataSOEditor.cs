using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.Tilemaps;

namespace DG.Data
{
    [CustomEditor(typeof(TileDataSO))]
    public class TileDataSOEditor : Editor
    {
        private GUIStyle _labelStyle;
        private static int Space = 5;
        public override void OnInspectorGUI()
        {
            TileDataSO tileData = (TileDataSO)target;

            EditorGUILayout.LabelField("Tile Data Configuration", EditorStyles.boldLabel);
            GUILayout.Space(10);

            DrawTileRow("Wall", ref tileData.Wall);
            GUILayout.Space(Space);
            DrawTileRow("Floor", ref tileData.Floor);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(tileData);
            }
        }

        private void DrawTileRow(string label, ref Tile tile)
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(EditorStyles.label)
                {
                    wordWrap = true
                };
            }

            EditorGUILayout.BeginHorizontal();

            GUILayout.BeginVertical(GUILayout.Width(300));

            // Tile setting
            EditorGUILayout.LabelField(label, _labelStyle);
            tile = (Tile)EditorGUILayout.ObjectField(tile, typeof(Tile), false);

            GUILayout.EndVertical();

            // tile sprite
            GUILayout.BeginVertical(GUILayout.Width(60));
            if (tile != null && tile.sprite != null)
            {
                GUILayout.FlexibleSpace();
                Rect rect = GUILayoutUtility.GetRect(50, 50, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                Texture texture = AssetPreview.GetAssetPreview(tile.sprite);
                if (texture != null)
                {
                    EditorGUI.DrawPreviewTexture(rect, texture);
                }
                else
                {
                    EditorGUI.DrawRect(rect, Color.white);
                    GUI.Label(rect, "Sprite Missing", new GUIStyle { alignment = TextAnchor.MiddleCenter });
                }
                GUILayout.FlexibleSpace();
            }
            else
            {
                GUILayout.FlexibleSpace();
                Rect rect = GUILayoutUtility.GetRect(50, 50, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                EditorGUI.DrawRect(rect, Color.gray);
                GUI.Label(rect, "No Tile", new GUIStyle { alignment = TextAnchor.MiddleCenter });
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }
    }

}

#endif
