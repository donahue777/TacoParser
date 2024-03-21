namespace LoggingKata
{
    // Parses a POI file to locate all the Taco Bells
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            //Line.Split(',') splits it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Error, array length is less than three");
                return null; 
            }

            //Grabs the latitude from array at index 0 then parses string as a double
            double latitude = double.Parse(cells[0]);
            
            
            //Grabs the longitude from array at index 1
            double longitude = double.Parse(cells[1]);
            
            //Grabs the name from array at index 2
            string name = cells[2];

            // TODO: Create a TacoBell class
            // that conforms to ITrackable

            //Created an instance of the Point Struct
            //Set the values of latitude and longitude to the Point struct
            Point point = new Point();
            point.Latitude = latitude;
            point.Longitude = longitude;

            //Created an instance of the TacoBell class
            //Set the values of the TacoBell class
            TacoBell tacoBell = new TacoBell();
            tacoBell.Name = name;
            tacoBell.Location = point;

            return tacoBell;
        }
    }
}
