using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankAttack
{
    class GreenPlayer : Player
    {
        bool spaceWasReleased = true;
        public GreenPlayer(
            MainGame game, 
            Vector2 startPosition, 
            Dictionary<string, 
            Texture2D> textures,
            Dictionary<string, SpriteFont> spriteFonts) 
            : base(game, startPosition, textures, spriteFonts)
        {
            this.TankHullTexture = textures["GreenTank/GreenTankHull"];
            this.TankTurretTexture = textures["GreenTank/GreenTurret"];
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            topHud.Location = new Vector2(20, 10);
        }

        public override void Interact(KeyboardState keyState)
        {
            base.Interact(keyState);
            if (keyState.IsKeyDown(Keys.Q))
                { TurretRotate(false); }

            if (keyState.IsKeyDown(Keys.E))
                { TurretRotate(true); }

            if (keyState.IsKeyDown(Keys.D))
                { HullRotate(true); }

            if (keyState.IsKeyDown(Keys.A))
                { HullRotate(false); }

            if (keyState.IsKeyDown(Keys.W))
                { Accelerate(); }

            if (keyState.IsKeyDown(Keys.S))
                { Reverse(); }

            // One shot per press
            if (keyState.IsKeyUp(Keys.Space)) { spaceWasReleased = true; }
            if (keyState.IsKeyDown(Keys.Space) && spaceWasReleased)
            {
                if (FireCooldown == 0) 
                {
                    weaponSystem.Fire(this);
                }
                spaceWasReleased = false;
            }   
        }
    }
}