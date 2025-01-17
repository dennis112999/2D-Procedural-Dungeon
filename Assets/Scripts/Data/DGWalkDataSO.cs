using System;
using UnityEngine;

namespace DG.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "DGWalkDataSO_", menuName = "DG/Data/SimpleRandomWalkSO")]
    public class DGWalkDataSO : ScriptableObject
    {
        /// <summary>
        /// Number of times the random walk process
        /// </summary>
        [Tooltip("Number of times the random walk process")]
        public int Repeats = 10;

        /// <summary>
        /// Length of each random walk
        /// </summary>
        [Tooltip("Length of each random walk")]
        public int WalkLength = 10;

        /// <summary>
        /// Random Start Pos each walk
        /// </summary>
        [Tooltip("Is Random Start Pos each walk process")]
        public bool IsRandomStartPos = true;

        /// <summary>
        /// The start pos for walk process
        /// </summary>
        [Tooltip("The start pos for walk process")]
        public Vector2Int StartPos = Vector2Int.zero;


        /// <summary>
        /// Minimum width of a room in the dungeon.
        /// </summary>
        [Tooltip("Minimum width of a room in the dungeon")]
        [Min(1)] public int MinRoomWidth = 4;

        /// <summary>
        /// Minimum height of a room in the dungeon.
        /// </summary>
        [Tooltip("Minimum height of a room in the dungeon")]
        [Min(1)] public int MinRoomHeight = 4;

        /// <summary>
        /// Width of the dungeon generation area.
        /// </summary>
        [Tooltip("Width of the dungeon generation area")]
        [Min(1)] public int DGWidth = 20;

        /// <summary>
        /// Height of the dungeon generation area.
        /// </summary>
        [Tooltip("Height of the dungeon generation area")]
        [Min(1)] public int DGHeight = 20;

        /// <summary>
        /// Offset value to adjust room positions and spacing.
        /// </summary>
        [Tooltip("Offset value to adjust room positions and spacing")]
        [Range(0, 10)] public int Offset = 1;
    }
}
