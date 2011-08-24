using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Life.Core.Platform
{
	public class DisplayCapabilities
    {
        #region [ Private Members ]

        private readonly bool isPrimary;
        private readonly Rectangle bounds;
        private readonly int refreshRate;
        private readonly int bitsPerPixel;

        private readonly IEnumerable<DisplayResolution> resolutions;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public bool IsPrimary
        {
            get { return isPrimary; }
        }

        public Rectangle Bounds
        {
            get { return this.bounds; }
        }

        public int RefreshRate
        {
            get { return this.refreshRate; }
        }

        public int BitsPerPixel
        {
            get { return this.bitsPerPixel; }
        }

        public IEnumerable<DisplayResolution> Resolutions
        {
            get { return this.resolutions; }
        }

        #endregion [ Public Properties ]

        public DisplayCapabilities( bool isPrimary, Rectangle bounds, 
            int refreshRate, int bitsPerPixel, IEnumerable<DisplayResolution> resolutions )
        {
            this.isPrimary = isPrimary;
            this.bounds = bounds;
            this.refreshRate = refreshRate;
            this.bitsPerPixel = bitsPerPixel;

            this.resolutions = resolutions.ToList( );
        }
    }

}

