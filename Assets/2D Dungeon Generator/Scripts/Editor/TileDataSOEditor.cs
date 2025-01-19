using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.Tilemaps;

namespace DG.Data
{
    [CustomEditor(typeof(TileDataSO))]
    public class TileDataSOEditor : Editor
    {
        private GUIStyle _titleStyle;
        private GUIStyle _subTitleStyle;
        private GUIStyle _labelStyle;

        private static int Space = 10;
        public override void OnInspectorGUI()
        {
            TileDataSO tileData = (TileDataSO)target;

            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter
                };
            }

            if (_subTitleStyle == null)
            {
                _subTitleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleLeft
                };
            }

            // Title
            EditorGUILayout.LabelField("Tile Data Configuration", _titleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);

            // Floor Tiles Header
            EditorGUILayout.LabelField("Floor Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Floor Tile
            DrawTileRow("Floor", ref tileData.Floor);
            GUILayout.Space(10);

            // Wall Tiles Header
            EditorGUILayout.LabelField("Wall Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Wall Tiles
            EditorGUILayout.BeginVertical();
            DrawTileRow("Wall_Top", ref tileData.Wall_Top);
            GUILayout.Space(Space);
            DrawTileRow("Wall_SideRight", ref tileData.Wall_SideRight);
            GUILayout.Space(Space);
            DrawTileRow("Wall_Bottom", ref tileData.Wall_Bottom);
            GUILayout.Space(Space);
            DrawTileRow("Wall_SideLeft", ref tileData.Wall_SideLeft);
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            // Inner Corner Tiles Header
            EditorGUILayout.LabelField("Inner Corner Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Inner Corner Tiles
            EditorGUILayout.BeginVertical();
            DrawTileRow("Wall_InnerConerDownLeft", ref tileData.Wall_InnerConerDownLeft);
            GUILayout.Space(Space);
            DrawTileRow("Wall_InnerConerDownRight", ref tileData.Wall_InnerConerDownRight);
            GUILayout.Space(Space);
            DrawTileRow("Wall_InnerConerUpLeft", ref tileData.Wall_InnerConerUpLeft);
            GUILayout.Space(Space);
            DrawTileRow("Wall_InnerConerUpRight", ref tileData.Wall_InnerConerUpRight);
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            // Diagona Corner Tiles Header
            EditorGUILayout.LabelField("Diagona Corner Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Diagonal Corner Tiles
            EditorGUILayout.BeginVertical();
            DrawTileRow("Wall_DiagonaCornerDownLeft", ref tileData.Wall_DiagonaCornerDownLeft);
            GUILayout.Space(Space);
            DrawTileRow("Wall_DiagonaCornerDownRight", ref tileData.Wall_DiagonaCornerDownRight);
            GUILayout.Space(Space);
            DrawTileRow("Wall_DiagonaCornerUpLeft", ref tileData.Wall_DiagonaCornerUpLeft);
            GUILayout.Space(Space);
            DrawTileRow("Wall_DiagonaCornerUpRight", ref tileData.Wall_DiagonaCornerUpRight);
            EditorGUILayout.EndVertical();

            GUILayout.Space(10);

            // Island - Horizontal Tiles Header
            EditorGUILayout.LabelField("Island - Horizontal Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            DrawTileRow("Wall_IslandHorizontalLeft", ref tileData.Wall_IslandHorizontalLeft);
            GUILayout.Space(Space);
            DrawTileRow("Wall_IslandHorizontalMiddle", ref tileData.Wall_IslandHorizontalMiddle);
            GUILayout.Space(Space);
            DrawTileRow("Wall_IslandHorizontalRight", ref tileData.Wall_IslandHorizontalRight);

            GUILayout.Space(10);

            // Island - Vertical Tiles Header
            EditorGUILayout.LabelField("Island - Vertical Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            DrawTileRow("Wall_IslandVerticalTop", ref tileData.Wall_IslandVerticalTop);
            GUILayout.Space(Space);
            DrawTileRow("Wall_IslandVerticalMiddle", ref tileData.Wall_IslandVerticalMiddle);
            GUILayout.Space(Space);
            DrawTileRow("Wall_IslandVerticalBottom", ref tileData.Wall_IslandVerticalBottom);

            GUILayout.Space(10);

            // Others Wall Tiles Header
            EditorGUILayout.LabelField("Other Wall Tiles", _subTitleStyle);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            DrawTileRow("Wall_SideFull", ref tileData.Wall_SideFull);

            // Mark as dirty if GUI changed
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

            // Label and Tile setting
            GUILayout.BeginVertical(GUILayout.Width(300));
            EditorGUILayout.LabelField(label, _labelStyle);
            tile = (Tile)EditorGUILayout.ObjectField(tile, typeof(Tile), false);
            GUILayout.EndVertical();

            // Add flexible space to push the sprite to the right
            GUILayout.FlexibleSpace();

            // Tile sprite preview
            GUILayout.BeginVertical(GUILayout.Width(60));
            GUILayout.FlexibleSpace();
            Rect rect = GUILayoutUtility.GetRect(50, 50, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
            if (tile != null && tile.sprite != null)
            {
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
            }
            else
            {
                EditorGUI.DrawRect(rect, Color.gray);
                GUI.Label(rect, "No Tile", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

    }

}

#endif
