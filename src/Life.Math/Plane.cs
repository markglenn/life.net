using System;

namespace Life.Math
{
    public enum PlaneSide
    {
        Front,
        Back,
        Coplanar
    }

    /// <summary>
    /// Three dimensional plane
    /// </summary>
    public struct Plane : IEquatable<Plane>
    {
        #region [ Public Properties ]
        
        /// <summary>
        /// Normal vector
        /// </summary>
        public readonly Vector3 Normal;

        /// <summary>
        /// Plane D component
        /// </summary>
        public readonly float D;

        #endregion [ Public Properties ]

        #region [ Constructors ]

        public Plane( Vector3 normal, float d )
        {
            this.Normal = normal;
            this.D = d;
        }

        public Plane( float x, float y, float z, float d )
        {
            this.Normal = new Vector3( x, y, z );
            this.D = d;
        }

		public Plane( Vector4 v )
		{
			this.Normal = new Vector3( v.X, v.Y, v.Z );
			this.D = v.W;
		}
		
        #endregion [ Constructors ]

        public float DistanceTo( Vector3 point )
        {
            return Vector3.Dot( this.Normal, point ) + this.D; 
        }

        public PlaneSide Side( Vector3 point )
        {
            var distance = DistanceTo( point );

            if ( distance > float.Epsilon )
                return PlaneSide.Front;
            if ( distance < -float.Epsilon )
                return PlaneSide.Back;

            return PlaneSide.Coplanar;
        }
        
		
        #region [ IEquality<Plane> Implementation ]

		public override bool Equals( object obj )
		{
            if ( ReferenceEquals( null, obj ) ) return false;
            return obj.GetType( ) == typeof( Matrix4 ) && this.Equals( ( Matrix4 )obj );
 
		}
		
        public bool Equals( Plane other )
        {
            return 
				this.Normal.Equals( other.Normal ) &&
				this.D.Equals( other.D );
        }

        public static bool operator ==( Plane left, Plane right )
        {
            return left.Equals( right );
        }

        public static bool operator !=( Plane left, Plane right )
        {
            return !left.Equals( right );
        }

        #endregion
        
        public override int GetHashCode( )
        {
            unchecked
            {
                var result = this.Normal.GetHashCode( );
                return ( result * 397 ) ^ this.D.GetHashCode( );
            }
        }
        
        public override string ToString( )
        {
            return String.Format( "({0}, {1})", this.Normal, this.D );
        }
    }
}
