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

        public ScreenManager(SiegeGame game)
        {
            menuScreen = new MenuScreen();
            gameScreen = new GameScreen(game);
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

        public IScreen GetScreen(Screen screen)
        {
            switch(screen)
            {
                case Screen.MAIN_MENU:
                    return menuScreen;
                case Screen.GAME:
                    return gameScreen;
                default:
                    return null;
            }
        }
    }
}
