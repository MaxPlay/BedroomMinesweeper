using Bedroom.Minesweeper.Game.Levels;
using Bedroom.Minesweeper.Levels;
using Bedroom.Minesweeper.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Game
{
    public class Main : IGameMain
    {
        public void Load()
        {
            Core.Instance.IsMouseVisible = true;
            new TestLevel("test");
            LevelManager.Instance.Load("test");
        }

        public void Unload()
        {

        }
    }
}
