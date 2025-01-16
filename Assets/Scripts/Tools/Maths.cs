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

    }
}
