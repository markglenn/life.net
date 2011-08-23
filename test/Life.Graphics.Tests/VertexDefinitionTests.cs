using System;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class VertexDefinitionTests
	{
		[Test]
		public void Ctor_InitializesEmptyDefinition( )
		{
			var definition = new VertexDefinition( );
			
			Assert.AreEqual( 0, definition.Elements.Count( ) );
			Assert.AreEqual( 0, definition.Stride );
		}
		
		[Test]
		public void Add_UpdatesStride( )
		{
			var definition = new VertexDefinition( );
			
			VertexElement element = new VertexElement( VertexElementType.Position,
				VertexElementFormat.Float, 3 );
				
			definition.Add ( element );
			
			Assert.AreEqual( 12, definition.Stride );
		}
	}
}

