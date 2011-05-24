using System;

namespace Life.Math
{
    public struct Vector2 : IEquatable<Vector2>
    {
        #region [ Public Properties ]

        public readonly float X;
        public readonly float Y;

        #endregion [ Public Properties ]

        /// <summary>
        /// Constructs a vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Vector2( float x, float y )
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Returns the length of the vector
        /// </summary>
        /// <returns>Vector length</returns>
        public float Length( )
        {
            return ( float ) System.Math.Sqrt( this.X * this.X + this.Y * this.Y );
        }

        /// <summary>
        /// Returns the squared length of the vector
        /// </summary>
        /// <returns>Vector squared length</returns>
        public float SquaredLength( )
        {
            return this.X * this.X + this.Y * this.Y;
        }

        #region [ Math Methods ]

        public static Vector2 Add( Vector2 left, Vector2 right )
        {
            return new Vector2( left.X + right.X, left.Y + right.Y );
        }

        public static Vector2 Subtract( Vector2 left, Vector2 right )
        {
            return new Vector2( left.X - right.X, left.Y - right.Y );
        }

        public static Vector2 Multiply( Vector2 vector, float scalar )
        {
            return new Vector2( vector.X * scalar, vector.Y * scalar );
        }

        public static Vector2 Divide( Vector2 vector, float scalar )
        {
            return new Vector2( vector.X / scalar, vector.Y / scalar );
        }

        #endregion [ Math Methods ]

        #region [ Advanced Math Methods ]

        public static float Dot( Vector2 v1, Vector2 v2 )
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        #endregion [ Advanced Math Methods ]

        #region [ Operators ]

        public static Vector2 operator +( Vector2 left, Vector2 right )
        {
            return Add( left, right );
        }

        public static Vector2 operator -( Vector2 left, Vector2 right )
        {
            return Subtract( left, right );
        }

        public static Vector2 operator /( Vector2 left, float right )
        {
            return Divide( left, right );
        }

        /// <summary>
        /// Multiplies a vector by a scalar
        /// </summary>
        /// <param name="left">Vector to scale</param>
        /// <param name="right">Scale factor</param>
        /// <returns>Scaled vector</returns>
        public static Vector2 operator *( Vector2 left, float right )
        {
            return Multiply( left, right );
        }

        /// <summary>
        /// Returns a negated vector
        /// </summary>
        /// <param name="vector">Original vector</param>
        /// <returns>The vector in the opposite direction</returns>
        public static Vector2 operator -( Vector2 vector )
        {
            return new Vector2( -vector.X, -vector.Y );
        }

        #endregion [ Operators ]

        #region [ Equatable Operations ]

        public bool Equals( Vector2 other )
        {
            return other.X.Equals( this.X ) && other.Y.Equals( this.Y );
        }

        public static bool operator ==( Vector2 left, Vector2 right )
        {
            return Equals( left, right );
        }

        public static bool operator !=( Vector2 left, Vector2 right )
        {
            return !Equals( left, right );
        }

        #endregion [ Equatable Operations ]

        #region [ Object Overrides ]

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;

            return ( obj.GetType( ) == typeof( Vector2 ) && Equals( ( Vector2 )obj ) );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                return ( this.X.GetHashCode( ) * 397 ) ^ this.Y.GetHashCode( );
            }
        }

        public override string ToString( )
        {
            return string.Format( "({0}, {1})", this.X, this.Y );
        }

        #endregion [ Object Overrides ]

        #region [ Static Properties ]

        public static readonly Vector2 Zero = new Vector2( 0, 0 );
        public static readonly Vector2 UnitX = new Vector2( 1, 0 );
        public static readonly Vector2 UnitY = new Vector2( 0, 1 );

        #endregion [ Static Properties ]
    }
}
