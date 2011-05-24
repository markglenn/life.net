using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    public class Vector4Tests
    {
        private readonly Vector4 v1 = new Vector4( 1, 2, 3, 4 );
        private readonly Vector4 v2 = new Vector4( 4, 3, 2, 1 );
       
        [Test]
        public void Ctor_SetsValues( )
        {
            var vector = new Vector4( 1, 2, 3, 4 );

            Assert.AreEqual( 1, vector.X );
            Assert.AreEqual( 2, vector.Y );
            Assert.AreEqual( 3, vector.Z );
            Assert.AreEqual( 4, vector.W );
        }

        [Test]
        public void Equals_TrueIfEqual( )
        {
            Assert.AreEqual( new Vector4( 1, 2, 3, 4 ), new Vector4( 1, 2, 3, 4 ) );
            Assert.AreNotEqual( new Vector4( 1, 2, 3, 4 ), new Vector4( 4, 3, 2, 1 ) );
        }

        [Test]
        public void Add_AddsTwoVectors( )
        {
            var result = Vector4.Add( v1, v2 );
            Assert.AreEqual( new Vector4( 5, 5, 5, 5 ), result );
        }

        [Test]
        public void Subtract_SubtractsTwoVectors( )
        {
            var result = Vector4.Subtract( v1, v2 );
            Assert.AreEqual( new Vector4( -3, -1, 1, 3 ), result );
        }

        [Test]
        public void Multiply_ScalesVectorByScalar( )
        {
            Assert.AreEqual( new Vector4( 2, 4, 6, 8 ), Vector4.Multiply( v1, 2 ) );
        }

        [Test]
        public void Divide_ScalesVectorByScalar( )
        {
            Assert.AreEqual( new Vector4( 0.5f, 1f, 1.5f, 2f ), Vector4.Divide( v1, 2 ) );
        }

        [Test]
        public void LengthSquared_ReturnsSquaredLength( )
        {
            Assert.AreEqual( 30, v1.LengthSquared( ) );
        }

        [Test]
        public void Length_ReturnsLength( )
        {
            Assert.AreEqual( ( float )System.Math.Sqrt( 30 ), v1.Length( ) );
        }

        [Test]
        public void Unit_ReturnsUnitLengthVector( )
        {
            Assert.That( System.Math.Abs( 1f - v1.Unit( ).Length( ) ) < 0.001 );
        }
    }
}
