using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class WallGenerator
    {
        /// <summary>
        /// Calculate Wall NeighborTypes
        /// </summary>
        /// <param name="WallPositions"></param>
        /// <param name="floorPos"></param>
        /// <returns></returns>
        public static Dictionary<Vector2Int, string> CalculateWallNeighborTypes(HashSet<Vector2Int> WallPositions, HashSet<Vector2Int> floorPos)
        {
            Dictionary<Vector2Int, string> wallBinaryMap = new Dictionary<Vector2Int, string>();

            foreach (var pos in WallPositions)
            {
                string neighboursBinaryType = "";

                foreach (var neighbour in Maths.EightDirections)
                {
                    var neighbourPos = pos + neighbour;
                    if (floorPos.Contains(neighbourPos))
                    {
                        neighboursBinaryType += "1";
                    }
                    else
                    {
                        neighboursBinaryType += "0";
                    }
                }

                wallBinaryMap[pos] = neighboursBinaryType;
            }

            return wallBinaryMap;
        }

        /// <summary>
        /// Find Walls by the directions
        /// </summary>
        /// <param name="floorPositions"></param>
        /// <param name="directionsList"></param>
        /// <returns>walla</returns>
        public static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
        {
            HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

            foreach (var pos in floorPositions)
            {
                foreach (var direction in directionsList)
                {
                    var neighbourPos = pos + direction;

                    if (!floorPositions.Contains(neighbourPos))
                    {
                        wallPositions.Add(neighbourPos);
                    }
                }
            }

            return wallPositions;
        }

    }
}
