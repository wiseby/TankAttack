using Microsoft.Xna.Framework;

namespace TankAttack
{
    public interface IGameObject
    {
        bool IsDead { set; }
        bool GotHit { get; set; }
        bool Collided { get; set; }
        Vector2 Position { get; set; }
        Vector2 Speed { get; set; }
        float Rotation { get; set; }
        Rectangle CollisionBox { get; }
        Circle CollisionCircle { get; }
    }
}
