using UnityEngine;
using UnityEngine.Tilemaps;

namespace DG.Data
{
    [CreateAssetMenu(fileName = "TileDataSO_", menuName = "DG/Data/TileDataSO", order = 1)]
    public class TileDataSO : ScriptableObject
    {
        [Header("Tile Data")]
        public Tile Floor;
        public Tile Wall;
    }
}
