using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.Utilities
{
    public static class absBearing
    {

        public static double bearing;

        static absBearing()
        {
            bearing = 0;
        }

        static public double getBearing()
        {
            return bearing;
        }

        static public void setBearing(double newBearing)
        {
            bearing = newBearing;
        }
    }

}
