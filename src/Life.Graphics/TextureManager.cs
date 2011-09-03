using System;
using System.Collections.Generic;
using Life.Graphics.Materials;
using Life.Archive;

namespace Life.Graphics
{
   public class TextureManager
    {
        #region [ Private Members ]

        private readonly IDictionary<string, Texture> textures = new Dictionary<string, Texture>( );
        private readonly IDevice device;

        #endregion [ Private Members ]

        public TextureManager( IDevice device )
        {
            if( device == null ) 
                throw new ArgumentNullException( "device" );

            this.device = device;
        }

        /// <summary>
        /// Loads a texture from a file archive
        /// </summary>
        /// <param name="archive">Archive that stores the file</param>
        /// <param name="filename">Name of the file to load</param>
        /// <returns>A new texture</returns>
        public Texture Load( IArchive archive, string filename )
        {
            if( filename == null ) 
                throw new ArgumentNullException( "filename" );

            Texture texture;

            // Try to get the texture if its already loaded
            if ( !this.textures.TryGetValue( filename, out texture ))
            {
                // Tell the driver to create a texture from this
                texture = this.device.CreateTexture( archive, filename );

                this.textures[ filename ] = texture;
            }

            return texture;
        }
    }
}

