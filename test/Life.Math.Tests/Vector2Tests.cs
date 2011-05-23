using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    public class Vector2Tests
    {
        [Test]
        public void Ctor_SetsXandY( )
        {
            var vector = new Vector2( 1, 2 );

            Assert.AreEqual( 1, vector.X );
            Assert.AreEqual( 2, vector.Y );
        }

        [Test]
        public void Equals_ReturnsTrueWhenEqual( )
        {
            Assert.AreEqual( new Vector2( 1, 2 ), new Vector2( 1, 2 ) );
        }

        [Test]
        public void Length_ReturnsVectorLength( )
        {
            Assert.AreEqual( 5, new Vector2( 3, 4 ).Length( ) );
        }

        [Test]
        public void Add_AddsTwoVectors( )
        {
            var result = Vector2.Add( new Vector2( 1, 2 ), new Vector2( 3, 4 ) );

            Assert.AreEqual( new Vector2( 4, 6 ), result );
        }

        [Test]
        public void Subtract_SubtractsTwoVectors( )
        {
            var result = Vector2.Subtract( new Vector2( 1, 2 ), new Vector2( 3, 4 ) );

            Assert.AreEqual( new Vector2( -2, -2 ), result );
        }

    }
}
