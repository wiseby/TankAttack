using Microsoft.Xna.Framework;

namespace TankAttack
{
    public class Obstacle : DrawableGameComponent, IGameObject
    {
        public Obstacle(Game game) : base(game)
        {
        }

        public bool IsDead { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public Vector2 Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public Vector2 Speed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public float Rotation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Rectangle CollisionBox => throw new System.NotImplementedException();

        public bool GotHit { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Circle CollisionCircle => throw new System.NotImplementedException();

        public bool Collided { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool IsColliding(Rectangle component)
        {
            throw new System.NotImplementedException();
        }
    }
}