using System;
using NUnit.Framework;
using OpenTK;
using Life.Graphics.OpenGL.Materials;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using FakeItEasy;
using Life.Archive;

namespace Life.Graphics.OpenGL.Tests.Materials
{
	[TestFixture]
	public class OpenGLTextureTests
	{
		private GameWindow gameWindow;

		[TestFixtureSetUp]
		public void FixtureSetup( )
		{
			this.gameWindow = new GameWindow( );	
		}
		
		[TestFixtureTearDown]
		public void FixtureTeardown( )
		{
			this.gameWindow.Dispose( );
		}

		
		static Stream CreateFakeImage( int width, int height )
		{
			var stream = new MemoryStream( );
			var bitmap = new Bitmap( width, height );
			
			bitmap.Save( stream, ImageFormat.Bmp );
			
			return stream;
		}
		
		
		[Test]
		public void LoadsTextureIntoOpenGLContext()
		{
			var archive = A.Fake<IArchive>( );
			
			A.CallTo( ( ) => archive.OpenFile( A<String>.Ignored ) )
				.Returns( CreateFakeImage( 10, 11 ) );
			
			using( var texture = new OpenGLTexture( "test.png", archive ) )
			{
				Assert.IsTrue( texture.Load( ) );
				Assert.AreEqual( 10, texture.Width );
				Assert.AreEqual( 11, texture.Height );
				Assert.AreEqual( PixelFormat.Format32bppPArgb, texture.Format );
			}
		}
		
		[Test]
		public void Load_CreatesOpenGLTexture( )
		{
			var archive = A.Fake<IArchive>( );
			
			A.CallTo( ( ) => archive.OpenFile( A<String>.Ignored ) )
				.Returns( CreateFakeImage( 10, 11 ) );
			
			using( var texture = new OpenGLTexture( "test.png", archive ) )
			{
				texture.Load( );
				Assert.AreNotEqual( 0, texture.TextureId );
			}
		}
	}
}

