using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankAttack
{
    class BrownPlayer : Player
    {
        bool enterWasReleased = true;
        public BrownPlayer(
            MainGame game, 
            Vector2 startPosition, 
            Dictionary<string, Texture2D> textures, 
            Dictionary<string, SpriteFont> spriteFonts ) 
            : base(game, startPosition, textures, spriteFonts)
        {
            this.TankHullTexture = textures["BrownTank/BrownTankHull"];
            this.TankTurretTexture = textures["BrownTank/BrownTurret"];
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            topHud.Location = new Vector2(Globals.ScreenWidth / 2 + 10, 20);
        }

        public override void Interact(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.NumPad4))
                { TurretRotate(false); }

            if (keyState.IsKeyDown(Keys.NumPad6))
                { TurretRotate(true); }

            if (keyState.IsKeyDown(Keys.Right))
                { HullRotate(true); }

            if (keyState.IsKeyDown(Keys.Left))
                { HullRotate(false); }

            if (keyState.IsKeyDown(Keys.Up))
            {
                Accelerate();
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                Reverse();
            }

            // One shot per press
            if (keyState.IsKeyUp(Keys.Enter)) { enterWasReleased = true; }
            if (keyState.IsKeyDown(Keys.Enter) && enterWasReleased)
            {
                if (FireCooldown == 0)
                {
                    weaponSystem.Fire(this);
                }
                enterWasReleased = false;
            }   
        }
    }
}