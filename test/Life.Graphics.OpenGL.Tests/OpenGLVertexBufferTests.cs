using System;
using NUnit.Framework;
using OpenTK.Graphics;
using OpenTK;

namespace Life.Graphics.OpenGL.Tests
{
	[TestFixture]
	public class OpenGLVertexBufferTests
	{
		private GameWindow gameWindow;
		
		[TestFixtureSetUp]
		public void SetupFixture( )
		{
			gameWindow = new GameWindow( );
		}
		
		[TestFixtureTearDown]
		public void TeardownFixture( )
		{
			gameWindow.Dispose( );
		}
		
		[Test]
		public void Ctor_CreatesBuffer( )
		{
			using( var buffer = new OpenGLVertexBuffer( ) )
			{
				Assert.AreNotEqual( 0, buffer.BufferID );
			}
		}
	}
}

