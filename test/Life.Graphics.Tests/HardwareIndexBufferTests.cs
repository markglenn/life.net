using System;
using NUnit.Framework;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class HardwareIndexBufferTests
	{
		class MockHardwareIndexBuffer : HardwareIndexBuffer
		{
			public MockHardwareIndexBuffer( IndexBufferFormat format, int num )
				: base( format, num )
			{
			}
			
			protected override bool DoLock (BufferLock lockType)
			{
				throw new NotImplementedException ();
			}
			
			protected override void DoUnlock ()
			{
				throw new NotImplementedException ();
			}
		}
		
		[Test]
		public void Ctor_SetsFormatAndNumIndices( )
		{
			HardwareIndexBuffer buffer = new MockHardwareIndexBuffer( IndexBufferFormat.Int, 4 );
			
			Assert.AreEqual( IndexBufferFormat.Int, buffer.Format );
			Assert.AreEqual( 4, buffer.NumIndices );
		}
	}
}

