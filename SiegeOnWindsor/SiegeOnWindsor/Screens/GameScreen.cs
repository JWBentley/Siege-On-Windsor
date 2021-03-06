﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnWindsor.Data.Enemies;
using SiegeOnWindsor.Graphics;
using Microsoft.Xna.Framework.Input;
using SiegeOnWindsor.Graphics.UI;
using SiegeOnWindsor.Data.Defences;

namespace SiegeOnWindsor.Screens
{
    public class GameScreen : Screen
    {
        World world; //Reference to the world

        private DefenceSelectPanel defenceSelectPanel;
        private UILabel moneyLabel;

        private UILabel waveLabel;
        private UIButton waveStartButton;

        private UIPanel pauseMenuPanel;
        private UIButton saveGameButton;

        private UIPanel endGamePanel;
        private UILabel wavesLastedLabel;

        public GameScreen(SiegeGame game) : base(game)
        {
            this.game = game; //Sets the game
            this.world = new World(this.game); //Creates a new world
        }


        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            //Wave info
            this.waveLabel = new UILabel(this.world.WaveController.GetWaveNumberText, Graphics.Graphics.arial32, Color.White, new Rectangle(10,10, (int)Graphics.Graphics.arial32.Object.MeasureString("Wave - xxxx").Length(), (int)Graphics.Graphics.arial32.Object.MeasureString("Wave - xxxx").Y));
            this.uiController.Components.Add(this.waveLabel);

            //Wave start button
            this.waveStartButton = new UIButton(Graphics.Graphics.blankButtonUI, Graphics.Graphics.arial32, "Start wave", new Vector2(10, (int)Graphics.Graphics.arial32.Object.MeasureString("Wave - xxxx").Y + 10))
            {
                Tint = Color.Green
            };
            this.waveStartButton.Click += (o, i) => { if (this.world.StartWave()) this.waveStartButton.Toggle(); };

            this.uiController.Components.Add(this.waveStartButton);

