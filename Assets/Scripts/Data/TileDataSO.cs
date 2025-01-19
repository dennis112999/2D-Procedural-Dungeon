using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG.Data
{
    [CreateAssetMenu(fileName = "TileDataSO_", menuName = "DG/Data/TileDataSO", order = 1)]
    public class TileDataSO : ScriptableObject
    {
        [Header("Tile Data")]
        public Tile Floor;

        [Header("Wall Data")]
        public Tile Wall_Top;
        public Tile Wall_SideRight;
        public Tile Wall_Bottom;
        public Tile Wall_SideLeft;

        public Tile Wall_InnerConerDownLeft;
        public Tile Wall_InnerConerDownRight;
        public Tile Wall_InnerConerUpLeft;
        public Tile Wall_InnerConerUpRight;

        public Tile Wall_DiagonaCornerDownLeft;
        public Tile Wall_DiagonaCornerDownRight;
        public Tile Wall_DiagonaCornerUpLeft;
        public Tile Wall_DiagonaCornerUpRight;

        public Tile Wall_SideFull;

        [Header("Island")]
        public Tile Wall_IslandHorizontalLeft; 
        public Tile Wall_IslandHorizontalMiddle;
        public Tile Wall_IslandHorizontalRight;

        public Tile Wall_IslandVerticalTop;
        public Tile Wall_IslandVerticalMiddle;
        public Tile Wall_IslandVerticalBottom;
    }
}
