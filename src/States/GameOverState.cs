using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankAttack;
using WebApiClient;

namespace States
{
    
    public class GameOverState : State
    {
        private SpriteFont winnerFont;
        private SpriteFont reportFont;
        private string winnerText;
        private string reportText;

        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;


        public GameOverState(
            MainGame game, 
            GraphicsDevice graphicsDevice, 
            ContentManager content,
            Dictionary<string, Texture2D> textures,
            Dictionary<string, SpriteFont> fonts
            ) 
            : base(game, graphicsDevice, content)
        {
            this.textures = textures;
            this.fonts = fonts;
            winnerFont = this.fonts["SpriteFonts/WinnerText"];
            reportFont = this.fonts["SpriteFonts/WinnerText"];
            reportText = "";
            Initialize();
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            graphicsDevice.Clear(Color.DarkGray);
            spriteBatch.DrawString(
                winnerFont,
                winnerText,
                new Vector2(400, Globals.ScreenHeight / 2),
                Color.Black
            );
            spriteBatch.DrawString(
                reportFont,
                reportText,
                new Vector2(600, 800),
                Color.Black
            );
            spriteBatch.End();
        }

        private void Initialize()
        {
            var score = new Score() 
            {
                GreenPlayerName = game.GreenPlayerName,
                GreenPlayerHealth = game.GreenPlayerScore,
                BrownPlayerName = game.BrownPlayerName,
                BrownPlayerHealth = game.BrownPlayerScore,
                SecondsPlayed = game.SecondsPlayed
            };

            var apiCaller = new ApiClient();
            apiCaller.PostScore(score);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (game.GreenPlayerScore > game.BrownPlayerScore)
            {
                winnerText = $"{game.GreenPlayerName} is the winner!";
            }

            else 
            {
                winnerText = $"{game.BrownPlayerName} is the winner!";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) 
            {  
                var nextState = new StartMenuState(
                    game, graphicsDevice, content, textures, fonts);
                game.ChangeState(nextState);
            }
        }
    }
}