using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax;
using NUnit.Framework;

namespace Life.Tests
{
    [TestFixture]
    public class KernelTests
    {
        private Kernel kernel;

        [SetUp]
        public void Setup( )
        {
            this.kernel = new Kernel( );
        }

        [Test]
        public void Ctor_HasNoServices( )
        {
            Assert.That( this.kernel.Any( ) == false );
        }

        [Test]
        public void AddService_AddsService( )
        {
            var service = A.Fake<IService>( );
            this.kernel.AddService( service );

            Assert.That( this.kernel.Any( s => s == service ) );
        }

        [Test]
        public void AddService_AddsWithPriority( )
        {
            for ( uint i = 0; i < 5; ++i )
                this.kernel.AddService( CreateFakeService( 5 - i ) );

            Assert.That( this.kernel.Select( s => s.Priority ).SequenceEqual( new uint[ ]{ 1, 2, 3, 4, 5 } ) );
        }

        [Test]
        public void AddService_StartsService( )
        {
            var fake = A.Fake<IService>( );

            this.kernel.AddService( fake );
            fake.Configure( ).CallsTo( m => m.Start( this.kernel ) ).MustHaveHappened( );
        }

		[Test]
		public void Run_ExitsWithNoServices( )
		{
			this.kernel.Run( );
		}
		
		[Test]
		public void Run_ExitsWithDeadService( )
		{
			var service = CreateFakeService( 100 );
			this.kernel.AddService( service );
			A.CallTo( ( ) => service.Status ).Returns( ServiceStatus.Dead );
			
			this.kernel.Run( );
		}
		
        private static IService CreateFakeService( uint priority )
        {
            var fake = A.Fake<IService>( );
            
			A.CallTo( ( ) => fake.Priority ).Returns( priority );
			A.CallTo( ( ) => fake.Status ).Returns( ServiceStatus.Alive );

            return fake;
        }
    }
}
