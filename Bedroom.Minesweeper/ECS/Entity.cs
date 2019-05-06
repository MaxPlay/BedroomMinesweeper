using Bedroom.Minesweeper.ECS.Components;
using Bedroom.Minesweeper.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bedroom.Minesweeper.ECS
{
    public class Entity : ISceneGraphNode
    {
        #region Private Fields

        private List<ISceneGraphNode> children;
        private List<Component> components;
        private ISceneGraphNode parent;

        #endregion Private Fields

        #region Public Constructors

        public Entity(string name, SceneGraph root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            components = new List<Component>();
            children = new List<ISceneGraphNode>();
        }

        public Entity(string name, Vector2 position, float rotation, Vector2 scale, SceneGraph root) : this(name, root)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }

        public Entity(SceneGraph root) : this("new Entity", root)
        {
        }

        public Entity(string name, ISceneGraphNode parent, SceneGraph root) : this(name, root)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public Entity(string name, Vector2 position, float rotation, Vector2 scale, ISceneGraphNode parent, SceneGraph root) : this(name, parent, root)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }

        #endregion Public Constructors

        #region Public Properties

        public IReadOnlyList<ISceneGraphNode> Children { get { return children; } }
        public Vector2 LocalPosition { get; set; }
        public float LocalRotation { get; set; }
        public Vector2 LocalScale { get; set; }
        public string Name { get; set; }

        public ISceneGraphNode Parent
        {
            get { return parent; }
            set
            {
                parent = (value == Root) ? null : value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return Parent == null ? LocalPosition : LocalPosition + Parent.Position;
            }

            set
            {
                if (Parent == null)
                    LocalPosition = value;
                else
                    LocalPosition = value - Parent.Position;
            }
        }

        public ISceneGraphNode Root { get; private set; }

        public float Rotation
        {
            get
            {
                return Parent == null ? LocalRotation : LocalRotation + Parent.Rotation;
            }

            set
            {
                if (Parent == null)
                    LocalRotation = value;
                else
                    LocalRotation = value - Parent.Rotation;
            }
        }

        public Vector2 Scale { get => Parent == null ? LocalScale : LocalScale * Parent.Scale; }

        #endregion Public Properties

        #region Public Methods

        public void AddChild(ISceneGraphNode node)
        {
            if (node == Root)
                return;

            if (!children.Contains(node))
                children.Add(node);

            if (node.Parent != null)
                node.Parent.RemoveChild(node);
            node.Parent = this;
        }

        public void RemoveChild(ISceneGraphNode node)
        {
            children.Remove(node);
            node.Parent = null;
        }

        public IEnumerable<ISceneGraphNode> EnumerateChildren()
        {
            return children;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public T AddComponent<T>() where T : Component
        {
            T component = Activator.CreateInstance(typeof(T), new object[] { this }) as T;
            components.Add(component);
            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            return components.FirstOrDefault() as T;
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            List<T> result = new List<T>(components.Count);
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                    result.Add((T)components[i]);
            }
            return result;
        }

        public bool RemoveComponent(Component component)
        {
            return components.Remove(component);
        }

        #endregion Public Methods
    }
}