﻿using System;
using NUnit.Framework;

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
            Assert.That( quaternion.Normalized( ).LengthSquared( ) == 1 );
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

        #endregion
    }
}
