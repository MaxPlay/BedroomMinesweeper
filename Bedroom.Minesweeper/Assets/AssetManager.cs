using Bedroom.Minesweeper.Exceptions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bedroom.Minesweeper.Assets
{
    public class AssetManager
    {
        #region Private Fields

        private ContentManager contentManager;

        #endregion Private Fields

        #region Public Constructors

        public AssetManager(ContentManager contentManager)
        {
            if (Instance != null)
                throw new SingletonException<AssetManager>();
            this.contentManager = contentManager;
            Textures = new AssetContainer<Texture2D>(contentManager);
            SoundEffects = new AssetContainer<SoundEffect>(contentManager);
        }

        #endregion Public Constructors

        #region Public Properties

        public static Texture2D Pixel { get; set; }

        public static AssetManager Instance { get; private set; }

        public AssetContainer<SoundEffect> SoundEffects { get; private set; }
        public AssetContainer<Texture2D> Textures { get; private set; }

        #endregion Public Properties
    }
}