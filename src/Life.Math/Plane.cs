using System;

namespace Life.Math
{
    public enum PlaneSide
    {
        Front,
        Back,
        Coplanar
    }

    public class Plane
    {
        #region [ Public Properties ]
        
        public readonly Vector3 Normal;
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
    }
}
