using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankAttack;
using UserInterface;

namespace States
{

    public enum MenuSelector { NameGreen = 0, NameBrown, StarGame, ExitGame }

    public class StartMenuState : State
    {
        private MenuSelector menuItemSelected;

        private bool keyUpReleased = true;
        private bool keyDownReleased = true;


        public Vector2 MenuPosition = new Vector2(TankAttack.Globals.ScreenWidth / 2, 200);

        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SpriteFont> _fonts;


        public TextInput GreenPlayerInput { get; set; }
        public TextInput BrownPlayerInput { get; set; }
        public Button StartGameButton { get; set; }
        public Button ExitButton { get; set; }
        public List<IMenuItem> MenuComponents { get; set; }

        public StartMenuState(
            Game1 game,
            GraphicsDevice graphicsDevice,
            ContentManager content,
            Dictionary<string, Texture2D> textures,
            Dictionary<string, SpriteFont> fonts
            )
            : base(game, graphicsDevice, content)
        {
            _textures = textures;
            _fonts = fonts;
            Initialize();
        }

        public void Initialize()
        {
            // GreenPlayer Textbox
            GreenPlayerInput = new TextInput(
                game,
                _textures["UI/TextBox"],
                _fonts["SpriteFonts/GreenPlayerInput"],
                _fonts["SpriteFonts/GreenPlayerTextBoxTitle"]
            )
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 200),
                Title = "GreenPlayer Name:"
            };

            // BrownPlayer Textbox
            BrownPlayerInput = new TextInput(
                game,
                _textures["UI/TextBox"],
                _fonts["SpriteFonts/BrownPlayerInput"],
                _fonts["SpriteFonts/BrownPlayerTextBoxTitle"]
            )
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 400),
                Title = "BrownPlayer Name:"
            };
            // StartButton
            StartGameButton = new Button(
                game,
                _textures["UI/GreenBtn"],
                _fonts["SpriteFonts/StartBtnText"])
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 600),
                Title = "Start Game"
            };

            // ExitButton
            ExitButton = new Button(
                game,
                _textures["UI/GreenBtn"],
                _fonts["SpriteFonts/ExitBtnText"])
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 800),
                Title = "Exit Game"
            };

            MenuComponents = new List<IMenuItem>() {
                GreenPlayerInput,
                BrownPlayerInput,
                StartGameButton,
                ExitButton
            };

            menuItemSelected = 0;
            MarkMenuItem();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            graphicsDevice.Clear(Color.BurlyWood);
            GreenPlayerInput.Draw(gameTime, spriteBatch);
            BrownPlayerInput.Draw(gameTime, spriteBatch);
            StartGameButton.Draw(gameTime, spriteBatch);
            ExitButton.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                { game.Exit(); }

            if (keyboardState.IsKeyUp(Keys.Down)) { keyDownReleased = true; }
            if (keyboardState.IsKeyDown(Keys.Down) && keyDownReleased == true)
            {
                if (menuItemSelected < MenuSelector.ExitGame)
                {
                    UnMarkMenuItem();
                    menuItemSelected++;
                    MarkMenuItem();
                }
                keyDownReleased = false;
            }

            if (keyboardState.IsKeyUp(Keys.Up)) { keyUpReleased = true; }
            if (keyboardState.IsKeyDown(Keys.Up) && keyUpReleased == true)
            {
                if (menuItemSelected > MenuSelector.NameGreen)
                {
                    UnMarkMenuItem();
                    menuItemSelected--;
                    MarkMenuItem();
                }
                keyUpReleased = false;
            }

            if (keyboardState.IsKeyDown(Keys.Enter))
            {

                switch (menuItemSelected)
                {
                    case MenuSelector.NameGreen:
                        break;

                    case MenuSelector.NameBrown:
                        break;

                    case MenuSelector.StarGame:
                        game.ChangeState(new GameState(game, graphicsDevice, content, _textures, _fonts));
                        break;

                    case MenuSelector.ExitGame:
                        game.Exit();
                        break;
                }
            }
            
        }

        private void MarkMenuItem()
        {
            MenuComponents[(int)menuItemSelected].Title = MenuComponents[(int)menuItemSelected].Title.Insert(0, "-- ");
        }

        private void UnMarkMenuItem()
        {
            MenuComponents[(int)menuItemSelected].Title = MenuComponents[(int)menuItemSelected].Title.Remove(0, 3);
        }
    }
}