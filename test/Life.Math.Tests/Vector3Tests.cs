using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    public class Vector3Tests
    {
        [Test]
        public void Ctor_SetsValues( )
        {
            var v = new Vector3( 1, 2, 3 );

            Assert.AreEqual( 1, v.X );
            Assert.AreEqual( 2, v.Y );
            Assert.AreEqual( 3, v.Z );
        }

        [Test]
        public void Unit_ReturnsUnitVector( )
        {
            var vector = new Vector3( 1, 2, 3 );
            var unit = vector.Unit( );

            Assert.IsTrue( System.Math.Abs( 1.0f - unit.Length( ) ) <= 0.001 );
        }

        #region [ Equals Tests ]

        [Test]
        public void Equals_ReturnsTrueOnEqual( )
        {
            Assert.IsTrue( new Vector3( 1, 2, 3 ).Equals( new Vector3( 1, 2, 3 ) ) );
        }

        [Test] 
        public void Equals_ReturnsFalseOnNotEqual( )
        {
            Assert.IsFalse( new Vector3( 1, 2, 3 ).Equals( new Vector3( 3, 2, 1 ) ) );
        }

        [Test]
        public void Equals_WithNull_IsFalse( )
        {
            Assert.IsFalse( new Vector3( 1, 2, 3 ).Equals( null ) );
        }

        #endregion [ Equals Tests ]

        #region [ Simple Math Tests ]

        [Test]
        public void Add_WithValue_ReturnsAddedVector( )
        {
            var result = Vector3.Add( new Vector3( 1, 2, 3 ), new Vector3( 4, 5, 6 ) );

            Assert.AreEqual( result, new Vector3( 5, 7, 9 ) );
        }

        [Test]
        public void Subtract_WithValue_ReturnsSubtractedVector( )
        {
            var result = Vector3.Subtract( new Vector3( 1, 2, 3 ), new Vector3( 4, 5, 6 ) );

            Assert.AreEqual( result, new Vector3( -3, -3, -3 ) );
        }

        [Test]
        public void Multiply_WithVectorAndScalar_ReturnsScaledVector( )
        {
            var result = Vector3.Multiply( new Vector3( 1, 2, 3 ), 2.0f );
            Assert.AreEqual( new Vector3( 2, 4, 6 ), result );
        }

        [Test]
        public void Divide_WithVectorAndScalar_ReturnsScaledVector( )
        {
            var result = Vector3.Divide( new Vector3( 2, 4, 6 ), 2.0f );
            Assert.AreEqual( new Vector3( 1, 2, 3 ), result );
        }

        #endregion [ Simple Math Tests ]

        #region [ Length / Distance Tests ]

        [Test]
        public void Length_WithVector_ReturnsVectorLength( )
        {
            Assert.AreEqual( 5, new Vector3( 0, 3, 4 ).Length( ) );
        }

        [Test] 
        public void LengthSquared_ReturnsLengthSquared( )
        {
            Assert.AreEqual( 25, new Vector3( 0, 3, 4 ).LengthSquared( ) );
        }

        #endregion [ Length / Distance Tests ]

        #region [ Advanced Math Tests ]

        [Test]
        public void Dot_CalculatesDotProduct( )
        {
            var v1 = new Vector3( 1, 2, 3 );
            var v2 = new Vector3( 2, 3, 4 );

            Assert.AreEqual( 20.0f, Vector3.Dot( v1, v2 ) );
        }

        [Test]
        public void Cross_CalculatesCrossProduct( )
        {
            var v1 = new Vector3( 1, 2, 3 );
            var v2 = new Vector3( 2, 3, 4 );

            Assert.AreEqual( new Vector3( -1, 2, -1 ), Vector3.Cross( v1, v2 ) );
        }

        #endregion [ Advanced Math Tests ]
    }
}
