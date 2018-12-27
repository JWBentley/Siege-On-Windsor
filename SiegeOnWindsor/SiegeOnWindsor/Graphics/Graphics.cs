using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeOnWindsor.Graphics
{
    public class Graphics
    {
        //List of textures

        //Backgrounds
        public static Graphic menuBackground;
        public static Graphic gameBackground;

        //UI
        public static Graphic defencePanelUI;

        //Tiles
        public static Graphic emptyTile;
        public static Graphic spawnTile;

        //Defences
        public static Graphic crownDef;
        public static Graphic stoneWallDef;
        public static Graphic guardDef;
        public static Graphic archerDef;
        public static Graphic catapultDef;

        //Enemies
        public static Graphic peasantEnemy_Left, peasantEnemy_Right;

        //List of fonts

        public static Font arial32;
        public static Font arial16;

        /// <summary>
        /// Loads the textures
        /// </summary>
        /// <param name="game">Game with content manager</param>
        public static void Load(SiegeGame game)
        {
            menuBackground = new Graphic("Backgrounds/menu_screen", game);
            gameBackground = new Graphic("Backgrounds/game_screen", game);

            defencePanelUI = new Graphic("UI/Panels/defence_panel", game);

            emptyTile = new Graphic("Tile/empty_tile", game);
            spawnTile = new Graphic("Tile/null_tile", game);

            crownDef = new Graphic("Tile/Defence/crown_defence", game);
            stoneWallDef = new Graphic("Tile/Defence/stone_wall_defence", game);
            guardDef = new Graphic("Tile/Defence/guard_defence", game);
            archerDef = new Graphic("Tile/Defence/archer_defence", game);
            catapultDef = new Graphic("Tile/Defence/catapult_defence", game);

            peasantEnemy_Left = new Graphic("Enemies/peasant_enemy_left", game);
            peasantEnemy_Right = new Graphic("Enemies/peasant_enemy_right", game);
            //testAnimation = new Animation(new String[] { "Enemies/Left", "Enemies/Right" }, game);

            arial32 = new Font("Fonts/default32", game);
            arial16 = new Font("Fonts/default16", game);
        }

        

        public class Visual<T>
        {
            /// <summary>
            /// Image to be drawn
            /// </summary>
            public T Object { set; get; }
            /// <summary>
            /// String ref
            /// </summary>
            public String Name { get; }

            public Visual(String s, SiegeGame game)
            {
                this.Name = s;
                //game.LoadBuffer.Add(this); //Calls for the game to load the image from the string ref
                this.Object = game.Content.Load<T>(this.Name);
            }
        }

        public class Graphic : Visual<Texture2D>
        {
            public Graphic(string s, SiegeGame game) : base(s, game)
            {

            }
        }

        public class Font : Visual<SpriteFont>
        {
            public Font(string s, SiegeGame game) : base(s, game)
            {

            }
        }

        /*
        public class Animation
        {
            Texture[] Sprites;
            int currentFrame = 0;

            public Animation(Animation a)
            {
                this.Sprites = a.Sprites;
            }

            public Animation(String[] s, SiegeGame game)
            {
                this.Sprites = new Texture[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    this.Sprites[i] = new Texture(s[i], game);
                }
            }

            public Texture GetFrame()
            {
                return this.Sprites[this.currentFrame];
            }

            public void NextFrame()
            {
                if (this.Sprites != null)
                {
                    if (this.currentFrame < this.Sprites.Length - 1)
                        this.currentFrame++;
                    else
                        this.currentFrame = 0;
                }
            }
        }
        */
    }
}
