using System;
using Life.Math;
using Life.Math.Bounds;

namespace Life.Core
{
    public class Camera : Frustum
    {
        #region [ Private Members ]

        private Quaternion rotation;
        private Vector3 position;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        /// <summary>
        /// Position of the camera
        /// </summary>
        public Vector3 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                this.View = GetViewMatrix( this.position, this.rotation );
            }
        }

        /// <summary>
        /// Rotation of the camera
        /// </summary>
        public Quaternion Rotation
        {
            get { return this.rotation; }
            set 
            { 
                this.rotation = value;
                this.View = GetViewMatrix( this.position, this.rotation );
            }
        }

        #endregion [ Public Properties ]

        public Camera( Matrix4 projection, Vector3 position, Quaternion rotation ) 
            : base( projection, GetViewMatrix( position, rotation ) )
        {
            this.position = position;
            this.rotation = rotation;
        }

        public Frustum FrustomToModelSpace( Matrix4 matrix )
        {
            return new Frustum( this.Projection, matrix * this.View );    
        }

        public static Matrix4 GetViewMatrix( Vector3 position, Quaternion rotation )
        {
            return Matrix4.Translation( -position ) * rotation.ToMatrix4( );
        }

        public void TranslateRelative( Vector3 direction, float distance )
        {
            var vector = this.rotation.RotateVector( direction );
            this.Position += vector * distance;
        }
    }

}

