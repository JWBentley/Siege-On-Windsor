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
        MenuScreen menuScreen;
        GameScreen gameScreen;

        public ScreenManager(GraphicsDevice gd, SiegeGame game)
        {
            menuScreen = new MenuScreen(gd, game);
            gameScreen = new GameScreen(gd, game);
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

        public Screen GetScreen(Screens screen)
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
