using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Bedroom.Minesweeper.Levels
{
    public interface ISceneGraphNode
    {
        #region Public Properties

        IReadOnlyList<ISceneGraphNode> Children { get; }
        Vector2 LocalPosition { get; set; }
        float LocalRotation { get; set; }
        Vector2 LocalScale { get; set; }
        ISceneGraphNode Parent { get; set; }
        Vector2 Position { get; set; }

        ISceneGraphNode Root { get; }
        float Rotation { get; set; }

        Vector2 Scale { get; }

        #endregion Public Properties

        #region Public Methods

        void AddChild(ISceneGraphNode node);

        void Draw(GameTime deltaTime);

        IEnumerable<ISceneGraphNode> EnumerateChildren();

        void RemoveChild(ISceneGraphNode node);

        void Update(GameTime deltaTime);

        #endregion Public Methods
    }
}