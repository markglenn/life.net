using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Math
{
    public class Plane
    {
        #region [ Public Properties ]
        
        public readonly Vector3 Normal;
        public readonly float D;

        #endregion [ Public Properties ]

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

        public float DistanceTo( Vector3 point )
        {
            return Vector3.Dot( this.Normal, point ) + this.D; 
        }
    }
}
