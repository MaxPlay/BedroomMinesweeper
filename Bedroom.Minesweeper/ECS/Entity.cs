using Bedroom.Minesweeper.ECS.Components;
using Bedroom.Minesweeper.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bedroom.Minesweeper.ECS
{
    public class Entity
    {
        #region Private Fields

        private List<Entity> children;
        private List<Component> components;
        private List<IDrawableComponent> drawableComponents;
        private List<IUpdatableComponent> updatableComponents;

        #endregion Private Fields

        #region Public Constructors

        public Entity(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            components = new List<Component>();
            children = new List<Entity>();
            drawableComponents = new List<IDrawableComponent>();
            updatableComponents = new List<IUpdatableComponent>();
        }

        public Entity(string name, Vector2 position, float rotation, Vector2 scale) : this(name)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }

        public Entity() : this("new Entity")
        {
        }

        public Entity(string name, Entity parent) : this(name)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public Entity(string name, Vector2 position, float rotation, Vector2 scale, Entity parent) : this(name, parent)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            LocalScale = scale;
        }

        #endregion Public Constructors

        #region Public Properties

        public IReadOnlyList<Entity> Children { get { return children; } }
        public Vector2 LocalPosition { get; set; }
        public float LocalRotation { get; set; }
        public Vector2 LocalScale { get; set; }
        public string Name { get; set; }

        public Entity Parent { get; set; }

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

        public void AddChild(Entity node)
        {
            if (node == Parent)
                return;

            if (!children.Contains(node))
                children.Add(node);

            if (node.Parent != null)
                node.Parent.RemoveChild(node);
            node.Parent = this;
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            if (component is IDrawableComponent)
                drawableComponents.Add(component as IDrawableComponent);
            if (component is IUpdatableComponent)
                updatableComponents.Add(component as IUpdatableComponent);
        }

        public T AddComponent<T>() where T : Component
        {
            T component = Activator.CreateInstance(typeof(T), new object[] { this }) as T;
            components.Add(component);
            return component;
        }

        public void Draw(GameTime deltaTime) => drawableComponents.ForEach(c => c.Draw(deltaTime));

        public IEnumerable<Entity> EnumerateChildren() => children;

        public T GetComponent<T>() where T : Component => components.FirstOrDefault() as T;

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

        public void RemoveChild(Entity node)
        {
            children.Remove(node);
            node.Parent = null;
        }

        public bool RemoveComponent(Component component)
        {
            if (component is IDrawableComponent)
                drawableComponents.Remove(component as IDrawableComponent);
            if (component is IUpdatableComponent)
                updatableComponents.Remove(component as IUpdatableComponent);
            return components.Remove(component);
        }

        public void Update(GameTime deltaTime) => updatableComponents.ForEach(c => c.Update(deltaTime));

        #endregion Public Methods
    }
}