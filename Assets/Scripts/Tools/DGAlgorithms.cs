using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// Dungeon generation algorithms
    /// </summary>
    public static class DGAlgorithms
    {
        /// <summary>
        /// Generates a random path using a simple random walk algorithm
        /// </summary>
        /// <param name="startPos">generation start Pos</param>
        /// <param name="walkStep">steps to take in the random walk</param>
        /// <returns>all unique positions visited</returns>
        public static HashSet<Vector2Int> RandomWalkPath(Vector2Int startPos, int walkStep)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();

            // Add the starting position to the path
            path.Add(startPos);
            var prevPos = startPos;

            // Perform the random walk for the specified number of steps
            for (int i = 0; i < walkStep; i++)
            {
                var newPos = prevPos + Maths.GetRandomBasicDirection();
                path.Add(newPos);
                prevPos = newPos;
            }

            return path;
        }
    }
}