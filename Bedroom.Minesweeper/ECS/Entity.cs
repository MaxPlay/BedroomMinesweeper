using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bedroom.Minesweeper.ECS.Components;

namespace Bedroom.Minesweeper.ECS
{
    public class Entity
    {
        public string Name { get; set; }

        List<Component> components;

        public bool RemoveComponent(Component component)
        {
            return components.Remove(component);
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
    }
}
