using System;
using NUnit.Framework;
using FakeItEasy;
using Life.Graphics;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class HardwareVertexBufferTests
	{
		class MockHardwareBuffer : HardwareVertexBuffer
		{
			public MockHardwareBuffer( VertexDefinition definition, int numVertices )
				: base( definition, numVertices )
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
			
			public override void Write<T>( T val )
			{
				throw new NotImplementedException ();
			}
			
			public override void Write<T>( T[ ] val )
			{
				throw new NotImplementedException ();
			}

		}
		
		[Test]
		public void Ctor_SetsDefinitionAndNumVertices( )
		{
			VertexElement element = new VertexElement( VertexElementType.Color,
				VertexElementFormat.Float, 4 );
				
			VertexDefinition definition = new VertexDefinition( new[] { element } );
			HardwareVertexBuffer buffer = new MockHardwareBuffer( definition, 5 );
			
			Assert.AreSame( definition, buffer.VertexDefinition );
			Assert.AreEqual( 16, buffer.Stride );
			Assert.AreEqual( 5, buffer.NumVertices );
		}
		
	}
}

