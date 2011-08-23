using System;
using NUnit.Framework;
using OpenTK.Graphics;
using OpenTK;
using FakeItEasy;

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
			using( var buffer = new OpenGLVertexBuffer( A.Dummy<VertexDefinition>( ), 1 ) )
				Assert.AreNotEqual( 0, buffer.BufferID );
		}
	}
}

