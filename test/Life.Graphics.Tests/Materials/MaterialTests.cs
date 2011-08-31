using System;
using NUnit.Framework;
using FakeItEasy;

namespace Life.Graphics.Materials.Tests
{
	[TestFixture]
	public class MaterialTests
	{
		private Texture texture;
		private Pass pass;
		
		[SetUp]
		public void Setup( )
		{
			this.texture = A.Fake<Texture>( );
			this.pass = new Pass{
				TextureUnits = new[] {
					new TextureUnit{ Texture = texture } 
				} 
			};
		}
		
		[Test]
		public void Load_LoadsPasses( )
		{
			var material = new Material( "test" ){
				Passes = new[]{ pass } 
			};
			
			material.Load( );
			
			A.CallTo( ( ) => texture.Load( ) ).MustHaveHappened( );
		}
		
		[Test]
		public void Dispose_DisposesTextures( )
		{
			var material = new Material( "test" ){
				Passes = new[]{ pass }
			};
			
			material.Dispose( );
			
			A.CallTo( ( ) => texture.Dispose( ) ).MustHaveHappened( );
		}
	}
}

