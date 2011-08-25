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
		struct Vertex
		{
			public float X, Y, Z;
		}
		
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
				Assert.AreNotEqual( 0, buffer.BufferId );
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
		
		[Test]
		public void Write_WritesToBuffer( )
		{
			using( var buffer = new OpenGLVertexBuffer( definition, BufferUsage.Static, 4 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
				{
					buffer.Write<Vertex>( new Vertex{ X = 1, Y = 2, Z = 3 } );
				}
			}
		}
		
		[Test]
		public void Write_TooManyValues_ThrowsException( )
		{
			using( var buffer = new OpenGLVertexBuffer( definition, BufferUsage.Static, 1 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
				{
					buffer.Write<Vertex>( new Vertex{ X = 1, Y = 2, Z = 3 } );
					Assert.Throws<InvalidOperationException>( ( ) =>
						buffer.Write<Vertex>( new Vertex{ X = 1, Y = 2, Z = 3 } ) );
				}
			}
		}
	}
}

