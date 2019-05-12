using Bedroom.Minesweeper.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bedroom.Minesweeper.Loading
{
    public sealed class GameLibrary
    {
        #region Public Constructors

        public GameLibrary(string filename, string rootNamespace, string rootType)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("The filename of the game library must be defined, but an empty string or a null value was found.", nameof(filename));

            if (!File.Exists(filename))
                throw new FileNotFoundException("The specified game library file could not be found.", filename);

            if (string.IsNullOrWhiteSpace(rootNamespace))
                throw new ArgumentException("The root namespace of the game library must be defined, but an empty string or a null value was found.", nameof(rootNamespace));

            if (string.IsNullOrWhiteSpace(rootType))
                throw new ArgumentException("The root type of the game library must be defined, but an empty string or a null value was found.", nameof(rootType));

            Filename = filename;
            RootNamespace = rootNamespace;
            RootType = rootType;
        }

        internal void CreateInstance()
        {
            if (!Loaded)
                return;

            Type maintype = LoadedAssembly.DefinedTypes.Where(info => info.Namespace == RootNamespace && info.Name == RootType).First();
            GameMainType = Activator.CreateInstance(maintype) as IGameMain;
            if (GameMainType == null)
                throw new GameMainLoadException();

            GameMainType.Load();
        }

        #endregion Public Constructors

        #region Public Properties

        public IGameMain GameMainType { get; private set; }

        public string Filename { get; private set; }

        public string RootNamespace { get; private set; }

        public string RootType { get; private set; }

        public Assembly LoadedAssembly { get; private set; }

        public bool Loaded { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Load()
        {
            Assembly assembly = Assembly.LoadFrom(Filename);
            Loaded = assembly != null;
            if (!Loaded)
                throw new GameLoadException(this);
            LoadedAssembly = assembly;
        }

        #endregion Public Methods
    }
}