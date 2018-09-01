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

namespace SiegeOnWindsor.Screens
{
    public class GameScreen : Screen
    {
        World world; //Reference to the world

        SpriteBatch spriteBatch; //Spritebatch

        public GameScreen(SiegeGame game) : base(game)
        {
            this.game = game; //Sets the game
        }


        public override void Initialize()
        {
            this.world = new World(); //Creates a new world
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice); //Creates sprite batch
        }

        public override void Update(GameTime gameTime)
        {
            this.world.Update(gameTime); //Updates world
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //Debugging pathfinding
            Stack<Vector2> test = this.world.aStar.Run(new Vector2(0, 0), new Vector2((this.world.Grid).GetLength(0) / 2, (this.world.Grid).GetLength(1) / 2));
            
            /*
             * DRAW BACKGROUND
             */
            this.spriteBatch.Draw(texture: Textures.gameBackground.Sprite, destinationRectangle: new Rectangle(0, 0, this.game._graphics.PreferredBackBufferWidth, this.game._graphics.PreferredBackBufferHeight), color: Color.White); //Draws the background fullscreen

            /*
             * DRAW TILES
             */
            int height = Convert.ToInt16(Math.Floor((double)(this.game._graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))));

            for (int x = 0; x < (this.world.Grid).GetLength(0); x++)
            {
                for (int y = 0; y < (this.world.Grid).GetLength(1); y++)
                {
                    if (this.world.GetTileAt(x, y).GetGraphic() != null && this.world.GetTileAt(x, y).GetGraphic().Sprite != null)
                    {
                        this.spriteBatch.Draw(this.world.GetTileAt(x, y).GetGraphic().Sprite, new Rectangle(
                            Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                            Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                            height,
                            height),
                            Color.White);
                        //DEBUGGING PATHFINDING
                        Color colorTest;
                        if (test.Contains(new Vector2(x, y)))
                            colorTest = Color.Green;
                        else
                            colorTest = Color.White;
                        this.spriteBatch.DrawString(this.game.Content.Load<SpriteFont>("Fonts/Font"), this.world.RiskMap[x, y].ToString(), new Vector2(Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)), Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height))), colorTest);
                    }
                
                    if (this.world.GetTileAt(x, y).defence != null && this.world.GetTileAt(x, y).defence.GetGraphic() != null && this.world.GetTileAt(x, y).defence.GetGraphic().Sprite != null)
                        this.spriteBatch.Draw(this.world.GetTileAt(x, y).defence.GetGraphic().Sprite, new Rectangle(
                            Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                            Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                            height,
                            height),
                            Color.White);



                    /*
                     * DRAW DEFENCE 
                     */

                    /*
                     * DRAW ENEMIES 
                     */
                    if (this.world.GetTileAt(x, y).enemies != null)
                        foreach (Enemy enemy in this.world.GetTileAt(x, y).enemies)
                        {
                            this.spriteBatch.Draw(enemy.GetGraphic().Sprite, new Rectangle(
                                    Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                                    Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                                    height,
                                    height),
                                    Color.White);
                        }
                }
            }

            /*
             * DRAW UI
             */
            this.spriteBatch.Draw(texture: Textures.defencePanelUI.Sprite, destinationRectangle: new Rectangle(991, 164, Textures.defencePanelUI.Sprite.Bounds.Width, Textures.defencePanelUI.Sprite.Bounds.Height), color: Color.White); //Draws the selection panel

            this.spriteBatch.End();
        }

        public override void UnloadContent()
        {

        }
    }
}
