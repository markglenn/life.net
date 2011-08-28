using System;
using NUnit.Framework;
using FakeItEasy;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class RenderOperationTests
	{
		[Test]
		public void Ctor_SetsOperationTypeAndCount( )
		{
			var operation = new RenderOperation( OperationType.LineList, 5,
				A.Dummy<HardwareVertexBuffer>( ), A.Dummy<HardwareIndexBuffer>( ) );
				
			Assert.AreEqual( OperationType.LineList, operation.OperationType );
			Assert.AreEqual( 5, operation.PrimitiveCount );
		}
		
		[Test]
		public void Ctor_SetsBuffers( )
		{
			var vertexBuffer = A.Dummy<HardwareVertexBuffer>( );
			var indexBuffer = A.Dummy<HardwareIndexBuffer>( );
			
			var operation = new RenderOperation( OperationType.LineList, 1,
				vertexBuffer, indexBuffer );
				
			Assert.AreSame( vertexBuffer, operation.VertexBuffer );
			Assert.AreSame( indexBuffer, operation.IndexBuffer );
		}
	}
}

