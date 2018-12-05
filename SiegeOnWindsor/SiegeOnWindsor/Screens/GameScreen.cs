using System;
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
            //Adds defence panel to the screen
            this.uiController.Components.Add(new DefenceSelectPanel(this.world, 
                new Rectangle(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))))) / 2) + ((this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1)))))) + 10, 
                this.game.Graphics.PreferredBackBufferHeight / 2 - 390 / 2, 202, 390), 
                new List<Defence>() { new StoneWallDef(), new GuardDef(), new DummyDef(Graphics.Graphics.archerDef), new DummyDef(Graphics.Graphics.catapultDef) }));

            this.uiController.Components.Add(new UILabel(
                this.world.getMoneyText, 
                Graphics.Graphics.arial32, Color.White, 
                new Rectangle(Convert.ToInt16(((this.game.Graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))))) / 2) + ((this.world.Grid).GetLength(0) * Convert.ToInt16(Math.Floor((double)(this.game.Graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1)))))) + 10, (int)(this.game.Graphics.PreferredBackBufferHeight * 0.15), 0, 0)));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (UIComponent component in this.uiController.Components)
            {
                component.Update(gameTime);
            }

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

            
            foreach (UIComponent component in this.uiController.Components)
            {
                component.Draw(gameTime, this.spriteBatch);

                if (component is DefenceSelectPanel)
                {
                    //Draw selected defence
                    if (((DefenceSelectPanel)component).SelectedDefence != null)
                        this.spriteBatch.Draw(((DefenceSelectPanel)component).SelectedDefence.GetGraphic().Object,
                            new Rectangle(Mouse.GetState().Position.X - (height / 2), Mouse.GetState().Position.Y - (height / 2), height, height),
                            Color.White);
                }
            }

            this.spriteBatch.End();
        }

        public override void UnloadContent()
        {

        }
    }
}
