using Microsoft.Xna.Framework;

namespace Bedroom.Minesweeper.ECS
{
    public interface IUpdatableComponent
    {
        #region Public Methods

        void Update(GameTime deltaTime);

        #endregion Public Methods
    }
}