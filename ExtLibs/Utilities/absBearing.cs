using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.Utilities
{
    /// <summary>
    /// Struct of a bearing object.
    /// </summary>
    public struct absBearing
    {
        public absBearing Set(UInt16 bearing)
        {
            this.bearing = bearing;

            return this;
        }

        public UInt16 bearing;  // Bearing received from the craft.
    };
}
