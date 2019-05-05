namespace Bedroom.Minesweeper.Assets
{
    public interface IAssetImporter<T>
    {
        #region Public Methods

        T Load(string file);

        #endregion Public Methods
    }
}