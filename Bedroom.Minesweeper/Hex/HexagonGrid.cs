using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Hex
{
    public class HexagonGrid
    {
        public Point Size { get; set; }

        private Hexagon[,] grid;

        public HexagonGrid(int width, int height)
        {
            Size = new Point(width, height);
            grid = new Hexagon[width, height];
            float offset;
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    offset = x % 2 == 0 ? 0.5f : 0f;
                    grid[x, y].Center = new Vector2(x, y) * Hexagon.Size + new Vector2(-Hexagon.Size.X * 0.25f * x, offset * Hexagon.Size.Y);
                }
            }
        }

        public bool GetHexagon(int x, int y, out Hexagon hexagon)
        {
            hexagon = new Hexagon();
            if (x < 0 && y < 0 && x >= Size.X && y >= Size.Y)
                return false;
            hexagon = grid[x, y];
            return true;
        }

        public Point GetRelative(Point location, RelativeHex relative)
        {
            bool isOffset = location.X % 2 == 0;
            switch (relative)
            {
                case RelativeHex.Up:
                    return new Point(location.X, location.Y - 1);
                case RelativeHex.RightUp:
                    return new Point(location.X + 1, isOffset ? location.Y : location.Y - 1);
                case RelativeHex.RightDown:
                    return new Point(location.X + 1, isOffset ? location.Y + 1 : location.Y);
                case RelativeHex.Down:
                    return new Point(location.X, location.Y + 1);
                case RelativeHex.LeftDown:
                    return new Point(location.X - 1, isOffset ? location.Y : location.Y - 1);
                case RelativeHex.LeftUp:
                    return new Point(location.X - 1, isOffset ? location.Y + 1 : location.Y);
                default:
                    return new Point();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Point highlight, Point[] secondaryHighlight, Texture2D texture)
        {
            spriteBatch.Begin();
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    spriteBatch.Draw(texture, grid[x, y].Center, highlight == new Point(x, y) ? Color.Red : secondaryHighlight.Contains(new Point(x, y)) ? Color.Pink : Color.White);
                }
            }
            spriteBatch.End();
        }

        public Point GetHexagonFromPositionOverGrid(Vector2 position)
        {
            Point closest = new Point();
            float smallestDistance = float.MaxValue;
            float currentDistance;
            Vector2 halfHex = Hexagon.Size * 0.5f;
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    currentDistance = Vector2.DistanceSquared(grid[x, y].Center + halfHex, position);
                    if (currentDistance < smallestDistance)
                    {
                        closest.X = x;
                        closest.Y = y;
                        smallestDistance = currentDistance;
                    }
                }
            }
            return closest;
        }
    }
}
