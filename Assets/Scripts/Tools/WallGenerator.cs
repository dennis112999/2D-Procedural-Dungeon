using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class WallGenerator
    {

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
