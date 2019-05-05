using Bedroom.Minesweeper.Exceptions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Bedroom.Minesweeper.Levels
{
    public class LevelManager : IDisposable
    {
        #region Private Fields

        private static short idGenerator; // There won't be more levels in this game than a short can hold (65536)

        private bool disposedValue = false;
        private Dictionary<string, short> levelLookup;
        private Dictionary<short, Level> levels;
        private List<Level> loadedLevels;

        #endregion Private Fields

        #region Public Constructors

        public LevelManager()
        {
            if (Instance != null)
                throw new SingletonException<LevelManager>();
            Instance = this;

            levels = new Dictionary<short, Level>();
            levelLookup = new Dictionary<string, short>();
            loadedLevels = new List<Level>();
        }

        #endregion Public Constructors

        #region Public Properties

        public static LevelManager Instance { get; private set; }

        #endregion Public Properties

        #region Public Methods
        
        public void Dispose()
        {
            Dispose(true);
        }

        public void Draw(GameTime gameTime)
        {
            loadedLevels.ForEach(l => l.DoDraw(gameTime));
        }

        public Level GetLevel(string name)
        {
            if (!levelLookup.ContainsKey(name))
                return null;
            return GetLevel(levelLookup[name]);
        }

        public Level GetLevel(short id)
        {
            if (!levels.ContainsKey(id))
                return null;
            return levels[id];
        }

        public void Load(string name)
        {
            if (!levelLookup.ContainsKey(name))
                throw new LevelNotFoundException(name);

            Load(levelLookup[name]);
        }

        public void Load(short id)
        {
            loadedLevels.ForEach(l => l.DeInit());
            Level level = GetLevel(id);
            // Init will be called in first update
            loadedLevels.Add(level);
        }

        public void LoadAdditive(string name)
        {
            if (!levelLookup.ContainsKey(name))
                throw new LevelNotFoundException(name);

            LoadAdditive(levelLookup[name]);
        }

        public void LoadAdditive(short id)
        {
            Level level = GetLevel(id);
            // Init will be called in first update
            loadedLevels.Add(level);
        }

        public void Register(Level level)
        {
            short id = Validate(level);

            levels.Add(id, level);
            levelLookup.Add(level.Name, id);
        }

        public void Update(GameTime gameTime)
        {
            loadedLevels.ForEach(l => l.DoUpdate(gameTime));
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Debug.Log("Disposing Level Manager.");

                if (disposing)
                {
                    loadedLevels.ForEach(l => l.DeInit());
                }

                levels.Clear();
                loadedLevels.Clear();

                Debug.Log("Level Manager Disposed.");

                disposedValue = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private short Validate(Level level)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            if (string.IsNullOrWhiteSpace(level.Name))
                throw new ArgumentException("A level name may not be null, empty or pure whitespace.", nameof(level.Name));

            if (levelLookup.ContainsKey(level.Name))
                throw new LevelRegisterException(level.Name, level);

            // We want an overflow to happen if we reach short.MaxValue, so wie put this into an
            // unchecked statement

            short id;
            short originalId = idGenerator;
            unchecked
            {
                do
                {
                    id = idGenerator++;
                    if (idGenerator == originalId)
                        throw new IndexOutOfRangeException("The maximum amount of levels is reached.");
                } while (!levels.ContainsKey(id));
            }

            return id;
        }

        #endregion Private Methods
    }
}