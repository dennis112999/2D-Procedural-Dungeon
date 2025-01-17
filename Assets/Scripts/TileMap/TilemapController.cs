using DG.Data;
using System.Collections.Generic;
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

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
        {
            PaintTiles(floorPos, _floorTileMap, _tileDataSO.Floor);
        }

        public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
        {
            PaintTiles(wallPositions, _wallTileMap, _tileDataSO.Wall);
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
