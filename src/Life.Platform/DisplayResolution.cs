using System;
using System.Drawing;

namespace Life.Platform
{
	public class DisplayResolution
    {
        #region [ Private Members ]

        private readonly Size bounds;
        private readonly int bitsPerPixel;
        private readonly int refreshRate;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public Size Bounds
        {
            get { return this.bounds; }
        }

        public int BitsPerPixel
        {
            get { return this.bitsPerPixel; }
        }

        public int RefreshRate
        {
            get { return this.refreshRate; }
        }

        #endregion [ Public Properties ]

        public DisplayResolution( Size bounds, int bitsPerPixel, int refreshRate )
        {
            this.bounds = bounds;
            this.bitsPerPixel = bitsPerPixel;
            this.refreshRate = refreshRate;
        }
    }
		
}