            //Adds defence panel to the screen
            this.defenceSelectPanel = new DefenceSelectPanel(this.world, 
                new Rectangle(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))))) / 2) + ((this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1)))))) + 10, 
                this.game.Graphics.PreferredBackBufferHeight / 2 - 390 / 2, 202, 390));

            this.uiController.Components.Add(this.defenceSelectPanel);

            //Adds money label to the screen
            this.moneyLabel = 
            new UILabel(
                this.world.getMoneyText, 
                Graphics.Graphics.arial32, Color.White, 
                new Rectangle(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))))) / 2) + ((this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1)))))) + 10, (int)(this.game.Graphics.PreferredBackBufferHeight * 0.15), 0, 0));

            this.uiController.Components.Add(this.moneyLabel);

            //Adds pause 
            this.pauseMenuPanel = new UIPanel(Graphics.Graphics.pauseMenuPanelUI, new Rectangle(this.game.Graphics.PreferredBackBufferWidth / 2 - 360 / 2, 0, 360, 720))
            {
                isActive = false,
                isVisible = false
            };

            //Pause menu title text
            this.pauseMenuPanel.AddComponent(new UILabel("Game paused", Graphics.Graphics.arial32, Color.Red), new Point((this.pauseMenuPanel.Bounds.Size.X / 2) - ((int)Graphics.Graphics.arial32.Object.MeasureString("Game paused").X / 2), 30));

            //Return to main menu button
            UIButton quitGame = new UIButton(Graphics.Graphics.blankButtonUI,
                                                Graphics.Graphics.arial32,
                                                "Quit to menu");
            quitGame.Click += (o, i) => { this.QuitCurrentGame(); };

            this.pauseMenuPanel.AddComponent(quitGame, new Point((this.pauseMenuPanel.Bounds.Size.X / 2) - ((int)quitGame.Bounds.Size.X / 2), 100));

            //Save progress button
            this.saveGameButton = new UIButton(Graphics.Graphics.blankButtonUI,
                                                Graphics.Graphics.arial32,
                                                "Save Game")
            {
                Tint = Color.Green
            };

            //Adds click function to the save game button
            this.saveGameButton.Click += (o, i) => {
                System.IO.File.WriteAllLines(SiegeGame.SaveFile, World.WorldToFile(this.world));
                Console.WriteLine(SiegeGame.SaveFile);
            };

            //Adds the save game button to the pause screen
            this.pauseMenuPanel.AddComponent(this.saveGameButton, new Point((this.pauseMenuPanel.Bounds.Size.X / 2) - ((int)this.saveGameButton.Bounds.Size.X / 2), 180));

            //Adds pause menu to the ui controller
            this.uiController.Components.Add(this.pauseMenuPanel);

            //End game panel
            this.endGamePanel = new UIPanel(Graphics.Graphics.endGamePanelUI, new Rectangle(this.game.Graphics.PreferredBackBufferWidth / 2 - 400 / 2, this.game.Graphics.PreferredBackBufferHeight / 2 - 236 / 2, 400, 236))
            {
                isActive = false,
                isVisible = false
            };

            this.wavesLastedLabel = new UILabel("You lasted * wave(s)", Graphics.Graphics.arial24, Color.White); //Creates waves lasted label
            this.endGamePanel.AddComponent(this.wavesLastedLabel, new Point(30, 90)); //Adds to the end game popup

            //Creates return to menu button
            UIButton returnToMenuButton = new UIButton(Graphics.Graphics.blankButtonUI, Graphics.Graphics.arial32, "Return to menu")
            {
                Tint = Color.Tomato
            };
            returnToMenuButton.Click += (o, i) => { this.QuitCurrentGame(); };

            //Adds the button to the end panel
            this.endGamePanel.AddComponent(returnToMenuButton, new Point(this.endGamePanel.Bounds.Width / 2 - returnToMenuButton.Bounds.Width / 2, 150));

            //Adds to controller
            this.uiController.Components.Add(this.endGamePanel);
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.world.IsRunning) //If the game is over
            {
                //Updates waves lasted
                this.wavesLastedLabel.Text = String.Format("You lasted {0} wave(s)", this.world.WaveController.WaveNumber - 1); 

                if (!this.endGamePanel.isActive)
                    this.endGamePanel.Toggle(); //Displays end game panel

                if (this.defenceSelectPanel.isActive)
                    this.defenceSelectPanel.isActive = false; //Defence select panel deactivated
            }
            else if (SiegeGame.prevKeyboard.IsKeyDown(Keys.Escape) && SiegeGame.currentKeyboard.IsKeyUp(Keys.Escape)) //If Escape key pressed
            {
                this.pauseMenuPanel.Toggle(); //Pause menu toggled
                this.world.isPaused = !this.world.isPaused; //Toggles world pause
            }
            else if(!this.world.WaveController.IsWaveActive) //Checks if wave is over
            {
                if (!this.waveStartButton.isActive)
                    this.waveStartButton.Toggle(); //Toggles the wave start button

                if (this.world.Enemies.Count == 0 && !this.saveGameButton.isActive) //When wave is over, show save button
                    this.saveGameButton.Toggle();
            }
            else if (this.world.WaveController.IsWaveActive || this.world.Enemies.Count > 0)
            {
                if (this.saveGameButton.isActive) //When wave is running, hide save button
                    this.saveGameButton.Toggle();
            }
            
            this.uiController.Update(gameTime);

            this.world.Update(gameTime); //Updates world
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //Debugging pathfinding
            //Stack<Vector2> test = this.world.aStar.Run(new Vector2(16, 16), new Vector2((this.world.Grid).GetLength(0) / 2, (this.world.Grid).GetLength(1) / 2));
            
            /*
             * DRAW BACKGROUND
             */
            this.spriteBatch.Draw(texture: Graphics.Graphics.gameBackground.Object, destinationRectangle: new Rectangle(0, 0, this.game.Graphics.PreferredBackBufferWidth, this.game.Graphics.PreferredBackBufferHeight), color: Color.White); //Draws the background fullscreen

            /*
             * DRAW TILES
             */
            int height = Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))));

            for (int x = 0; x < (this.world.Grid).GetLength(0); x++)
            {
                for (int y = 0; y < (this.world.Grid).GetLength(1); y++)
                {
                    if (this.world.GetTileAt(x, y).GetGraphic() != null && this.world.GetTileAt(x, y).GetGraphic().Object != null)
                    {
                        //Draws the tile
                        this.spriteBatch.Draw(this.world.GetTileAt(x, y).GetGraphic().Object, new Rectangle(
                            Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                            Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                            height,
                            height),
                            Color.White);

                        //Draws the defence on top of the tile
                        if (this.world.GetTileAt(x, y).Defence != null)
                        {
                            this.spriteBatch.Draw(this.world.GetTileAt(x, y).Defence.GetGraphic().Object, new Rectangle(
                                Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                                Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                                height,
                                height),
                                Color.White);

                           // Console.WriteLine("Drawing " + this.world.GetTileAt(x, y).Defence.ToString());
                        }


                        //this.spriteBatch.DrawString(this.game.Content.Load<SpriteFont>("Fonts/default32"), this.world.RiskMap[x, y].ToString(), new Vector2(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)), Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height))), this.world.RiskMap[x, y] > 1 ? Color.Purple : Color.White);

                        /*
                        if (this.world.SelectedTile == new Vector2(x, y))
                            this.spriteBatch.DrawString(
                                Graphics.Graphics.arial32.Object, 
                                "X", 
                                new Vector2(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)), 
                                Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height))), 
                                Color.Blue);*/

                        //DEBUGGING PATHFINDING
                        /*
                        Color colorTest;
                        if (test.Contains(new Vector2(x, y)))
                            colorTest = Color.Green;
                        else
                            colorTest = Color.White;
                        this.spriteBatch.DrawString(this.game.Content.Load<SpriteFont>("Fonts/default32"), this.world.RiskMap[x, y].ToString(), new Vector2(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)), Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height))), colorTest);
                        */
                    }

                    /*
                     * DRAW DEFENCE 
                     */


                    /*if (this.world.GetTileAt(x, y).enemies != null)
                        foreach (Enemy enemy in this.world.GetTileAt(x, y).enemies)
                        {
                            this.spriteBatch.Draw(enemy.GetGraphic().Sprite, new Rectangle(
                                    Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                                    Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                                    height,
                                    height),
                                    Color.White);
                        }*/
                }
            }

            /*
             * DRAW ENEMIES 
             */
            foreach (Enemy enemy in this.world.Enemies)
            {
                this.spriteBatch.Draw(enemy.GetGraphic().Object, new Rectangle(
                            Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (enemy.GetActualLocation().X * height)),
                            Convert.ToInt16((this.game.Graphics.PreferredBackBufferHeight * 0.05) + (enemy.GetActualLocation().Y * height)),
                            height,
                            height),
                            Color.White);
            }

            /*
             * DRAW UI
             */
            /*
           this.world.spawnEnemy.Draw(gameTime, this.spriteBatch);
           this.world.buildWall.Draw(gameTime, this.spriteBatch);
           this.world.deployGuard.Draw(gameTime, this.spriteBatch);
           */

            this.uiController.Draw(gameTime);

            //Draw selected defence
            if (this.defenceSelectPanel.SelectedDefence != null)
                this.spriteBatch.Draw(this.defenceSelectPanel.SelectedDefence.GetGraphic().Object,
                    new Rectangle(Mouse.GetState().Position.X - (height / 2), Mouse.GetState().Position.Y - (height / 2), height, height),
                    Color.White);

            this.spriteBatch.End();
        }

        public override void UnloadContent()
        {

        }

        /// <summary>
        /// Loads world object into the game screen
        /// </summary>
        /// <param name="world"></param>
        public void LoadWorld(World world)
        {
            this.world = world;

            if (this.pauseMenuPanel.isActive)
                this.pauseMenuPanel.Toggle(); //Unpauses game

            if (this.endGamePanel.isActive)
                this.endGamePanel.Toggle(); //Removes game over panel

            this.moneyLabel.textGetter = this.world.getMoneyText; //Reallocates money label delegate

            this.defenceSelectPanel.UpdateWorld(this.world); //Updates defenceselectpanel and its world reference

            this.waveLabel.textGetter = this.world.WaveController.GetWaveNumberText; //Updates wave number label with new wave controller
        }

        /// <summary>
        /// Performs cleanup to quit the current game and create a new one
        /// </summary>
        private void QuitCurrentGame()
        {
            this.game.ScreenManager.SwitchScreen(Screens.MAIN_MENU);

            this.world = new World(this.game); //Creates new world

            if (this.pauseMenuPanel.isActive)
                this.pauseMenuPanel.Toggle(); //Unpauses game

            if (this.endGamePanel.isActive)
                this.endGamePanel.Toggle(); //Removes game over panel

            this.moneyLabel.textGetter = this.world.getMoneyText; //Reallocates money label delegate

            this.defenceSelectPanel.UpdateWorld(this.world); //Updates defenceselectpanel and its world reference

            this.waveLabel.textGetter = this.world.WaveController.GetWaveNumberText; //Updates wave number label with new wave controller
        }
    }
}
