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

        public static double[] xplane;

        public static double[] yplane;

        public static bool active;

        private static int current;

        private static double deg2rad = (Math.PI / 180.0);

        private static double rad2deg = (180.0 / Math.PI);

        // Constructor, which initializes the members to 0.
        static absBearing()
        {
            bearing = 0;
            mag = 0;
            current = 0;
            active = false;

            xplane = new double[10];
            yplane = new double[10];
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

        static public void clearBearings()
        {
            int i;

            for(i = 0; i < xplane.Length; i++)
            {
                xplane[i] = -1;
                yplane[i] = -1;
            }
        }

        /*
         * Function takes in the angle and magnitude and uses those to get the 
         * associated X and Y values.  Those are then stored in the array 
         * until it needs to be averaged.
         */
        static public void logDetectedXY(double angle, double mag)
        {
            double x;
            double y;

            // Must convert to radians for the trig functions.
            x = Math.Cos((angle * deg2rad)) * mag;
            y = Math.Sin((angle * deg2rad)) * mag;

            xplane[current] = x;
            yplane[current] = y;

            current++;
        }

        /*
         * Calculates the final average of the values stored in the arrays of
         * X ad Y values.
         */
        static public void averageBearings()
        {
            int i;

            double xAvg = 0.0;
            double yAvg = 0.0;
            double num = 0.0;
            double result = -1.0;

            // Adds all values for X and Y, then breaks when an empty spot is observed.
            for(i = 0; i < 100; i++)
            { 
                // -1 is empty.  Can break after since they are added in order.
                if(xplane[i] != -1)
                {
                    xAvg += xplane[i];
                    yAvg += yplane[i];
                    num++;
                }
                else
                {
                    break;
                }
            }

            // Now get the average.
            xAvg = (xAvg / num);
            yAvg = (yAvg / num);

            // Get the angle in radians and convert to degrees.
            result = (Math.Atan2(yAvg, xAvg)) * rad2deg;

            // Now set the bearing and mag to be the believed bearing and mag.
            bearing = result;
            mag = Math.Sqrt((xAvg * xAvg) + (yAvg * yAvg));
            current = 0;

            // Now at the end we clear the arrays for the next scan.
            clearBearings();
        }
    }

}
