using System;
using System.Collections.Generic;
using System.IO;

namespace CarApp
{
    public class DataHandler
    {
        // Læser cars.txt fra mappen "Data"
        private static readonly string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
        private static string filePath;

        // Gemmer alle biler og deres ture i én fil
        public static void SaveCars(List<Car> cars)
        {
            // Tjek om mappen findes, ellers oprettes ny mappe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var car in cars)
                {
                    writer.WriteLine(car.ToString());
                }
            }
            Console.WriteLine("Alle biler er gemt i filen.");
        }

        // Indlæser alle biler og deres ture fra én fil
        public static List<Car> LoadCars()
        {
            var cars = new List<Car>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Filen findes ikke.");
                return cars; // tom liste
            }

            Car currentCar = null;
            CarOwner currentOwner = null;
            var owners = new Dictionary<string, CarOwner>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("# Owner:"))
                {
                    var name = line.Replace("# Owner:", "").Trim();
                    if (!owners.ContainsKey(name))
                        owners[name] = new CarOwner(name);
                    currentOwner = owners[name];
                }
                else if (line.StartsWith("# Car:") && currentOwner != null)
                {
                    currentCar = Car.FromFormattedString(line, currentOwner);
                    cars.Add(currentCar);
                }
                else if (line.StartsWith("Trip:") && currentCar != null)
                {
                    var trip = Trip.FromFormattedString(line);
                    currentCar.Trips.Add(trip);
                }
            }

            Console.WriteLine("Alle biler og ejere er indlæst fra fil.");
            return cars;
        }
        public static void SetFilePath(string path)
        {
            filePath = path;
        }

    }
}