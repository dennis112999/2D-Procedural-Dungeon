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
    }
}
