using Bedroom.Minesweeper.Assets;
using Bedroom.Minesweeper.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bedroom.Minesweeper
{
    /// <summary>
    /// This is the main type of the game. It is called "Core", because it is the central point where
    /// everything takes place.
    /// </summary>
    public class Core : Game
    {
        #region Public Constructors

        public Core()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            new AssetManager(Content);
            new LevelManager();
        }

        #endregion Public Constructors

        #region Public Properties

        public static GraphicsDeviceManager Graphics { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            LevelManager.Instance.Draw(gameTime);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run. This is
        /// where it can query for any required services and load any non-graphic related content.
        /// Calling base.Initialize will enumerate through any components and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            AppData.Load();
            Debug.Setup(); // Debug is not fully usable until the console pops up which happens in the next frame
            // I know it is already loaded the Args in Debug.Setup(), but when it comes to the point
            // where we change things, we do not want the software to break
            CommandLineArguments.Load();
            Input.Init();
            Input.RegisterClick(Keys.Escape, Exit);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            // This is a dummy/fallback graphic that may be used to draw debug stuff or anything, really
            AssetManager.Pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            AssetManager.Pixel.SetData(new Color[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            LevelManager.Instance.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world, checking for collisions,
        /// gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            LevelManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        #endregion Protected Methods
    }
}