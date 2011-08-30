namespace Life.Math.Bounds
{

    /// <summary>
    /// Defines the 6 planes the make up a frustum.
    /// </summary>
    public enum FrustumPlane
    {
        /// <summary>
        /// Z Near plane
        /// </summary>
        Near = 0,

        /// <summary>
        /// Z Far plane
        /// </summary>
        Far,

        /// <summary>
        /// Left plane
        /// </summary>
        Left,

        /// <summary>
        /// Right plane
        /// </summary>
        Right,

        /// <summary>
        /// Top plane
        /// </summary>
        Top,

        /// <summary>
        /// Bottom plane
        /// </summary>
        Bottom,
        
    }

    /// <summary>
    /// Viewing frustum of some point (such as a camera)
    /// </summary>
    public class Frustum
    {
        #region [ Private Members ]

        private Matrix4 projection;
        private Matrix4 view;

        private bool isDirty;

        private Plane[ ] planes;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        /// <summary>
        /// Projection Matrix
        /// </summary>
        public Matrix4 Projection
        {
            get { return this.projection; }
            set
            {
                this.projection = value;
                this.isDirty = true;
            }
        }

        /// <summary>
        /// View Matrix
        /// </summary>
        public Matrix4 View
        {
            get { return this.view; }
            set
            {
                this.view = value;
                this.isDirty = true;
            }
        }

        public Plane[ ] Planes
        {
            get
            {
                UpdateFrustum( );
                return this.planes;
            }
        }

        #endregion [ Public Properties ]

        public Frustum( Matrix4 projection, Matrix4 view )
        {
            this.projection = projection;
            this.view = view;
            this.isDirty = true;

            UpdateFrustum( );
        }

        public void UpdateFrustum( )
        {
            if ( this.isDirty )
                this.planes = GetFrustumPlanes( this.projection, this.view );
        }

        #region [ Private Methods ]

        /// <summary>
        /// Gets the six frustum planes from the projection and view matrices
        /// </summary>
        /// <param name="projection">Projection matrix</param>
        /// <param name="view">View matrix</param>
        /// <returns>Frustum planes that align with the current view and projection</returns>
        private static Plane[ ] GetFrustumPlanes( Matrix4 projection, Matrix4 view )
        {
            var matrix = ( view * projection ).M;

            var xVector = new Vector4( matrix[ 0 ], matrix[ 1 ], matrix[ 2 ], matrix[ 3 ] );
            var yVector = new Vector4( matrix[ 4 ], matrix[ 5 ], matrix[ 6 ], matrix[ 7 ] );
            var zVector = new Vector4( matrix[ 8 ], matrix[ 9 ], matrix[ 10 ], matrix[ 11 ] );
            var offset = new Vector4( matrix[ 12 ], matrix[ 13 ], matrix[ 14 ], matrix[ 15 ] );

            var planes = new Plane[ 6 ];

            planes[ ( int )FrustumPlane.Left ] = new Plane( offset + xVector );
            planes[ ( int )FrustumPlane.Right ] = new Plane( offset - xVector );
            planes[ ( int )FrustumPlane.Top ] = new Plane( offset - yVector );
            planes[ ( int )FrustumPlane.Bottom ] = new Plane( offset + yVector );
            planes[ ( int )FrustumPlane.Near ] = new Plane( zVector );
            planes[ ( int )FrustumPlane.Far ] = new Plane( offset - zVector );

            return planes;
        }
    
        #endregion [ Private Methods ]
    }
}
