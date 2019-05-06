using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bedroom.Minesweeper.Levels
{
    public class SceneGraph : ISceneGraphNode
    {
        private List<ISceneGraphNode> children;

        public Vector2 Position { get => Vector2.Zero; set { } }

        public float Rotation { get => 0f; set { } }

        public Vector2 Scale { get => Vector2.Zero; }

        public ISceneGraphNode Parent { get => null; set { } }

        public IReadOnlyList<ISceneGraphNode> Children { get { return children; } }

        public ISceneGraphNode Root => this;

        public Vector2 LocalPosition { get => Vector2.Zero; set { } }
        public float LocalRotation { get => 0f; set { } }
        public Vector2 LocalScale { get => Vector2.Zero; set { } }

        public void AddChild(ISceneGraphNode node)
        {
            if (children.Contains(node))
                return;

            if (node.Parent != null)
                node.Parent.RemoveChild(node);
            node.Parent = null;

            children.Add(node);
        }

        #region Public Methods

        public void Draw(GameTime gameTime)
        {
            // TODO: Draw nodes here
        }

        public IEnumerable<ISceneGraphNode> EnumerateChildren()
        {
            return children;
        }

        public void RemoveChild(ISceneGraphNode node)
        {
            children.Remove(node);
            node.Parent = null;
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Update nodes here
        }

        #endregion Public Methods
    }
}