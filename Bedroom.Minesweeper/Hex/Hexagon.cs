using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Hex
{
    public struct Hexagon
    {
        /// <summary>
        /// The center of a hexagon differs from its location in the array, it is used for rendering
        /// </summary>
        public Vector2 Center;
        /// <summary>
        /// We can make the size static, since it is global
        /// </summary>
        public static Vector2 Size;
    }
}
