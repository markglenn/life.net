using System;
using NUnit.Framework;
using FakeItEasy;
using Life.Archive;

namespace Life.Graphics.Tests
{
	[TestFixture]
	public class TextureManagerTests
	{
		[Test]
		public void Load_LoadsNewTexture( )
		{
			var device = A.Fake<IDevice>( );
			var archive = A.Dummy<IArchive>( );
			
			var manager = new TextureManager( device );
			
			manager.Load( archive, "example_file.png" );
			A.CallTo( ( ) => device.CreateTexture( archive, "example_file.png" ) )
				.MustHaveHappened( );
		}
		
		[Test]
		public void Load_ReturnsExistingTexture( )
		{
			var device = A.Fake<IDevice>( );
			var archive = A.Dummy<IArchive>( );
			
			var manager = new TextureManager( device );
			
			manager.Load( archive, "example_file.png" );
			manager.Load( archive, "example_file.png" );
			
			A.CallTo( ( ) => device.CreateTexture( archive, "example_file.png" ) )
				.MustHaveHappened( Repeated.Exactly.Once );
				
		}
	}
}

