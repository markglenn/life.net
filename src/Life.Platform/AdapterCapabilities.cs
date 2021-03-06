using System;
using System.Collections.Generic;

namespace Life.Platform
{
    public class AdapterCapabilities
    {
        #region [ Private Members ]

        private readonly IEnumerable<DisplayCapabilities> displays;

        #endregion [ Private Members ]

        #region [ Public Properties ]

        public IEnumerable<DisplayCapabilities> Displays
        {
            get { return this.displays; }
        }

        #endregion [ Public Properties ]

        public AdapterCapabilities( IEnumerable<DisplayCapabilities> displays )
        {
            this.displays = displays;
        }
    }
}

