using Microsoft.Xna.Framework;
using System;

namespace Bedroom.Minesweeper.Levels
{
    public abstract class Level
    {

        #region Public Constructors

        // A man needs a name.
        public Level(string name)
        {
            Name = name;
            LevelManager.Instance.Register(this);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; private set; }

        #endregion Public Properties

        #region Protected Properties

        protected SceneGraph SceneGraph { get; private set; }

        public bool Initialized { get; private set; }

        #endregion Protected Properties

        #region Public Methods

        public void DoDraw(GameTime gameTime)
        {
            SceneGraph.Draw(gameTime);
        }

        public void DoUpdate(GameTime gameTime)
        {
            if (!Initialized)
            {
                Init();
                Initialized = true;
            }

            SceneGraph.Update(gameTime);
        }

        #endregion Public Methods

        #region Internal Methods

        public abstract void DeInit();

        public abstract void Init();

        #endregion Internal Methods

        #region Protected Methods

        protected abstract void Draw(GameTime deltaTime);

        protected abstract void Update(GameTime deltaTime);

        #endregion Protected Methods
    }
}