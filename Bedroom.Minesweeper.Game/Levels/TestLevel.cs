using Bedroom.Minesweeper.Assets;
using Bedroom.Minesweeper.Assets.Importer;
using Bedroom.Minesweeper.Hex;
using Bedroom.Minesweeper.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Game.Levels
{
    public class TestLevel : Level
    {
        private int hexReference;
        private Point selectedHex;
        private Point[] adjacent;
        private HexagonGrid grid;

        public TestLevel(string name) : base(name)
        {
        }

        public override void DeInit()
        {

        }

        public override void Init()
        {
            Hexagon.Size = new Vector2(32, 28);
            grid = new HexagonGrid(10, 10);
            hexReference = AssetManager.Instance.Textures.LoadAssetFromFile("hextile", "Textures/testhex.png", new PngImporter());
            selectedHex = new Point();
            adjacent = new Point[6];
        }

        protected override void Draw(GameTime deltaTime)
        {
            grid.Draw(Core.SpriteBatch, selectedHex, adjacent, AssetManager.Instance.Textures.GetAsset(hexReference));
        }

        protected override void Update(GameTime deltaTime)
        {
            selectedHex = grid.GetHexagonFromPositionOverGrid(Mouse.GetState().Position.ToVector2());
            for (int i = 0; i < 6; i++)
            {
                adjacent[i] = grid.GetRelative(selectedHex, (RelativeHex)i);
            }
        }
    }
}
