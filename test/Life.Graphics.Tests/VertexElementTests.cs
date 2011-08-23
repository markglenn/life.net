using System;
using NUnit.Framework;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class VertexElementTests
	{
		[Test]
		public void Ctor_SetsFormatAndType( )
		{
			var element = new VertexElement( VertexElementType.Color, VertexElementFormat.Int, 4 );
			
			Assert.AreEqual ( VertexElementType.Color, element.Type );
			Assert.AreEqual ( VertexElementFormat.Int, element.Format );
		}
		
		[Test]
		public void Ctor_SetsSize( )
		{
			Assert.AreEqual( 16, new VertexElement( VertexElementType.Color, VertexElementFormat.Int, 4 ).Size );
			Assert.AreEqual( 12, new VertexElement( VertexElementType.Color, VertexElementFormat.Int, 3 ).Size );
			Assert.AreEqual( 4, new VertexElement( VertexElementType.Color, VertexElementFormat.Short, 2 ).Size );
		}
		
		[Test]
		public void Compare_ComparesTypes( )
		{
			VertexElement element1 = new VertexElement( VertexElementType.Texture1, VertexElementFormat.Int, 1 );
			VertexElement element2 = new VertexElement( VertexElementType.Texture0, VertexElementFormat.Int, 1 );
			
			Assert.AreEqual( 1, element1.CompareTo( element2 ) );
			Assert.AreEqual( 0, element1.CompareTo( element1 ) );
		}
	}
}

