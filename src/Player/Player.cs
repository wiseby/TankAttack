using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TankAttack
{
    class Player : DrawableGameComponent, IGameObject
    {
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public float Radius { get; set; }
        public float HullRotation { get; set; }
        public float TurretRotation { get; set; }
        public float Rotation { get; set; }
        public Vector2 BarrelTip { get; set; }

        // Collision
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, TankHullTexture.Width, TankHullTexture.Height);
            }
        }

        // Time
        public float CurrentTime { get; set; }
        public float PreviousTime { get; set; }

        protected WeaponSystem weaponSystem;

        public bool IsFiring { get; set; }
        public int FireCooldown { get; set; }

        public Dictionary<string, Texture2D> Textures { get; set; }
        public Texture2D TankTurretTexture { get; protected set; }
        public Texture2D TankHullTexture { get; protected set; }

        // Player topHUD
        protected TopHUD topHud;

        /* #region  constructor */

        public Player(
            Game game, 
            Vector2 startPosition, 
            Dictionary<string, Texture2D> textures,
            Dictionary<string, SpriteFont> spriteFonts) 
            : base(game)
        {
            Position = startPosition;
            Speed = Position;
            Rotation = 90;
            IsFiring = false;
            FireCooldown = 0;
            PreviousTime = 0.0f;

            topHud = new TopHUD(game, spriteFonts);
            topHud.NameText = this.GetType().Name;

            Textures = textures;

            weaponSystem = new WeaponSystem(game, this);
        }
        /* #endregion */

        public virtual void Interact(KeyboardState keyState)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                TankHullTexture,
                Position,
                null,
                Color.White,
                HullRotation + MathHelper.PiOver2,
                new Vector2(
                    TankHullTexture.Width / 2,
                    TankHullTexture.Height / 2),
                Globals.TankScale,
                SpriteEffects.None,
                0f);

            spriteBatch.Draw(
                TankTurretTexture,
                Position,
                null,
                Color.White,
                TurretRotation + MathHelper.PiOver2,
                new Vector2(
                    TankTurretTexture.Width / 2,
                    TankTurretTexture.Height / 2),
                Globals.TankScale,
                SpriteEffects.None,
                0f);

            weaponSystem.Draw(spriteBatch);
            topHud.Draw(spriteBatch);
        }


        public override void Update(GameTime gameTime)
        {
            // Moving outside map.
            if (Position.X < 0)
                Position = new Vector2(0, Position.Y);
            if (Position.X > Globals.ScreenWidth)
                Position = new Vector2(Globals.ScreenWidth, Position.Y);
            if (Position.Y < 0)
                Position = new Vector2(Position.X, 0);
            if (Position.Y > Globals.ScreenHeight)
                Position = new Vector2(Position.X, Globals.ScreenHeight);

            // TODO Shoot cooldown count.
            CurrentTime = gameTime.TotalGameTime.Seconds;
            if (CurrentTime - PreviousTime >= Globals.FireCooldownLimit)
            {
                IsFiring = false;
                FireCooldown = 0;
                PreviousTime = CurrentTime;
            }

            BarrelTip = Position + new Vector2((float)Math.Cos(HullRotation),
                (float)Math.Sin(HullRotation));

            weaponSystem.Update();

            TankAttack.debugWindow.Output.Add(
                $"{this.GetType().Name} Projectiles: {weaponSystem.GetTotalNumberOfProjectiles()}");
        }

        public void Accelerate()
        {
            Speed += new Vector2((float)Math.Cos(HullRotation),
                (float)Math.Sin(HullRotation)) * Globals.TankSpeed;
        }

        public void Decelerate()
        {
            Speed -= new Vector2((float)Math.Cos(HullRotation),
                (float)Math.Sin(HullRotation)) * Globals.TankSpeed;
        }

        public bool IsColliding(Rectangle componentRect)
        {
            if (componentRect.Intersects(this.CollisionBox))
            {
                return true;
            }
            return false;
        }
    }
}