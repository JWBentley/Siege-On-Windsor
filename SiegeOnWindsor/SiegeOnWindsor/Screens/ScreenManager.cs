using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Screens
{
    public class ScreenManager
    {
        private Screens currentScreen; //Reference to the current screens enum

        private MenuScreen menuScreen; //Instance of the menu screen
        private GameScreen gameScreen; //Instance of the game screen

        public ScreenManager(GraphicsDevice gd, SiegeGame game)
        {
            menuScreen = new MenuScreen(game);
            gameScreen = new GameScreen(game);

            currentScreen = Screens.MAIN_MENU;
        }

        public void InitializeScreens()
        {
            menuScreen.Initialize();
            gameScreen.Initialize();
        }

        public void LoadScreensContent()
        {
            menuScreen.LoadContent();
            gameScreen.LoadContent();
        }

        public void UnloadScreensContent()
        {
            menuScreen.UnloadContent();
            gameScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            this.GetScreen(this.currentScreen).Update(gameTime); //Updates the current screen
        }

        public void Draw(GameTime gameTime)
        {
            this.GetScreen(this.currentScreen).Draw(gameTime); //Updates the current screen
        }

        public void SwitchScreen(Screens screen)
        {
            this.currentScreen = screen;
        }

        private Screen GetScreen(Screens screen)
        {
            switch(screen)
            {
                case Screens.MAIN_MENU:
                    return menuScreen;
                case Screens.GAME:
                    return gameScreen;
                default:
                    return null;
            }
        }
    }
}
