using System;
using Life.Graphics.Resources;
using Life.Archive;
using System.Drawing.Imaging;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using OpenTKPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Life.Graphics.OpenGL
{
	public class OpenGLTexture : Texture
	{
		private uint textureId;
		private int width, height;
		
		public OpenGLTexture( string name, IArchive archive )
			: base( name, archive )
		{
		}

		public override bool DoLoad( )
		{
			using( var stream = this.Archive.OpenFile( this.Name ) )
            {
                if ( stream == null )
                    return false;

				// Create the texture within OpenGL
				GL.GenTextures( 1, out textureId );
				GL.BindTexture( TextureTarget.Texture2D, textureId );
				
				// Load the image from the stream
				var image = new Bitmap( stream );
				
	            BitmapData data = image.LockBits( new Rectangle( 0, 0, image.Width, image.Height ),
	                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );

				// Write the raw bitmap to the hardware buffer
				GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height,
					0, OpenTKPixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0 );
					
				image.UnlockBits( data );
				
				this.width = image.Width;
				this.height = image.Height;
            }

            return true;
		}
		
		public override bool DoUnload( )
		{
			GL.DeleteTexture( this.textureId );
			return true;
		}
		
		#region [ Implemented abstract members of Texture ]
		
		public override int Width 
		{
			get { return this.width; }
		}

		public override int Height 
		{
			get { return this.height; }
		}

		public override PixelFormat Format 
		{
			get { return PixelFormat.Format32bppPArgb; }
		}
		
		#endregion
	}
}

