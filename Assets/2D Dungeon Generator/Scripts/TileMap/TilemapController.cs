using DG.Data;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG.Gameplay
{
    public class TilemapController : MonoBehaviour
    {
        [Header("TileMap")]
        [SerializeField] private Tilemap _floorTileMap;
        [SerializeField] private Tilemap _wallTileMap; 

        [Header("Data")]
        [SerializeField] private TileDataSO _tileDataSO;

        private Dictionary<HashSet<int>, Tile> _wallTypeToTileMapping;


        private void InitializeWallTypeToTileMapping()
        {
            if (_wallTypeToTileMapping != null) return;

            _wallTypeToTileMapping = new Dictionary<HashSet<int>, Tile>
            {
                // Basic Wall Sides
                { WallTypeDefinitions.WallTop,                     _tileDataSO.Wall_Top },
                { WallTypeDefinitions.WallSideRight,               _tileDataSO.Wall_SideRight },
                { WallTypeDefinitions.WallBottom,                  _tileDataSO.Wall_Bottom },
                { WallTypeDefinitions.WallSideLeft,                _tileDataSO.Wall_SideLeft },

                // Inner Corners
                { WallTypeDefinitions.WallInnerCornerDownLeft,     _tileDataSO.Wall_InnerConerDownLeft },
                { WallTypeDefinitions.WallInnerCornerDownRight,    _tileDataSO.Wall_InnerConerDownRight },
                { WallTypeDefinitions.WallInnerCornerUpLeft,       _tileDataSO.Wall_InnerConerUpLeft },
                { WallTypeDefinitions.WallInnerCornerUpRight,      _tileDataSO.Wall_InnerConerUpRight },

                // Diagonal Corners
                { WallTypeDefinitions.WallDiagonalCornerDownLeft,  _tileDataSO.Wall_DiagonaCornerDownLeft },
                { WallTypeDefinitions.WallDiagonalCornerDownRight, _tileDataSO.Wall_DiagonaCornerDownRight },
                { WallTypeDefinitions.WallDiagonalCornerUpLeft,    _tileDataSO.Wall_DiagonaCornerUpLeft },
                { WallTypeDefinitions.WallDiagonalCornerUpRight,   _tileDataSO.Wall_DiagonaCornerUpRight },

                // Island Walls (Horizontal)
                { WallTypeDefinitions.WallIslandHorizontalLeft,    _tileDataSO.Wall_IslandHorizontalLeft },
                { WallTypeDefinitions.WallIslandHorizontalMiddle,  _tileDataSO.Wall_IslandHorizontalMiddle },
                { WallTypeDefinitions.WallIslandHorizontalRight,   _tileDataSO.Wall_IslandHorizontalRight },

                // Island Walls (Vertical)
                { WallTypeDefinitions.WallIslandVerticalTop,       _tileDataSO.Wall_IslandVerticalTop },
                { WallTypeDefinitions.WallIslandVerticalMiddle,    _tileDataSO.Wall_IslandVerticalMiddle },
                { WallTypeDefinitions.WallIslandVerticalBottom,    _tileDataSO.Wall_IslandVerticalBottom },

                // Full Walls
                { WallTypeDefinitions.WallFull,                    _tileDataSO.Wall_SideFull },
            };
        }

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
        {
            PaintTiles(floorPos, _floorTileMap, _tileDataSO.Floor);
        }

        public void PaintSingleWall(Dictionary<Vector2Int, string> wallBinaryMap)
        {
            InitializeWallTypeToTileMapping();

            foreach (var pair in wallBinaryMap)
            {
                Vector2Int pos = pair.Key;
                string binaryType = pair.Value;

                int typeAsInt = Convert.ToInt32(binaryType, 2);
                Tile tile = GetTileForCornerWallType(typeAsInt);

                if (tile != null)
                {
                    PaintSingleTile(_wallTileMap, tile, pos);
                }
            }
        }

        private Tile GetTileForCornerWallType(int typeAsInt)
        {
            foreach (var pair in _wallTypeToTileMapping)
            {
                if (pair.Key.Contains(typeAsInt))
                {
                    return pair.Value;
                }
            }
            return null;
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
        {
            foreach (var pos in positions)
            {
                PaintSingleTile(tilemap, tile, pos);
            }
        }

        public void Clear()
        {
            _floorTileMap.ClearAllTiles();
            _wallTileMap.ClearAllTiles();
        }

        #region Tile

        /// <summary>
        /// Paint Single Tile
        /// </summary>
        /// <param name="tilemap"></param>
        /// <param name="tile"></param>
        /// <param name="pos"></param>
        private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
        {
            var tilePos = tilemap.WorldToCell((Vector3Int)pos);
            tilemap.SetTile(tilePos, tile);
        }

        #endregion Tile
    }
}
