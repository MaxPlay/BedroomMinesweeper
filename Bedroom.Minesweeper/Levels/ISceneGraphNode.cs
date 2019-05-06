using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bedroom.Minesweeper.Levels
{
    public interface ISceneGraphNode
    {
        Vector2 Position { get; set; }

        float Rotation { get; set; }

        Vector2 Scale { get; }

        Vector2 LocalPosition { get; set; }

        float LocalRotation { get; set; }

        Vector2 LocalScale { get; set; }

        ISceneGraphNode Parent { get; set; }

        IReadOnlyList<ISceneGraphNode> Children { get; }

        ISceneGraphNode Root { get; }

        void AddChild(ISceneGraphNode node);

        void RemoveChild(ISceneGraphNode node);

        IEnumerable<ISceneGraphNode> EnumerateChildren();
    }
}
