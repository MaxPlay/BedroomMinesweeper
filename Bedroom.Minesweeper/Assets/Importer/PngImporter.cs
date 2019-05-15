using Bedroom.Minesweeper.Exceptions;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Assets.Importer
{
    public class PngImporter : IAssetImporter<Texture2D>
    {
        public Texture2D Load(string file)
        {
            file = Path.Combine(AppData.ApplicationFolder, file);
            if (!File.Exists(file))
                throw new FileNotFoundException($"Could not find file {file}.", file);

            Texture2D texture;
            using (FileStream stream = File.OpenRead(file))
            {
                texture = Texture2D.FromStream(Core.Graphics.GraphicsDevice, stream);
            }
            return texture;
        }
    }
}
