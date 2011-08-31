using System;
using Life.Archive;
using System.Drawing.Imaging;

namespace Life.Graphics.Materials
{
	public abstract class Texture : ResourceBase
    {
        #region [ Private Members ]

        private readonly IArchive archive;

        #endregion [ Private Members ]

        protected Texture( string name, IArchive archive )
            : base( name )
        {
            if( archive == null ) 
                throw new ArgumentNullException( "archive" );

            this.archive = archive;
        }

        public IArchive Archive
        {
            get { return this.archive; }
        }

        /// <summary>
        /// Width of the texture in pixels
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        /// Height of the texture in pixels
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Format of each pixel
        /// </summary>
        public abstract PixelFormat Format { get; }
    }
}

