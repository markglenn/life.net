using System;
using System.Linq;
using NUnit.Framework;

namespace Life.Math.Tests
{
    [TestFixture]
    class Matrix4Tests
    {
        private float[ ] values;

        [SetUp]
        public void Setup( )
        {
            this.values = Enumerable.Range( 1, 16 ).Select( i => ( float )i ).ToArray( );
        }

        [Test]
        public void Ctor_WithSingleValue_SetsAllToValue( )
        {
            Assert.IsTrue( new Matrix4( 0 ).M.All( m => m == 0 ) );
            Assert.IsTrue( new Matrix4( 1 ).M.All( m => m == 1 ) );
        }

        [Test]
        public void Ctor_With16Values_SetsAllValues( )
        {
            var m = new Matrix4( 
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16 );

            Assert.AreEqual( new float[ ]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, m.M );
        }

        [Test]
        public void Ctor_WithArray_SetsAllValues( )
        {
            Assert.AreEqual( values, new Matrix4( values ).M );
        }

        [Test]
        public void Ctor_WithInvalidArray_ThrowsArgumentOutOfRangeException( )
        {
            Assert.Throws<ArgumentOutOfRangeException>( ( ) => new Matrix4( new float[14] ) );
        }

        [Test]
        public void Equals_ReturnsTrueForSameValues( )
        {
            Assert.AreEqual( new Matrix4( 1 ), new Matrix4( 1 ) );
            Assert.AreNotEqual( new Matrix4( 0 ), new Matrix4( 1 ) );
        }

        [Test]
        public void Transpose_TransposesMatrix( )
        {
            var matrix = new Matrix4( values );
            var matrix2 = new Matrix4( 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15, 4, 8, 12, 16 );

            Assert.AreEqual( matrix2, matrix.Transpose( ) );
        }

        [Test]
        public void Add_AddsMatrices( )
        {
            var matrix1 = new Matrix4( values );
            var matrix2 = new Matrix4( values.Reverse( ).ToArray( ) );

            var expected = new Matrix4( 17 );

            Assert.AreEqual( expected, Matrix4.Add( matrix1, matrix2 ) );
        }

        [Test]
        public void Subtract_SubtractsMatrices( )
        {
            var matrix1 = new Matrix4( values );
            var matrix2 = new Matrix4( values.Reverse( ).ToArray( ) );

            var expected = new Matrix4( -15, -13, -11, -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, 11, 13, 15 );

            Assert.AreEqual( expected, Matrix4.Subtract( matrix1, matrix2 ) );
        }

        [Test]
        public void Scale_CreatesScaleMatrix( )
        {
            var matrix = Matrix4.Scale( 1, 2, 3 );

            var expected = new Matrix4(
                1, 0, 0, 0,
                0, 2, 0, 0,
                0, 0, 3, 0,
                0, 0, 0, 1
            );

            Assert.AreEqual( expected, matrix );
        }

        [Test]
        public void Scale_WithVector_CreatesScaleMatrix( )
        {
            Assert.AreEqual( Matrix4.Scale( 1, 2, 3 ), Matrix4.Scale( new Vector3( 1, 2, 3 ) ) );
        }

        [Test]
        public void Multiply_AgainstIdentity_ReturnsUnchangedMatrix( )
        {
            var matrix = new Matrix4( values );

            Assert.AreEqual( matrix, Matrix4.Multiply( matrix, Matrix4.Identity ) );
        }
 
        [Test]
        public void Translate_CreatesTranslationMatrix( )
        {
            var translation = Matrix4.Translation( 1, 2, 3 );

            var expected = new Matrix4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                1, 2, 3, 1
            );

            Assert.AreEqual( expected, translation );
        }
    }
}
