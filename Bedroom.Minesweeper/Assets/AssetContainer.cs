using Bedroom.Minesweeper.Exceptions;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Bedroom.Minesweeper.Assets
{
    public class AssetContainer<T> where T : class
    {
        #region Private Fields

        private static int idGenerator; // I guess 4294967295 ids should be enough for all the assets in this game

        /// <summary>
        /// The storage for all the ids by name.
        /// </summary>
        private Dictionary<string, int> assetLookup;

        /// <summary>
        /// The storage for all the assets by id.
        /// </summary>
        private Dictionary<int, T> assets;

        /// <summary>
        /// A reference to the content manager.
        /// </summary>
        private ContentManager contentManager;

        #endregion Private Fields

        #region Public Constructors

        public AssetContainer(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            assets = new Dictionary<int, T>();
            assetLookup = new Dictionary<string, int>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The amount of loaded assets.
        /// </summary>
        public int Count { get { return assets.Count; } }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets an asset by id. If the id is not found, the asset will be null.
        /// </summary>
        /// <param name="id">The id of the asset.</param>
        /// <returns>The requested asset.</returns>
        public T GetAsset(int id)
        {
            if (!assets.ContainsKey(id))
                return null;

            return assets[id];
        }

        /// <summary>
        /// Gets an asset by name. If the id is not found, the asset will be null.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <returns>The requested asset.</returns>
        public T GetAsset(string name)
        {
            if (!assetLookup.ContainsKey(name))
                return null;

            return GetAsset(assetLookup[name]);
        }

        /// <summary>
        /// Loads an asset by the content loader.
        /// </summary>
        /// <param name="name">The name of the asset in the application.</param>
        /// <param name="file">The filename of the asset.</param>
        /// <returns>The id of the loaded asset.</returns>
        public int LoadAsset(string name, string file)
        {
            int id = ValidateLoadingRequest(name, file);

            // We have a valid id, we can now load the asset
            T asset = contentManager.Load<T>(file);

            assetLookup.Add(name, id);
            assets.Add(id, asset);
            return id;
        }

        /// <summary>
        /// Allows loading an asset from file (not as an xnb). This can be done with custom files or
        /// anything per runtime.
        /// </summary>
        /// <param name="name">The name of the asset in the application.</param>
        /// <param name="file">The filename of the asset.</param>
        /// <param name="importer">The importer that is used to import the asset.</param>
        /// <returns>The id of the loaded asset.</returns>
        public int LoadAssetFromFile(string name, string file, IAssetImporter<T> importer)
        {
            if (importer == null)
                throw new System.ArgumentNullException(nameof(importer));

            int id = ValidateLoadingRequest(name, file);

            T asset = importer.Load(file);

            assetLookup.Add(name, id);
            assets.Add(id, asset);
            return id;
        }

        #endregion Public Methods

        #region Private Methods

        private int ValidateLoadingRequest(string name, string file)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("The asset name cannot be null, empty or just whitespace. A man needs a name!", nameof(name));

            if (string.IsNullOrWhiteSpace(file))
                throw new System.ArgumentException("The file name cannot be null, empty or just whitespace.", nameof(file));

            if (assetLookup.ContainsKey(name))
                throw new AssetLoadException<T>($"An asset with the name \"{name}\" of type {nameof(T)} was already loaded.");

            // We use unchecked, because we actually want the integer overflow. In case we reach
            // int.MaxValue, we want to get the next id in the safe range (this would be int.MinValue).

            int id;
            unchecked
            {
                do
                {
                    id = idGenerator++;
                } while (assets.ContainsKey(id));
            }
            return id;
        }

        #endregion Private Methods
    }
}