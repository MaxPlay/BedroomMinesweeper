using Bedroom.Minesweeper.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bedroom.Minesweeper.Loading
{
    public class GameLoading : IDisposable
    {
        #region Private Fields

        private List<GameLibrary> loadedGameLibraries;

        #endregion Private Fields

        #region Public Constructors

        public GameLoading()
        {
            if (Instance != null)
                throw new SingletonException<GameLoading>();
            Instance = this;
            loadedGameLibraries = new List<GameLibrary>();
        }

        #endregion Public Constructors

        #region Public Properties

        public static GameLoading Instance { get; private set; }

        internal GameLibrary[] LoadedGameLibraries { get { return loadedGameLibraries.ToArray(); } }

        #endregion Public Properties

        #region Public Methods

        public void Load(LoadingPreferences preferences)
        {
            string library = Path.Combine(AppData.ApplicationFolder, preferences.Library);
            GameLibrary gameLibrary = new GameLibrary(library, preferences.RootNamespace, preferences.RootType);
            gameLibrary.Load();
            gameLibrary.CreateInstance();
            loadedGameLibraries.Add(gameLibrary);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    loadedGameLibraries.ForEach(library => library.GameMainType.Unload());
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion


        #endregion Public Methods
    }
}