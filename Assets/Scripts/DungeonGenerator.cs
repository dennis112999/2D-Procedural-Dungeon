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

            if(_dgWalkDataSO.EnableRoomDGGeneration)
            {
                CreateRooms();
            }
            else
            {
                HashSet<Vector2Int> floorPositions = GenerateDungeonFloor();
                GenerateDungeonWall(floorPositions);
            }
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
            HashSet<Vector2Int> cornerPositions = WallGenerator.FindWallsInDirections(floorPositions, Maths.EightDirections);
            Dictionary<Vector2Int, string> cornerBinaryMap = WallGenerator.CalculateWallNeighborTypes(cornerPositions, floorPositions);

            _tilemapController.PaintSingleWall(cornerBinaryMap);
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
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

            if (_dgWalkDataSO.IsRandomStartPos)
            {
                floorPositions = CreateRoomsRandomly(roomList);
            }
            else
            {
                floorPositions = CreateSampleRooms(roomList, _dgWalkDataSO.Offset);
            }

            // Connect Rooms
            List<Vector2Int> roomCenters = GetRoomCenters(roomList);
            HashSet<Vector2Int> roadPositions = ConnectRooms(roomCenters);
            floorPositions.UnionWith(roadPositions);

            // Paint Floor Tiles
            _tilemapController.PaintFloorTiles(floorPositions);

            // Paint Wall Tiles
            GenerateDungeonWall(floorPositions);
        }

        /// <summary>
        /// Connect Rooms
        /// </summary>
        /// <param name="roomList"></param>
        /// <returns></returns>
        private List<Vector2Int> GetRoomCenters(List<BoundsInt> roomList)
        {
            List<Vector2Int> roomCenters = new List<Vector2Int>();
            foreach (var room in roomList)
            {
                roomCenters.Add(((Vector2Int)Vector3Int.RoundToInt(room.center)));
            }

            return roomCenters;
        }

        /// <summary>
        /// Connect Rooms
        /// </summary>
        /// <param name="roomCenters">room Centers</param>
        /// <returns>roads</returns>
        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> rooms = new HashSet<Vector2Int>();

            // Randomly select the starting room center
            var curRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(curRoomCenter);

            while (roomCenters.Count > 0)
            {
                Vector2Int closeset = Maths.FindClosestPoint(curRoomCenter, roomCenters);
                roomCenters.Remove(closeset);

                HashSet<Vector2Int> newCorridor = CreateRoad(curRoomCenter, closeset);
                
                curRoomCenter = closeset;
                rooms.UnionWith(newCorridor);
            }

            return rooms;
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

        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

            foreach (var roomBounds in roomList)
            {
                // Calculate the center of the room
                var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                var roomFloor = RunRandomWalk(_dgWalkDataSO, roomCenter);

                // Add valid positions to the floor set
                AddValidFloorPositions(roomBounds, roomFloor, floor);
            }

            return floor;
        }

        /// <summary>
        /// Add valid positions to the floor set
        /// </summary>
        /// <param name="roomBounds">Bounds of the room</param>
        /// <param name="roomFloor">Generated floor positions from random walk</param>
        /// <param name="floor">The set to store valid floor positions</param>
        private void AddValidFloorPositions(BoundsInt roomBounds, HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> floor)
        {
            int xMin = roomBounds.xMin + _dgWalkDataSO.Offset;
            int xMax = roomBounds.xMax - _dgWalkDataSO.Offset;
            int yMin = roomBounds.yMin + _dgWalkDataSO.Offset;
            int yMax = roomBounds.yMax - _dgWalkDataSO.Offset;

            foreach (var pos in roomFloor)
            {
                if (pos.x >= xMin && pos.x <= xMax && pos.y >= yMin && pos.y <= yMax)
                {
                    floor.Add(pos);
                }
            }
        }

        #endregion Room

        #region Road

        private HashSet<Vector2Int> CreateRoad(Vector2Int curRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> road = new HashSet<Vector2Int>();
            var pos = curRoomCenter;
            road.Add(pos);

            MoveVertically(ref pos, destination.y, road);
            MoveHorizontally(ref pos, destination.x, road);

            return road;
        }

        private void MoveVertically(ref Vector2Int pos, int targetY, HashSet<Vector2Int> road)
        {
            while (pos.y != targetY)
            {
                if (targetY > pos.y)
                {
                    pos += Vector2Int.up;
                }
                else if (targetY < pos.y)
                {
                    pos += Vector2Int.down;
                }

                road.Add(pos);
            }
        }

        private void MoveHorizontally(ref Vector2Int pos, int targetX, HashSet<Vector2Int> road)
        {
            while (pos.x != targetX)
            {
                if (targetX > pos.x)
                {
                    pos += Vector2Int.right;
                }
                else if (targetX < pos.x)
                {
                    pos += Vector2Int.left;
                }

                road.Add(pos);
            }
        }

        #endregion Road
    }

}