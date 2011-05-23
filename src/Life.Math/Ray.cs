using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Math
{
    public class Ray
    {
        #region [ Public Properties ]

        /// <summary>
        /// Ray origin
        /// </summary>
        public readonly Vector3 Origin;

        /// <summary>
        /// Direction of ray
        /// </summary>
        public readonly Vector3 Direction;

        #endregion [ Public Properties ]

        public Ray( Vector3 origin, Vector3 direction )
        {
            this.Origin = origin;
            this.Direction = direction;
        }
    }
}
