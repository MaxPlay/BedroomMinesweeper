using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.ECS.Components
{
    public abstract class Component
    {
        private Entity entity;

        public Component(Entity entity)
        {
            this.entity = entity;
        }

        public Entity Entity
        {
            get
            {
                return entity;
            }

            set
            {
                if (value == null)
                    return;

                if (entity == value)
                    return;

                entity.RemoveComponent(this);
                entity = value;
                entity.AddComponent(this);
            }
        }
    }
}
