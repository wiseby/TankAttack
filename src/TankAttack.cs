using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankAttack
{
    public class TankAttack : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Player> players;
        KeyboardState previousKbState;
        public static List<IGameObject> gameComponents;

        private Texture2D groundDessertTexture;
        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;

        // Collisions control
        private Collision collision;

        private bool gameOver;

        public static DebugWindow debugWindow;

        string[] spriteAssets = new string[]
        {
            "BrownTank/BrownTankHull",
            "BrownTank/BrownTurret",
            "GreenTank/GreenTankHull",
            "GreenTank/GreenTurret",
            "WeaponSystem/BarrelFlame",
            "WeaponSystem/Bullet",
            "WeaponSystem/Projectile",
            "Terrain/DessertTile",
            "UI/DebugConsole"
        };

        string[] spriteFonts = new string[]
        {
            "SpriteFonts/Name",
            "SpriteFonts/Score",
            "SpriteFonts/DebugOutput"
        };

        public TankAttack()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;

            // Run in window mode when debugging!!!
            graphics.IsFullScreen = false;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            Window.Title = "Tank Attack!";
            players = new List<Player>();
            gameComponents = new List<IGameObject>();

            base.Initialize();

            AddPlayer(new GreenPlayer(this, new Vector2(200, Globals.ScreenHeight / 2), textures, fonts));
            AddPlayer(new BrownPlayer(this, new Vector2(Globals.ScreenWidth - 200, Globals.ScreenHeight / 2), textures, fonts));

            debugWindow = new DebugWindow(this, true, fonts, textures);

            // Create a Collision Instance
            collision = new Collision();

            // Create terrain, map etc...
        }

        private void AddPlayer(Player player)
        {
            players.Add(player);
            gameComponents.Add(player);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            // Load background etc etc.
            LoadTextures(spriteAssets);
            LoadUIFonts(spriteFonts);
            groundDessertTexture = textures["Terrain/DessertTile"];
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (debugWindow.Output.Count > 15)
                { debugWindow.Output.Clear(); }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();
            
            KeyboardState keyState = Keyboard.GetState();

            collision.CheckColliders(gameComponents, gameTime);

            // For every player (wich is 2 at the moment) 
            // make movement for each of them.
            foreach(var player in players)
            {
                player.Interact(keyState);
                player.Update(gameTime);
                if (player.IsDead) { gameOver = true; }
            }
            previousKbState = keyState;
            
            base.Update(gameTime);
            if (gameOver) { Exit(); }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Brown);

            spriteBatch.Begin();
            for (int i = 0; i < Globals.ScreenHeight; i += groundDessertTexture.Height)
            {
                for (int j = 0; j < Globals.ScreenWidth; j += groundDessertTexture.Width)
                {
                    spriteBatch.Draw(groundDessertTexture, new Vector2(j, i), Color.White);
                }
            }
            
            foreach (var player in players)
            {
                player.Draw(spriteBatch);
            }

            debugWindow.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoadTextures(string[] spriteAssets)
        {
            foreach (var sprite in spriteAssets)
            {
                textures.Add(sprite, Content.Load<Texture2D>(sprite));
            }
        }

        private void LoadUIFonts(string[] spriteFonts)
        {
            foreach (var spriteFont in spriteFonts)
            {
                fonts.Add(spriteFont, Content.Load<SpriteFont>(spriteFont));
            }
        }
    }
}
