using NUnit.Framework;
using OQuaternion = OpenTK.Quaternion;
using OMatrix4 = OpenTK.Matrix4;
using System;
namespace Life.Math.Tests
{
    [TestFixture]
    public class QuaternionTests
    {
        [Test]
        public void Ctor_SetsVandW( )
        {
            var vector = new Vector3( 2, 3, 4 );
            var quaternion = new Quaternion( 1, 2, 3, 4 );

            Assert.AreEqual( 1, quaternion.W );
            Assert.AreEqual( vector, quaternion.V );
        }

        [Test]
        public void Ctor_WithWAndVector_SetsVAndW( )
        {
            var vector = new Vector3( 2, 3, 4 );
            var quaternion = new Quaternion( 1, vector );

            Assert.AreEqual( 1, quaternion.W );
            Assert.AreEqual( vector, quaternion.V );
        }

        #region Length Tests

        [Test]
        public void LengthSquared_ReturnsLengthSquared( )
        {
            var quaternion = new Quaternion( 2, 2, 3, 4 );

            Assert.AreEqual( 33, quaternion.LengthSquared( ) );
        }

        [Test]
        public void Length_ReturnsLength( )
        {
            var quaternion = new Quaternion( 2, 2, 3, 4 );
            Assert.AreEqual( 33, System.Math.Round( System.Math.Pow( quaternion.Length( ), 2 ) ) );
        }

        #endregion

        [Test]
        public void Normalized_GetsNormalizedQuaternion( )
        {
            var quaternion = new Quaternion( 1, 2, 3, 4 );

            Assert.That( quaternion.LengthSquared( ) != 1 );
            var delta = System.Math.Abs( 1.0f - quaternion.Normalized( ).LengthSquared( ) );
            Assert.That( delta < 0.01f );
        }

        #region Equals Tests

        [Test]
        public void Equals_IsTrueWhenEqualQuaternions( )
        {
            Assert.AreEqual( new Quaternion( 1, 2, 3, 4 ), new Quaternion( 1, 2, 3, 4 ) );
        }

        [Test]
        public void Equals_IsFalseWhenDifferentQuaternions( )
        {
            Assert.AreNotEqual( new Quaternion( 1, 2, 3, 4 ), new Quaternion( 1, 2, 5, 4 ) );
        }

        #endregion

        #region [ Conjugate Tests ]
        
        [Test]
        public void Conjugate_CalculatesConjugate( )
        {
            var quaternion = new Quaternion( 1, 2, 3, 4 );

            Assert.AreEqual( new Quaternion( 1, -2, -3, -4 ), quaternion.Conjugate( ) );
        }

        [Test]
        public void Conjugate_ReturnsOriginalWhenConjugatingConjugate( )
        {
            var quaternion = new Quaternion( 1, 2, 3, 4 );

            Assert.AreEqual( quaternion, quaternion.Conjugate( ).Conjugate( ) );
        }

        #endregion

        #region [ Math Tests ]

        [Test]
        public void Add_AddsTwoQuaternions( )
        {
            var q1 = new Quaternion( 1, 2, 3, 4 );
            var q2 = new Quaternion( 2, 3, 4, 5 );

            Assert.AreEqual( new Quaternion( 3, 5, 7, 9 ), Quaternion.Add( q1, q2 ) );
        }

        [Test]
        public void Subtract_SubtractsTwoQuaternions( )
        {
            var q1 = new Quaternion( 1, 2, 3, 4 );
            var q2 = new Quaternion( 2, 3, 4, 5 );

            Assert.AreEqual( new Quaternion( -1, -1, -1, -1 ), Quaternion.Subtract( q1, q2 ) );
        }

        [Test]
        public void Multiply_MultipliesQuaternions( )
        {
            var q1 = new Quaternion( 1, 2, 3, 4 );

            Assert.AreEqual( q1, Quaternion.Multiply( q1, Quaternion.Identity ) );
        }

        [Test]
        public void Dot_ReturnsDotProduct( )
        {
            var q1 = new Quaternion( 1, 2, 3, 4 );
            var q2 = new Quaternion( 2, 3, 4, 5 );

            Assert.AreEqual( 40, Quaternion.Dot( q1, q2 ) );

        }

        [Test]
        public void FromAxisAngle_CalculatesQuaternion( )
        {
            var result = Quaternion.FromAxisAngle( new Vector3( 1, 2, 3 ), ( float ) System.Math.PI / 2.0f );
            var expected = new Quaternion( 0.7071068f, 0.7071068f, 1.414214f, 2.12132f );

            Assert.That( Quaternion.Subtract( result, expected ).Length( ) < 0.001f );
        }
		
		[Test]
		public void ToMatrix4_ConvertsToRotationMatrix( )
		{
			var matrix = new Quaternion( 4, 1, 2, 3 ).Normalized( ).ToMatrix4( );
			
			var expected = new Matrix4( 
				0.1333333f, -0.6666667f, 0.7333333f, 0,
				0.9333333f, 0.3333333f, 0.1333334f, 0,
				-0.3333333f, 0.6666666f, 0.6666667f, 0,
				0, 0, 0, 1 );
			
			Assert.AreEqual( expected, matrix );
			for( int i = 0; i < 16; ++i )
				Assert.That( System.Math.Abs( expected.M[i] - matrix.M[ i ] ) < 0.001f );
			
		}
        #endregion
    }
}
