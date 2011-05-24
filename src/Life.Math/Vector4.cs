using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Math
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public Vector4( float x, float y, float z, float w )
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        #region [ Math Operations ]

        public static Vector4 Add( Vector4 left, Vector4 right )
        {
            return new Vector4( left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W );
        }

        public static Vector4 Subtract( Vector4 left, Vector4 right )
        {
            return new Vector4( left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W );
        }

        #endregion [ Math Operations ]

        #region [ IEquality<Vector4> Implementation ]

        public bool Equals( Vector4 other )
        {
            return 
                other.X.Equals( this.X ) && 
                other.Y.Equals( this.Y ) && 
                other.Z.Equals( this.Z ) && 
                other.W.Equals( this.W );
        }

        public static bool operator ==( Vector4 left, Vector4 right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Vector4 left, Vector4 right )
        {
            return !left.Equals( right );
        }

        #endregion [ IEquality<Vector4> Implementation ]

        #region [ Object Overrides ]

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            return obj.GetType( ) == typeof( Vector4 ) && this.Equals( ( Vector4 )obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var result = this.X.GetHashCode( );
                result = ( result * 397 ) ^ this.Y.GetHashCode( );
                result = ( result * 397 ) ^ this.Z.GetHashCode( );
                result = ( result * 397 ) ^ this.W.GetHashCode( );
                return result;
            }
        }

        #endregion [ Object Overrides ]

        public static Vector4 Multiply( Vector4 left, float right )
        {
            return new Vector4( left.X * right, left.Y * right, left.Z * right, left.W * right );
        }

        public static Vector4 Divide( Vector4 left, float right )
        {
            return new Vector4( left.X / right, left.Y / right, left.Z / right, left.W / right );
        }

        public float LengthSquared( )
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
        }

        public float Length( )
        {
            return ( float )System.Math.Sqrt( LengthSquared( ) );
        }

        public Vector4 Unit( )
        {
            return Vector4.Divide( this, this.Length( ) );
        }
    }
}
