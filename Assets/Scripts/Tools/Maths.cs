using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class Maths
    {
        /// <summary>
        /// A list of basic 2D directions - Up, Right, Down, Left
        /// </summary>
        public static List<Vector2Int> BasicDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0,1),  // Up
            new Vector2Int(1,0),  // Right
            new Vector2Int(0,-1), // Down
            new Vector2Int(-1,0), // Left
        };

        /// <summary>
        /// A list of 2D directions covering all eight cardinal and diagonal directions
        /// Order: Up, Up-Right, Right, Down-Right, Down, Down-Left, Left, Up-Left
        /// </summary>
        public static readonly List<Vector2Int> EightDirections = new List<Vector2Int>
        {
            new Vector2Int(0, 1),   // Up
            new Vector2Int(1, 1),   // Up-Right
            new Vector2Int(1, 0),   // Right
            new Vector2Int(1, -1),  // Down-Right
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, -1), // Down-Left
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(-1, 1),  // Up-Left
        };

        public static Vector2Int GetRandomBasicDirection()
        {
            return BasicDirectionsList[Random.Range(0, BasicDirectionsList.Count)];
        }

        /// <summary>
        /// Find Closest Point
        /// </summary>
        /// <param name="curPos">current Pos</param>
        /// <param name="checkPoints">check Points List</param>
        /// <returns></returns>
        public static Vector2Int FindClosestPoint(Vector2Int curPos, List<Vector2Int> checkPoints)
        {
            Vector2Int closestPos = Vector2Int.zero;
            float length = float.MaxValue;

            foreach (Vector2Int pos in checkPoints)
            {
                float curDis = Vector2.Distance(curPos, pos);
                if (curDis < length)
                {
                    length = curDis;
                    closestPos = pos;
                }
            }

            return closestPos;
        }

    }
}
