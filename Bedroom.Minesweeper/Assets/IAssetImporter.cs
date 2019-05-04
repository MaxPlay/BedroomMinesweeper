namespace Bedroom.Minesweeper.Assets
{
    public interface IAssetImporter<T>
    {
        T Load(string file);
    }
}