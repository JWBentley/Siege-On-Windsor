using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SiegeOnWindsor.data;
using Microsoft.Xna.Framework.Graphics;
using SiegeOnWindsor.Data.Enemies;

namespace SiegeOnWindsor.Screens
{
    public class GameScreen : Screen
    {
        World world;

        SpriteBatch tilesBatch;

        public GameScreen(SiegeGame game) : base(game)
        {
            this.game = game;
        }


        public override void Initialize()
        {
            this.world = new World();
        }

        public override void LoadContent()
        {
            this.tilesBatch = new SpriteBatch(this.game.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            this.world.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawTileGrid();
        }

        private void DrawTileGrid()
        {
            this.tilesBatch.Begin(samplerState: SamplerState.PointClamp);

            int height = Convert.ToInt16(Math.Floor((double)(this.game._graphics.PreferredBackBufferHeight * 0.9 / this.world.Grid.GetLength(1))));

            for (int x = 0; x < (this.world.Grid).GetLength(0); x++)
            {
                for (int y = 0; y < (this.world.Grid).GetLength(1); y++)
                {
                    if (this.world.GetTileAt(x, y).GetGraphic() != null && this.world.GetTileAt(x, y).GetGraphic().Sprite != null)
                            this.tilesBatch.Draw(this.world.GetTileAt(x, y).GetGraphic().Sprite, new Rectangle(
                                Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                                Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                                height,
                                height),
                                Color.White);

                    //Render defence

                    if (this.world.GetTileAt(x, y).enemies != null)
                        foreach (Enemy enemy in this.world.GetTileAt(x, y).enemies)
                        {
                            this.tilesBatch.Draw(enemy.GetGraphic().Sprite, new Rectangle(
                                    Convert.ToInt16(((this.game._graphics.PreferredBackBufferWidth - (this.world.Grid).GetLength(0) * height) / 2) + (x * height)),
                                    Convert.ToInt16((this.game._graphics.PreferredBackBufferHeight * 0.05) + (y * height)),
                                    height,
                                    height),
                                    Color.White);
                        }
                }
            }

            this.tilesBatch.End();
        }

        public override void UnloadContent()
        {

        }
    }
}
