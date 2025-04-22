using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CarApp
{
    public class DataHandler
    {
        // Sti til datamappen
        private static readonly string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
        private static string filePath;

        // Sætter den interne sti til datafilen
        public static void SetFilePath(string path)
        {
            filePath = path;
        }

        // Gemmer alle ejere, deres biler og ture i tekstfilen
        public static void SaveCars(List<CarOwner> owners)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var owner in owners)
                {
                    writer.WriteLine($"# Owner: {owner.Name}");

                    foreach (var car in owner.Cars)
                    {
                        writer.WriteLine(car.ToFormattedString());

                        foreach (var trip in car.Trips)
                        {
                            writer.WriteLine(trip.ToFormattedString());
                        }
                    }
                }
            }

            Console.WriteLine("Alle biler og ejere er gemt i filen.");
        }

        // Indlæser biler og ejere fra tekstfilen. Returnerer biler og sætter ejere via out-parameter
        public static List<Car> LoadCars(out List<CarOwner> owners)
        {
            var cars = new List<Car>();
            var ownerDict = new Dictionary<string, CarOwner>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Filen findes ikke.");
                owners = new List<CarOwner>();
                return cars;
            }

            Car currentCar = null;
            CarOwner currentOwner = null;

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("# Owner:"))
                {
                    var name = line.Replace("# Owner:", "").Trim();
                    if (!ownerDict.ContainsKey(name))
                        ownerDict[name] = new CarOwner(name);

                    currentOwner = ownerDict[name];
                }
                else if (line.StartsWith("# Car:"))
                {
                    currentCar = Car.FromFormattedString(line, currentOwner); // currentOwner kan være null
                    cars.Add(currentCar);
                }
                else if (line.StartsWith("Trip:") && currentCar != null)
                {
                    var trip = Trip.FromFormattedString(line);
                    currentCar.Trips.Add(trip);
                }
            }

            owners = new List<CarOwner>(ownerDict.Values);

            Console.WriteLine("Alle biler og ejere er indlæst fra fil.");
            return cars;
        }
        public static void SaveAll(List<Car> allCars, List<CarOwner> owners)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using StreamWriter writer = new StreamWriter(filePath)
            {
                AutoFlush = true
            };

            // Først: Gem alle biler uden ejer
            foreach (var car in allCars)
            {
                if (car.Owner == null)
                {
                    writer.WriteLine(car.ToFormattedString());

                    foreach (var trip in car.Trips)
                    {
                        writer.WriteLine(trip.ToFormattedString());
                    }
                }
            }

            // Dernæst: Gem ejere og deres biler
            foreach (var owner in owners)
            {
                writer.WriteLine($"# Owner: {owner.Name}");

                foreach (var car in owner.Cars)
                {
                    writer.WriteLine(car.ToFormattedString());

                    foreach (var trip in car.Trips)
                    {
                        writer.WriteLine(trip.ToFormattedString());
                    }
                }
            }
        }

    }

}
