using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Bedroom.Minesweeper.Levels
{
    public class SceneGraph : ISceneGraphNode
    {
        #region Private Fields

        private List<ISceneGraphNode> children;

        #endregion Private Fields

        #region Public Constructors

        public SceneGraph()
        {
            children = new List<ISceneGraphNode>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IReadOnlyList<ISceneGraphNode> Children { get { return children; } }
        public Vector2 LocalPosition { get => Vector2.Zero; set { } }
        public float LocalRotation { get => 0f; set { } }
        public Vector2 LocalScale { get => Vector2.Zero; set { } }
        public ISceneGraphNode Parent { get => null; set { } }
        public Vector2 Position { get => Vector2.Zero; set { } }

        public ISceneGraphNode Root => this;
        public float Rotation { get => 0f; set { } }

        public Vector2 Scale { get => Vector2.Zero; }

        #endregion Public Properties

        #region Public Methods

        public void AddChild(ISceneGraphNode node)
        {
            if (children.Contains(node))
                return;

            if (node.Parent != null)
                node.Parent.RemoveChild(node);
            node.Parent = null;

            children.Add(node);
        }

        public void Draw(GameTime deltaTime) => children.ForEach(c => c.Draw(deltaTime));

        public IEnumerable<ISceneGraphNode> EnumerateChildren() => children;

        public void RemoveChild(ISceneGraphNode node)
        {
            children.Remove(node);
            node.Parent = null;
        }

        public void Update(GameTime deltaTime) => children.ForEach(c => c.Update(deltaTime));

        #endregion Public Methods
    }
}