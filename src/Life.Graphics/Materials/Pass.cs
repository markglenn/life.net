using System;
using System.Collections.Generic;
using System.Drawing;
using Life.Graphics.Materials;

namespace Life.Graphics
{
    public class Pass
    {
        #region [ Public Properties ]

        /// <summary>
        /// Ambient color
        /// </summary>
        public Color Ambient { get; set; }

        /// <summary>
        /// Diffuse color
        /// </summary>
        public Color Diffuse { get; set; }

        /// <summary>
        /// Specular color
        /// </summary>
        public Color Specular { get; set; }

        /// <summary>
        /// Texture units in this pass
        /// </summary>
        public IEnumerable<TextureUnit> TextureUnits { get; set; }

        /// <summary>
        /// Is lighting enabled
        /// </summary>
        public bool LightingEnabled { get; set; }

        #endregion [ Public Properties ]
    }

}

