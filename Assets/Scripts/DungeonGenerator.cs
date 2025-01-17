using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

using DG.Data;
using Tools;

namespace DG.Gameplay
{
    public class DungeonGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DGWalkDataSO _dgWalkDataSO;
        [SerializeField] protected TilemapController _tilemapController;

        #region Dungeon

        public void GenerateDungeon()
        {
            _tilemapController.Clear();

            //HashSet<Vector2Int> floorPositions = GenerateDungeonFloor();
            //GenerateDungeonWall(floorPositions);

            CreateRooms();
        }

        public void ClearDungeon()
        {
            _tilemapController.Clear();
        }

        /// <summary>
        /// Generate Dungeon Floor
        /// </summary>
        /// <returns></returns>
        private HashSet<Vector2Int> GenerateDungeonFloor()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(_dgWalkDataSO, _dgWalkDataSO.StartPos);
            _tilemapController.PaintFloorTiles(floorPositions);

            return floorPositions;
        }

        /// <summary>
        /// Generate Dungeon Wall
        /// </summary>
        /// <param name="floorPositions">floor Positions</param>
        private void GenerateDungeonWall(HashSet<Vector2Int> floorPositions)
        {
           HashSet<Vector2Int> wallPositions = WallGenerator.FindWallsInDirections(floorPositions, Maths.BasicDirectionsList);
           _tilemapController.PaintWallTiles(wallPositions);
        }

        private HashSet<Vector2Int> RunRandomWalk(DGWalkDataSO DGWalkDataSO, Vector2Int pos)
        {
            var curPos = pos;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

            for (int i = 0; i < DGWalkDataSO.Repeats; i++)
            {
                var path = DGAlgorithms.RandomWalkPath(curPos, DGWalkDataSO.WalkLength);
                floorPositions.UnionWith(path);

                if (DGWalkDataSO.IsRandomStartPos)
                {
                    curPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                }
            }

            return floorPositions;
        }

        #endregion Dungeon

        #region Room

        /// <summary>
        /// Creates rooms
        /// </summary>
        private void CreateRooms()
        {
            // Convert dungeon rooms
            var roomList = DGAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)_dgWalkDataSO.StartPos, new Vector3Int(_dgWalkDataSO.DGWidth, _dgWalkDataSO.DGHeight, 0)),
                _dgWalkDataSO.MinRoomWidth, _dgWalkDataSO.MinRoomHeight);

            // Create floor positions
            HashSet<Vector2Int> floorPositions = CreateSampleRooms(roomList, _dgWalkDataSO.Offset);
            _tilemapController.PaintFloorTiles(floorPositions);
        }

        /// <summary>
        /// Generates floor positions for rooms
        /// </summary>
        /// <param name="roomList">List of rooms</param>
        /// <param name="offset">Offset to leave as a border around the room</param>
        /// <returns>all floor positions</returns>
        private HashSet<Vector2Int> CreateSampleRooms(List<BoundsInt> roomList, int offset)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            foreach (var room in roomList)
            {
                for (int col = offset; col < room.size.x - offset; col++)
                {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                        // Calculate the absolute position
                        Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                        floor.Add(pos);
                    }
                }
            }

            return floor;
        }

        #endregion Room
    }

}