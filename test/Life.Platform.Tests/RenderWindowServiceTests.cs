using System;
using NUnit.Framework;
using FakeItEasy;
using OpenTK.Platform;
using Life;

namespace Life.Platform.Tests
{
	[TestFixture]
	public class RenderWindowServiceTests
	{
		[Test]
		public void Ctor_SetsWindow( )
		{
			var window = A.Dummy<IGameWindow>( );
			
			using( var service = new RenderWindowService( window ) )
				Assert.AreSame( window, service.Window );
		}
		
		[Test]
		public void Dispose_DisposesWindow( )
		{
			var window = A.Fake<IGameWindow>( );
			
			using( var service = new RenderWindowService( window ) ){ }
			A.CallTo( ( ) => window.Dispose( ) ).MustHaveHappened( );
		}
		
		[Test]
		public void WidthAndHeight_SetToWindowSize( )
		{
			var window = A.Fake<IGameWindow>( );
			A.CallTo( ( ) => window.Width ).Returns( 50 );
			A.CallTo( ( ) => window.Height ).Returns( 100 );
			
			using( var service = new RenderWindowService( window ) )
			{
				Assert.AreEqual( 50, service.Width );
				Assert.AreEqual( 100, service.Height );
			}
		}
		
		[Test]
		public void Update_ProcessesEvents( )
		{
			var window = A.Fake<IGameWindow>( );
			using( var service = new RenderWindowService( window ) )
			{
				service.Update( A.Dummy<GameTime>( ) );
				A.CallTo( ( ) => window.ProcessEvents( ) ).MustHaveHappened( );
			}
		}
	}
}

