using Bedroom.Minesweeper.Exceptions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Assets
{
    public class AssetManager
    {
        private ContentManager contentManager;

        public static AssetManager Instance { get; private set; }

        public AssetContainer<Texture2D> Textures { get; private set; }

        public AssetContainer<SoundEffect> SoundEffects { get; private set; }

        public AssetManager(ContentManager contentManager)
        {
            if (Instance != null)
                throw new SingletonException<AssetManager>();
            this.contentManager = contentManager;
            Textures = new AssetContainer<Texture2D>(contentManager);
            SoundEffects = new AssetContainer<SoundEffect>(contentManager);
        }
    }
}
