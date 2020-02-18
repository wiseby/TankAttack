using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using States;

namespace TankAttack
{
    class Player : DrawableGameComponent, IGameObject, IMovableComponent
    {
        
        private MainGame _game;


        public string PlayerName { get; set; }
        public bool IsReversing { get; set; }
        public bool IsAccelerating { get; set; }
        public bool CanRotate { get; set; }
        public bool CanAccelerate { get; set; }
        public bool CanReverse { get; set; }
        public bool GotHit { get; set; }
        public bool Collided { get; set; }
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public float Radius { get; set; }
        public float HullRotation { get; set; }
        public float TurretRotation { get; set; }
        public float Rotation { get; set; }
        public Vector2 BarrelTip { get; set; }
        public int Health { get; set; }


        // Collision
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, TankHullTexture.Width, TankHullTexture.Height);
            }
        }

        public Circle CollisionCircle
        {
            get
            {
                return new Circle((int)Position.X, (int)Position.Y, TankHullTexture.Height / 2 - 5);
            }
        }

        // Time
        public float CurrentTime { get; set; }
        public float PreviousTime { get; set; }


        public bool IsFiring { get; set; }
        public int FireCooldown { get; set; }

        public Dictionary<string, Texture2D> Textures { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public Texture2D TankTurretTexture { get; protected set; }
        public Texture2D TankHullTexture { get; protected set; }

        // Player topHUD
        protected TopHUD topHud;
        protected WeaponSystem weaponSystem;

        /* #region  constructor */

        public Player(
            MainGame game, 
            Vector2 startPosition, 
            Dictionary<string, Texture2D> textures,
            Dictionary<string, SpriteFont> spriteFonts) 
            : base(game)
        {
            _game = game;
            Position = startPosition;
            Speed = Position;

            Fonts = spriteFonts;
            Textures = textures;

            weaponSystem = new WeaponSystem(game, this);
            //Initialize();
        }
        /* #endregion */

        public override void Initialize()
        {
            topHud = new TopHUD(_game, Fonts);
            CanReverse = true;
            CanAccelerate = true;
            Rotation = 90;
            IsFiring = false;
            FireCooldown = 0;
            PreviousTime = 0.0f;
            Health = 100;
            base.Initialize();
        }

        public virtual void Interact(KeyboardState keyState)
        {
            // IsReversing = false;
            // IsAccelerating = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            weaponSystem.Draw(spriteBatch);
            
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

            Vector2 BarrelOffset = new Vector2((float)Math.Cos(TurretRotation), (float)Math.Sin(TurretRotation)) * 32;
            BarrelTip = Position + BarrelOffset;

            weaponSystem.Update();

            // TankAttack.debugWindow.Output.Add(
            //     $"{this.GetType().Name} Projectiles: {weaponSystem.GetTotalNumberOfProjectiles()}");

            if (Collided == true)
            { 
                if (IsAccelerating)
                {
                    CanAccelerate = false; 
                    MainGame.debugWindow.Output.Add($"{this.GetType().Name}Cant Accelerate!");
                }

                if (IsReversing) 
                {
                    CanReverse = false; 
                    MainGame.debugWindow.Output.Add($"{this.GetType().Name}Cant Reverse!");
                }
            }

            if (GotHit) { Health -= 10; }

            if (Health <= 0) { IsDead = true; }
            
            GotHit = false;

            topHud.NameText = $"{PlayerName} | Health: {Health}";            
        }

        public void Accelerate()
        {
            if (CanAccelerate)
            {
                Speed += new Vector2((float)Math.Cos(HullRotation),
                    (float)Math.Sin(HullRotation)) * Globals.TankSpeed;
                Position = Speed;
                IsAccelerating = true;
            }
        }

        public void Reverse()
        {
            if(CanReverse)
            {
                Speed -= new Vector2((float)Math.Cos(HullRotation),
                    (float)Math.Sin(HullRotation)) * Globals.TankSpeed;
                Position = Speed;
                IsReversing = true;
            }
        }

        public bool IsColliding(Rectangle componentRect)
        {
            if (componentRect.Intersects(this.CollisionBox))
            {
                return true;
            }
            return false;
        }

        public void HullRotate(bool clockWise)
        {
            if (clockWise) { HullRotation += Globals.RotationSpeed; }
            else { HullRotation -= Globals.RotationSpeed; }
        }

        public void TurretRotate(bool clockWise)
        { 
            if (clockWise) { TurretRotation += Globals.TurretRotationSpeed; }
            else { TurretRotation -= Globals.TurretRotationSpeed; }
        }
    }
}