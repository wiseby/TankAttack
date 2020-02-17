using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankAttack;

namespace States
{
    public class GameState : State
    {
        private List<Player> players;
        private KeyboardState previousKbState;
        private Texture2D groundDessertTexture;

        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;

        private List<IGameObject> gameComponents = TankAttack.Game1.GameComponents;

        // Collisions control
        private Collision collision;

        private bool gameOver;

        public GameState(
            Game1 game, 
            GraphicsDevice graphicsDevice, 
            ContentManager content,
            Dictionary<string, Texture2D> textures,
            Dictionary<string, SpriteFont> fonts) 
            : base(game, graphicsDevice, content)
        {
            this.textures = textures;
            this.fonts = fonts;
            Initialize();
        }

        private void Initialize()
        {
            players = new List<Player>();

            AddPlayer(new GreenPlayer(
                game, 
                new Vector2(
                    200, 
                    Globals.ScreenHeight / 2), 
                    textures, 
                    fonts));
            AddPlayer(new BrownPlayer(
                game, 
                new Vector2(
                    Globals.ScreenWidth - 200, 
                    Globals.ScreenHeight / 2), 
                    textures, 
                    fonts));

            collision = new Collision();
            groundDessertTexture = textures["Terrain/DessertTile"];
        }

        private void AddPlayer(Player player)
        {
            players.Add(player);
            gameComponents.Add(player);
        }

        

        public override void PostUpdate(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
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

            foreach (var component in gameComponents)
            {
                component.Collided = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           graphicsDevice.Clear(Color.Brown);

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
            spriteBatch.End();
        }
    }
}