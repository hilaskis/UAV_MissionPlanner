using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MissionPlanner.Utilities
{
    public static class absBearing
    {
        // Absolute bearing of the signal.
        public static double bearing;

        // Magnitude of the signal.
        public static double mag;

        // Constructor, which initializes the members to 0.
        static absBearing()
        {
            bearing = 0;
            mag = 0;
        }

        // Gets the bearing of the signal.
        static public double getBearing()
        {
            return bearing;
        }

        // Sets the bearing of the signal.
        static public void setBearing(double newBearing)
        {
            bearing = newBearing;
        }

        // Gets the magnitude of the signal.
        static public double getMag()
        {
            return mag;
        }

        // Set the magnitue of the detected signal.
        static public void setMag(double newMag)
        {
            mag = newMag;
        }
    }

}
