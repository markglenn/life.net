using System;
using System.Runtime.InteropServices;

namespace Life.Math
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    public struct Quaternion : IEquatable<Quaternion>
    {
        #region [ Public Properties ]
        
        public readonly float W;
        public readonly Vector3 V;

        #endregion [ Public Properties ]

        public Quaternion( float w, float x, float y, float z )
            : this( w, new Vector3( x, y, z ) ) {}

        public Quaternion( float w, Vector3 v )
        {
            this.W = w;
            this.V = v;
        }

        /// <summary>
        /// Gets the squared length of the quaternion
        /// </summary>
        /// <returns>Squared length</returns>
        public float LengthSquared( )
        {
            return this.W * this.W + this.V.LengthSquared( );
        }

        public float Length( )
        {
            return ( float ) System.Math.Sqrt( LengthSquared( ) );
        }

        public Quaternion Normalized( )
        {
            float scale = 1.0f / this.Length( );

            return new Quaternion( this.W * scale, this.V * scale );
        }

        public Quaternion Conjugate( )
        {
            return new Quaternion( this.W, -this.V );
        }

        #region [ IEquality Implementation ]

        public bool Equals( Quaternion other )
        {
            return other.W.Equals( this.W ) && other.V.Equals( this.V );
        }

        public static bool operator ==( Quaternion left, Quaternion right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Quaternion left, Quaternion right )
        {
            return !left.Equals( right );
        }

        #endregion [ IEquality Implementation ]

        #region [ Object Overrides ]

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            return obj.GetType( ) == typeof( Quaternion ) && Equals( ( Quaternion )obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                return ( this.W.GetHashCode( ) * 397 ) ^ this.V.GetHashCode( );
            }
        }

        #endregion [ Object Overrides ]

        public static Quaternion Add( Quaternion left, Quaternion right )
        {
            return new Quaternion( left.W + right.W, left.V + right.V );
        }

        public static Quaternion Subtract( Quaternion left, Quaternion right )
        {
            return new Quaternion( left.W - right.W, left.V - right.V );
        }

        public static readonly Quaternion Identity = new Quaternion( 1, Vector3.Zero );

        public static Quaternion Multiply( Quaternion left, Quaternion right )
        {
            return new Quaternion( 
                left.W * right.W - Vector3.Dot( left.V, right.V ),
                left.W * right.V + right.W * left.V + Vector3.Cross( left.V, right.V ) );
        }
    }
}
