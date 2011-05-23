using System;

namespace Life.Math
{
    public class Vector2 : IEquatable<Vector2>
    {
        #region [ Public Properties ]

        public readonly float X;
        public readonly float Y;

        #endregion [ Public Properties ]

        public Vector2( float x, float y )
        {
            this.X = x;
            this.Y = y;
        }

        #region [ Equatable Operations ]

        public bool Equals( Vector2 other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
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
            if ( ReferenceEquals( this, obj ) ) return true;
            
            return ( obj.GetType( ) == typeof( Vector2 ) && Equals( ( Vector2 ) obj ) );
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

        public float Length( )
        {
            return ( float ) System.Math.Sqrt( this.X * this.X + this.Y * this.Y );
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

        #endregion [ Math Methods ]

    }
}
