using Microsoft.Xna.Framework;

namespace Bedroom.Minesweeper.ECS
{
    public interface IDrawableComponent
    {
        #region Public Methods

        void Draw(GameTime deltaTime);

        #endregion Public Methods
    }
}