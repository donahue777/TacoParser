using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            //File.ReadAllLines(path) grabs all the lines from csv file. 
            var lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                logger.LogError("There's nothing there..");
            }
            if (lines.Length == 1)
            {
                logger.LogWarning("There's one line left!");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();
            //Parses every line in lines collection to ITrackable
            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double distance = 0;

            //Collects our latitude and longitude referenced from the ITrackable interface
            //and calculates the locations from the info provided with the GeroCoordinate
            //latitude and longitude methods.
            for(int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate();

                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                //The location from locA at index 0 gets compared to every
                //location of the list in locB and uses the GetDistanceTo() method
                //to calculate the distance between them all.
                for(int k = 0; k < locations.Length; k++)
                {
                    ITrackable locB = locations[k];
                    GeoCoordinate corB = new GeoCoordinate();

                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    if(corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }

            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart.");

        }
    }
}
