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
			public MockHardwareBuffer( VertexDefinition definition, uint numVertices )
				: base( definition, numVertices )
			{
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
