using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TankAttack
{
    class Projectile : DrawableGameComponent, IGameObject
    {
        private bool gotHit = false;
        public bool GotHit {
            set { gotHit = value; }
            get { return gotHit; }
        }
        public bool Collided { get; set; }

        public bool IsDead { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        // Texture Properties
        public Texture2D ProjectileTexture { get; set; }

        // Collision
        public Rectangle CollisionBox 
        { 
            get 
            {
                return new Rectangle((int)Position.X, (int)Position.Y, ProjectileTexture.Width, ProjectileTexture.Height);        
            } 
        } 

        public Circle CollisionCircle
        {
            get 
            {
                return new Circle((int)Position.X, (int)Position.Y, ProjectileTexture.Height / 2);
            }
        }

        public Projectile(
            Game game, Vector2 startPosition, float startRotation, Texture2D projectileTexture) 
                : base(game)
        {
            Position = startPosition;
            Speed = Position;
            Rotation = startRotation;
            ProjectileTexture = projectileTexture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws a projectile.
            spriteBatch.Draw(
                ProjectileTexture, 
                Position, 
                null, 
                Color.White,
                Rotation + MathHelper.PiOver2,
                new Vector2(ProjectileTexture.Width / 2, ProjectileTexture.Height / 2),
                Globals.TankScale, 
                SpriteEffects.None, 
                0f
                );
        }

        internal void Explode()
        {
            // Write out explosion sprite

        }

        public void Update()
        {   
            Position = Speed;
            Accelerate();
        }

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(Rotation),
                (float)Math.Sin(Rotation)) * Globals.ProjectileSpeed;
        }    
    }
}
