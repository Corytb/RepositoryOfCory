using System;
using System.Collections.Generic;
using System.IO;

namespace GarminGpsScript
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\coryt\Downloads\2022-10-23-10-53-19.csv";      //filepath for the CSV file
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                List<string> listA = new List<string>();
                var counter = 0;
                var remainder = 0;
                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    foreach (var item in values)
                    {
                        listA.Add(item);
                    }

                    remainder = counter % 10; //optionally finds every 10th data point to thin out the data

                    if (values[1] == "12" && remainder == 0) //number "12" is the prefix denoting a line containing location data; if "remainder == 0" this is the 10th point from the last one
                    {
                        var latString = values[7];  //gets the lat-long string values
                        var longString = values[10];

                        latString = latString.Replace("\"", "");    //removes some leading quotation marks from each lat/long string
                        longString = longString.Replace("\"", "");  

                        float latSemiCircle = float.Parse(latString);  //converts the string to float
                        float longSemiCircle = float.Parse(longString);

                        var latDegrees = latSemiCircle * (180 / Math.Pow(2,31));    // converts the "semicircle" location to degrees
                        var longDegrees = longSemiCircle * (180 / Math.Pow(2,31));

                        Console.WriteLine($"{latDegrees}, {longDegrees}");   //prints the final lat-long pair
                    }
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
            Console.ReadLine();
        }
    }
}
