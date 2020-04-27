using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankAttack;
using UserInterface;

namespace States
{

    public enum MenuSelector { NameGreen = 0, NameBrown, StartGame, ExitGame }

    public class StartMenuState : State
    {
        private MenuSelector menuItemSelected;

        private bool keyUpReleased = true;
        private bool keyDownReleased = true;
        private bool allKeysReleased = true;


        public Vector2 MenuPosition = new Vector2(TankAttack.Globals.ScreenWidth / 2, 200);

        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SpriteFont> _fonts;
        public List<IMenuItem> MenuComponents { get; set; }
        private readonly int[] inputChars = { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 };

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
            MenuComponents = new List<IMenuItem>();
            // GreenPlayer Textbox
            MenuComponents.Add(
                new TextInput(
                game,
                _textures["UI/TextBox"],
                _fonts["SpriteFonts/GreenPlayerInput"],
                _fonts["SpriteFonts/GreenPlayerTextBoxTitle"]
            )
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 200),
                Title = "GreenPlayer Name:",
                InputText = "GreenPlayer"
            });

            // BrownPlayer Textbox
            MenuComponents.Add(
                new TextInput(
                game,
                _textures["UI/TextBox"],
                _fonts["SpriteFonts/BrownPlayerInput"],
                _fonts["SpriteFonts/BrownPlayerTextBoxTitle"]
            )
                {
                    Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 400),
                    Title = "BrownPlayer Name:",
                    InputText = "BrownPlayer"
                });

            // StartButton
            MenuComponents.Add(
                new Button(
                game,
                _textures["UI/GreenBtn"],
                _fonts["SpriteFonts/StartBtnText"])
                {
                    Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 600),
                    Title = "Start Game"
                });

            // ExitButton
            MenuComponents.Add(
                new Button(
                game,
                _textures["UI/GreenBtn"],
                _fonts["SpriteFonts/ExitBtnText"])
            {
                Position = new Vector2(TankAttack.Globals.ScreenWidth / 2, 800),
                Title = "Exit Game"
            });

            menuItemSelected = 0;
            MenuComponents[0].Activate();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            graphicsDevice.Clear(Color.BurlyWood);
            foreach (var item in MenuComponents)
            {
                item.Draw(gameTime, spriteBatch);
            }
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
                    MenuComponents[(int)menuItemSelected].Deactivate();
                    menuItemSelected++;
                    MenuComponents[(int)menuItemSelected].Activate();
                }
                keyDownReleased = false;
            }

            if (keyboardState.IsKeyUp(Keys.Up)) { keyUpReleased = true; }
            if (keyboardState.IsKeyDown(Keys.Up) && keyUpReleased == true)
            {
                if (menuItemSelected > MenuSelector.NameGreen)
                {
                    MenuComponents[(int)menuItemSelected].Deactivate();
                    menuItemSelected--;
                    MenuComponents[(int)menuItemSelected].Activate();
                }
                keyUpReleased = false;
            }

            var selectedItem = MenuComponents[(int)menuItemSelected];

            switch (menuItemSelected)
            {
                case MenuSelector.NameGreen:
                    if (keyboardState.IsKeyDown(Keys.Back) && selectedItem.InputText.Length > 0
                        && allKeysReleased == true)
                    {
                        selectedItem.InputText = selectedItem.InputText.Remove(selectedItem.InputText.Length - 1, 1);
                        allKeysReleased = false;
                    }
                    else if (allKeysReleased == true)
                    {
                        foreach (var key in keyboardState.GetPressedKeys())
                        {
                            if ((int)key > 64 && (int)key < 91)
                                selectedItem.InputText += key.ToString();
                            allKeysReleased = false;
                        }
                    }

                    break;

                case MenuSelector.NameBrown:
                    if (keyboardState.IsKeyDown(Keys.Back) && selectedItem.InputText.Length > 0
                        && allKeysReleased == true)
                    {
                        selectedItem.InputText = selectedItem.InputText.Remove(selectedItem.InputText.Length - 1, 1);
                        allKeysReleased = false;
                    }
                    else if (allKeysReleased == true)
                    {
                        foreach (var key in keyboardState.GetPressedKeys())
                        {
                            if ((int)key > 64 && (int)key < 91)
                                selectedItem.InputText += key.ToString();
                            allKeysReleased = false;
                        }
                    }
                    break;

                case MenuSelector.StartGame:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        game.GreenPlayerName = MenuComponents[0].InputText;
                        game.BrownPlayerName = MenuComponents[1].InputText;
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

        private void UnMarkMenuItem()
        {
            MenuComponents[(int)menuItemSelected].Title = MenuComponents[(int)menuItemSelected].Title.Remove(0, 3);
        }
    }
}