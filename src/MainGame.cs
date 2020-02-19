using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using States;

namespace TankAttack
{
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // States
        private State currentState;
        private State nextState;
        
        // Assets
        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;
        public static List<IGameObject> GameComponents;

        // Player Data
        public string GreenPlayerName { get; set; }
        public string BrownPlayerName { get; set; }
        public int GreenPlayerScore { get; set; }
        public int BrownPlayerScore { get; set; }
        public int SecondsPlayed { get; set; }
        
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
            "UI/DebugConsole",
            "UI/TextBox",
            "UI/GreenBtn"
        };

        string[] spriteFonts = new string[]
        {
            "SpriteFonts/Name",
            "SpriteFonts/Score",
            "SpriteFonts/DebugOutput",
            "SpriteFonts/StartBtnText",
            "SpriteFonts/PlayAgainBtnText",
            "SpriteFonts/ExitBtnText",
            "SpriteFonts/ControlsBtnText",
            "SpriteFonts/GreenPlayerInput",
            "SpriteFonts/BrownPlayerInput",
            "SpriteFonts/GreenPlayerTextBoxTitle",
            "SpriteFonts/BrownPlayerTextBoxTitle",
            "SpriteFonts/WinnerText"
        };

        // Debugging
        public static DebugWindow debugWindow;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;

            // Run in window mode when debugging!!!
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            Window.Title = "Tank Attack!";
            
            GameComponents = new List<IGameObject>();

            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();

            

            base.Initialize();
        }

        

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            // Load background etc etc.
            LoadTextures(spriteAssets);
            LoadUIFonts(spriteFonts);
            debugWindow = new DebugWindow(this, true, fonts, textures);
            
            currentState = new StartMenuState(
                this, 
                graphics.GraphicsDevice,
                Content, 
                textures, 
                fonts);
            nextState = null;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            if (nextState != null) 
            { 
                currentState = nextState; 
                nextState = null;
            }

            currentState.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime, spriteBatch);
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

        public void ChangeState(State state)
        {
            if (nextState == null) { nextState = state; }
        }
    }
}
