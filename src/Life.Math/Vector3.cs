using System;
using System.Runtime.InteropServices;

namespace Life.Math
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    public struct Vector3 : IEquatable<Vector3>
    {
        #region [ Public Members ]

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        #endregion [ Public Members ]

        /// <summary>
        /// Constructs a new vector
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="z">Z component</param>
        public Vector3( float x, float y, float z )
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Returns the length of the vector
        /// </summary>
        /// <returns>Vector length</returns>
        public float Length( )
        {
            return ( float ) System.Math.Sqrt( LengthSquared( ) );
        }

        /// <summary>
        /// Returns the squared length of the vector
        /// </summary>
        /// <remarks>Used for expensive operations that will do square root at the end</remarks>
        /// <returns>Squared length of vector</returns>
        public float LengthSquared( )
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
        }

        /// <summary>
        /// Returns a unit length vector
        /// </summary>
        /// <returns>Unit vector in same direction</returns>
        public Vector3 Unit( )
        {
            return this / Length( );
        }

        #region [ Equality Methods ]

        public bool Equals( Vector3 other )
        {
            return other.X.Equals( this.X ) && other.Y.Equals( this.Y ) && other.Z.Equals( this.Z );
        }


        public static bool operator ==( Vector3 left, Vector3 right )
        {
            return Equals( left, right );
        }

        public static bool operator !=( Vector3 left, Vector3 right )
        {
            return !Equals( left, right );
        }

        #endregion [ Equality Methods ]

        #region [ Math Methods ]
        
        public static Vector3 Add( Vector3 left, Vector3 right )
        {
            return new Vector3( left.X + right.X, left.Y + right.Y, left.Z + right.Z );
        }

        public static Vector3 Subtract( Vector3 left, Vector3 right )
        {
            return new Vector3( left.X - right.X, left.Y - right.Y, left.Z - right.Z );
        }

        public static Vector3 Multiply( Vector3 left, float right )
        {
            return new Vector3( left.X * right, left.Y * right, left.Z * right );
        }

        public static Vector3 Divide( Vector3 left, float right )
        {
            return new Vector3( left.X / right, left.Y / right, left.Z / right );
        }

        public static float Dot( Vector3 v1, Vector3 v2 )
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 Cross( Vector3 left, Vector3 right )
        {
            return new Vector3(
                left.Y * right.Z - left.Z * right.Y,
                left.Z * right.X - left.X * right.Z,
                left.X * right.Y - left.Y * right.X );
        }

        #endregion [ Math Methods ]

        #region [ Object Overrides ]

        public override string ToString( )
        {
            return String.Format( "({0}, {1}, {2})", this.X, this.Y, this.Z );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            return obj.GetType( ) == typeof( Vector3 ) && this.Equals( ( Vector3 )obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var result = this.X.GetHashCode( );
                result = ( result * 397 ) ^ this.Y.GetHashCode( );
                result = ( result * 397 ) ^ this.Z.GetHashCode( );
                return result;
            }
        }

        #endregion [ Object Overrides ]

        #region [ Operators ]

        public static Vector3 operator +( Vector3 left, Vector3 right )
        {
            return Add( left, right );
        }

        public static Vector3 operator -( Vector3 left, Vector3 right )
        {
            return Subtract( left, right );
        }

        public static Vector3 operator /( Vector3 left, float right )
        {
            return Divide( left, right );
        }

        public static Vector3 operator *( Vector3 left, float right )
        {
            return Multiply( left, right );
        }

        public static Vector3 operator *( float left, Vector3 right )
        {
            return Multiply( right, left );
        }

        public static Vector3 operator -( Vector3 vector )
        {
            return new Vector3( -vector.X, -vector.Y, -vector.Z );
        }

        #endregion [ Operators ]

        #region [ Static Properties ]

        public static Vector3 Zero = new Vector3( 0, 0, 0 );

        public static Vector3 UnitX = new Vector3( 1, 0, 0 );
        public static Vector3 UnitY = new Vector3( 0, 1, 0 );
        public static Vector3 UnitZ = new Vector3( 0, 0, 1 );

        #endregion [ Static Properties ]

    }
}
