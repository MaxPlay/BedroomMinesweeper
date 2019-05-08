using Bedroom.Minesweeper.ECS;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            entities = new List<Entity>();
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Initialized { get; private set; }
        public string Name { get; private set; }

        #endregion Public Properties

        #region Protected Properties

        List<Entity> entities;

        #endregion Protected Properties

        #region Public Methods

        public abstract void DeInit();

        public void DoDraw(GameTime gameTime)
        {
            entities.ForEach(c => c.Draw(gameTime));
        }

        public void DoUpdate(GameTime gameTime)
        {
            if (!Initialized)
            {
                Init();
                Initialized = true;
            }

            entities.ForEach(c => c.Update(gameTime));
        }

        public abstract void Init();

        #endregion Public Methods

        #region Protected Methods

        protected abstract void Draw(GameTime deltaTime);

        protected abstract void Update(GameTime deltaTime);

        #endregion Protected Methods
    }
}