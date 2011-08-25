using System;
using OpenTK;
using NUnit.Framework;

namespace Life.Graphics.OpenGL.Tests
{
	[TestFixture]
	public class OpenGLIndexBufferTests
	{
		private GameWindow gameWindow;
		
		[TestFixtureSetUp]
		public void SetupFixture( )
		{
			this.gameWindow = new GameWindow( );
		}
		
		[TestFixtureTearDown]
		public void TeardownFixture( )
		{
			gameWindow.Dispose( );
		}
	
		[Test]
		public void Ctor_CreatesIndexBuffer( )
		{
			using( var buffer = new OpenGLIndexBuffer( IndexBufferFormat.UShort, BufferUsage.Static, 1 ) )
				Assert.AreNotEqual( 0, buffer.BufferId );
		}
		
		[Test]
		public void Lock_WithoutDiscard_ThrowsException( )
		{
			using( var buffer = new OpenGLIndexBuffer( IndexBufferFormat.UShort, BufferUsage.Static, 1 ) )
			{
				Assert.Throws<NotSupportedException>( ( ) => buffer.Lock( BufferLock.ReadWrite ) );
				Assert.Throws<NotSupportedException>( ( ) => buffer.Lock( BufferLock.ReadOnly ) );
			}
		}
		
		[Test]
		public void Lock_LocksBuffer( )
		{
			using( var buffer = new OpenGLIndexBuffer( IndexBufferFormat.UShort, BufferUsage.Static, 1 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
					Assert.IsTrue( buffer.IsLocked );	
					
				Assert.IsFalse( buffer.IsLocked );
			}
		}
		
		[Test]
		public void Write_WritesToBuffer( )
		{
			using( var buffer = new OpenGLIndexBuffer( IndexBufferFormat.UShort, BufferUsage.Static, 1 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
					buffer.Write<uint>( 5 );
			}
		}
		
		[Test]
		public void Write_TooManyValues_ThrowsException( )
		{
			using( var buffer = new OpenGLIndexBuffer( IndexBufferFormat.UShort, BufferUsage.Static, 1 ) )
			{
				using( buffer.Lock( BufferLock.Discard ) )
				{
					buffer.Write<uint>( 5 );
					Assert.Throws<InvalidOperationException>( ( ) => buffer.Write<uint>( 6 ) );
				}
			}
		}
	}
}

