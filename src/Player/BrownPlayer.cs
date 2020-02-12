using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankAttack
{
    class BrownPlayer : Player
    {
        public BrownPlayer(
            Game game, 
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
            topHud.Location = new Vector2(Globals.ScreenWidth / 2 + 10, 20);
        }

        public override void Interact(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.NumPad4))
                TurretRotation -= Globals.TurretRotationSpeed;

            if (keyState.IsKeyDown(Keys.NumPad6))
                TurretRotation += Globals.TurretRotationSpeed;

            if (keyState.IsKeyDown(Keys.Right))
                HullRotation += Globals.RotationSpeed;

            if (keyState.IsKeyDown(Keys.Left))
                HullRotation -= Globals.RotationSpeed;

            if (keyState.IsKeyDown(Keys.Up))
            {
                Accelerate();
                Position = Speed;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                Decelerate();
                Position = Speed;
            }

            if (keyState.IsKeyDown(Keys.Enter))
            {
                weaponSystem.Fire(this);
            }   
        }
    }
}