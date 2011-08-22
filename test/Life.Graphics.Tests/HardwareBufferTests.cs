using System;
using NUnit.Framework;
using FakeItEasy;
using OpenTK.Graphics;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class HardwareBufferTests
	{
		class MockHardwareBuffer : HardwareBuffer
		{
			
		}
		
		[Test]
		public void Ctor_CreatesBuffer( )
		{
			GraphicsContext.CreateDummyContext( );
			using( var buffer = new MockHardwareBuffer( ) )
				Assert.AreNotEqual( 0, buffer.BufferID );
		}
	}
}

