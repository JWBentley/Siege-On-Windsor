﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiegeOnWindsor.Graphics;
using SiegeOnWindsor.data;

namespace SiegeOnWindsor.Data.Defences
{
    public abstract class Defence : IUpdate
    {
        private Textures.Texture graphic;

        public Defence()
        {

        }

        public Defence(Textures.Texture g)
        {
            this.graphic = g;
        }

        public abstract void Update();

        public virtual Textures.Texture GetGraphic()
        {
            return this.graphic != null ? this.graphic : Textures.emptyTile;
        }
    }
}
