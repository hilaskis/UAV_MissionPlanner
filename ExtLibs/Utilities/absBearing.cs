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

        // Array of the x-plane values of the detected signal.
        public static double[] xplane;

        // Array of the y-plane values of the detected signal.
        public static double[] yplane;

        // Flag to determine if a scan is in progress.
        public static bool active;

        // A count of how long the scan has been active - based off of a timer in FlightData.
        public static int activeCount;

        // Flag to determine is a signal was detected during a scan.
        public static bool foundSignal;

        // A count of the number of detections made during a scan.
        public static int current;

        // Conversion for degrees to radians.
        private static double deg2rad = (Math.PI / 180.0);

        // Conversion for radians to degrees.
        private static double rad2deg = (180.0 / Math.PI);

        // Size of the arrays where data is stored for averaging.
        private static int arraySize = 100;

        /* 
         * Constructor, which initializes the members to 0.
         */
        static absBearing()
        {
            bearing = 0;
            mag = 0;
            current = 0;
            active = false;
            foundSignal = false;
            activeCount = 0;

            xplane = new double[arraySize];
            yplane = new double[arraySize];

            // Set values in arrays to -1.
            clearBearings();
        }

        /*
         * Gets the bearing of the signal.
         */
        static public double getBearing()
        {
            return bearing;
        }

        /* 
         * Sets the bearing of the signal.
         */
        /*static public void setBearing(double newBearing)
        {
            bearing = newBearing;
        }*/

        /*
         * Gets the magnitude of the signal.
         */
        static public double getMag()
        {
            return mag;
        }

        /*
         * Set the magnitue of the detected signal.
         */
        /*static public void setMag(double newMag)
        {
            mag = newMag;
        }*/

        /*
         * Sets all values in the xplane a yplane arrays to -1. 
         */ 
        static public void clearBearings()
        {
            int i;

            for (i = 0; i < arraySize; i++)
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

            if (current <= arraySize)
            {
                // Check for an angle between 180 and 360 represented as -PI to just less than 0.  
                if((angle >= -Math.PI) && (angle < 0))
                {
                    // Convert to make angle 0 to 2PI.
                    angle = (Math.PI * 2) + angle;
                }

                // Get the magnitude for the x and y planes, then log into the arrays.
                x = Math.Cos((angle)) * Math.Abs(mag);
                y = Math.Sin((angle)) * Math.Abs(mag);
                xplane[current] = x;
                yplane[current] = y;

                // Increment array position tracker.
                current++;
            }
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

            // Clear the found flag, which remains cleared unless a signal was detected.
            foundSignal = false;

            activeCount = 0;
            current = 0;

            // Adds all values for X and Y, then breaks when an empty spot is observed.
            for (i = 0; i < arraySize; i++)
            { 
                // -1 is empty.  Can break after since they are added in order.
                if(xplane[i] != -1)
                {
                    xAvg += xplane[i];
                    yAvg += yplane[i];
                    num++;

                    // Set foundSignal flag since one was detected and logged.
                    foundSignal = true;
                }
                else
                {
                    break;
                }
            }

            // Only perform computations for the average if a signal was detected.
            if (num > 0.0)
            {
                // Now get the average.
                xAvg = (xAvg / num);
                yAvg = (yAvg / num);

                // Get the angle in radians and convert to degrees.
                result = (Math.Atan2(yAvg, xAvg)) * rad2deg;

                // Now set the bearing and mag to be the believed bearing and mag.
                bearing = result;
                mag = Math.Sqrt((xAvg * xAvg) + (yAvg * yAvg));
                current = 0;
            }

            // Now at the end we clear the arrays for the next scan.
            clearBearings();
        }
    }

}
