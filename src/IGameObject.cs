using Microsoft.Xna.Framework;

namespace TankAttack
{
    public interface IGameObject
    {
        bool IsDead { get; set; }
        Vector2 Position { get; set; }
        Vector2 Speed { get; set; }
        float Rotation { get; set; }
        Rectangle CollisionBox { get; }

        bool IsColliding(Rectangle component);
    }
}
