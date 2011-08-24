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
            fake.Configure( ).CallsTo( m => m.Start( ) ).MustHaveHappened( );
        }

        private static IService CreateFakeService( uint priority )
        {
            var fake = A.Fake<IService>( );

            fake.Configure( ).CallsTo( m => m.Priority ).Returns( priority );
            fake.Configure( ).CallsTo( m => m.Status ).Returns( ServiceStatus.Alive );

            return fake;
        }
    }
}
