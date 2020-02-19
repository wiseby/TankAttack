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
        private bool allKeysReleased = true;


        public Vector2 MenuPosition = new Vector2(TankAttack.Globals.ScreenWidth / 2, 200);

        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SpriteFont> _fonts;


        public TextInput GreenPlayerInput { get; set; }
        public TextInput BrownPlayerInput { get; set; }
        public Button StartGameButton { get; set; }
        public Button ExitButton { get; set; }
        public List<IMenuItem> MenuComponents { get; set; }

        public StartMenuState(
            MainGame game,
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
                Title = "GreenPlayer Name:",
                InputText = "GreenPlayer"
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
                Title = "BrownPlayer Name:",
                InputText = "BrownPlayer"
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

            if (keyboardState.GetPressedKeyCount() < 1) { allKeysReleased = true; }


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

            switch (menuItemSelected)
            {
                case MenuSelector.NameGreen:
                    if(keyboardState.IsKeyDown(Keys.Back) && GreenPlayerInput.InputText.Length > 0 
                        && allKeysReleased == true)
                    {
                        GreenPlayerInput.InputText = GreenPlayerInput.InputText.Remove(GreenPlayerInput.InputText.Length - 1, 1);
                        allKeysReleased = false;
                    }
                    else if (allKeysReleased == true)
                    {
                        foreach (var key in keyboardState.GetPressedKeys())
                        {
                            if ((int)key > 64 && (int)key <  91)
                            GreenPlayerInput.InputText += key.ToString();
                            allKeysReleased = false;
                        }
                    }
                    
                    break;

                case MenuSelector.NameBrown:
                    if(keyboardState.IsKeyDown(Keys.Back) && BrownPlayerInput.InputText.Length > 0 
                        && allKeysReleased == true)
                    {
                        BrownPlayerInput.InputText = BrownPlayerInput.InputText.Remove(BrownPlayerInput.InputText.Length - 1, 1);
                        allKeysReleased = false;
                    }
                    else if (allKeysReleased == true)
                    {
                        foreach (var key in keyboardState.GetPressedKeys())
                        {
                            if ((int)key > 64 && (int)key <  91)
                            BrownPlayerInput.InputText += key.ToString();
                            allKeysReleased = false;
                        }
                    }
                    break;

                case MenuSelector.StarGame:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.GreenPlayerName = GreenPlayerInput.InputText;
                        game.BrownPlayerName = BrownPlayerInput.InputText;
                        game.ChangeState(
                            new GameState(game, graphicsDevice, content, _textures, _fonts, gameTime));
                    }
                    break;

                case MenuSelector.ExitGame:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.Exit();
                    }
                    break;
                
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