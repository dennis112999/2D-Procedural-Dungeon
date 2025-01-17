using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

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

        #region Room

        public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            List<BoundsInt> roomsList = new List<BoundsInt>();
            RecursiveSplit(spaceToSplit, minWidth, minHeight, roomsList);
            return roomsList;
        }

        private static void RecursiveSplit(BoundsInt room, int minWidth, int minHeight, List<BoundsInt> roomsList)
        {
            // Check if the room can be split
            if (room.size.x < minWidth || room.size.y < minHeight) return;

            bool splitHorizontally = Random.value < 0.5f;

            // Try splitting the room
            if (splitHorizontally && room.size.y >= minHeight * 2)
            {
                SplitHorizontally(room, minWidth, minHeight, roomsList);
            }
            else if (!splitHorizontally && room.size.x >= minWidth * 2)
            {
                SplitVertically(room, minWidth, minHeight, roomsList);
            }
            else
            {
                roomsList.Add(room);
            }
        }

        private static void SplitHorizontally(BoundsInt room, int minWidth, int minHeight, List<BoundsInt> roomsList)
        {
            int splitY = Random.Range(1, room.size.y);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, splitY, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + splitY, room.min.z),
                                            new Vector3Int(room.size.x, room.size.y - splitY, room.size.z));

            RecursiveSplit(room1, minWidth, minHeight, roomsList);
            RecursiveSplit(room2, minWidth, minHeight, roomsList);
        }

        private static void SplitVertically(BoundsInt room, int minWidth, int minHeight, List<BoundsInt> roomsList)
        {
            int splitX = Random.Range(1, room.size.x);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(splitX, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + splitX, room.min.y, room.min.z),
                                            new Vector3Int(room.size.x - splitX, room.size.y, room.size.z));

            RecursiveSplit(room1, minWidth, minHeight, roomsList);
            RecursiveSplit(room2, minWidth, minHeight, roomsList);
        }

        #endregion Room
    }
}