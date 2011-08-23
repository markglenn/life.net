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
		private VertexElement element;
		private VertexDefinition definition;
		
		[TestFixtureSetUp]
		public void SetupFixture( )
		{
			this.gameWindow = new GameWindow( );
			this.element = new VertexElement( VertexElementType.Normal, VertexElementFormat.Float, 3 );
			this.definition = new VertexDefinition( new[]{ element } );
		}
		
		[TestFixtureTearDown]
		public void TeardownFixture( )
		{
			gameWindow.Dispose( );
		}
		
		[Test]
		public void Ctor_CreatesBuffer( )
		{
			using( var buffer = new OpenGLVertexBuffer( A.Dummy<VertexDefinition>( ), BufferUsage.Static, 1 ) )
				Assert.AreNotEqual( 0, buffer.BufferID );
		}
		
		[Test]
		public void Ctor_SetsBufferSize( )
		{
			using( var buffer = new OpenGLVertexBuffer( definition, BufferUsage.Static, 4 ) )
			{
				Assert.AreEqual( 48, buffer.BufferSize );
			}
		}
		
		[Test]
		public void Lock_WithoutDiscard_ThrowsException( )
		{
			using( var buffer = new OpenGLVertexBuffer( definition, BufferUsage.Static, 4 ) )
			{
				Assert.Throws<NotSupportedException>( ( ) => buffer.Lock( BufferLock.ReadWrite ) );
				Assert.Throws<NotSupportedException>( ( ) => buffer.Lock( BufferLock.ReadOnly ) );
			}
		}
		
		[Test]
		public void Lock_LocksBuffer( )
		{
			using( var buffer = new OpenGLVertexBuffer( definition, BufferUsage.Static, 4 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
					Assert.IsTrue( buffer.IsLocked );	
					
				Assert.IsFalse( buffer.IsLocked );
			}
		}
	}
}

