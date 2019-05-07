namespace Bedroom.Minesweeper.ECS.Components
{
    public abstract class Component
    {
        #region Private Fields

        private Entity entity;

        #endregion Private Fields

        #region Public Constructors

        public Component(Entity entity)
        {
            this.entity = entity;
        }

        #endregion Public Constructors

        #region Public Properties

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

        #endregion Public Properties
    }
}