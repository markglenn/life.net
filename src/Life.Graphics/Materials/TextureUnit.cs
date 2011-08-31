using System;

namespace Life.Graphics.Materials
{
	public class TextureUnit
	{
        #region [ Public Properties ]

        /// <summary>
        /// Texture used in this texture unit
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Addressing of the texture
        /// </summary>
        public TextureAddressing TextureAddressing { get; set; }

        #endregion [ Public Properties ]
	}
	
    public enum TextureAddressing
    {
        /// <summary>
        /// Wraps the texture at the 1.0 border
        /// </summary>
        Wrap,
        
        /// <summary>
        /// Mirror the texture at the 1.0 border
        /// </summary>
        Mirror,

        /// <summary>
        ///	Texture clamps at the 1.0 border
        ///	</summary>
        Clamp,

        /// <summary>
        ///	Texture addresses above 1.0 will have border color
        ///	</summary>
        Border
    }
}

