using System;
using System.Collections.Generic;
using System.IO;

namespace CarApp
{
    public class DataHandler
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\cars.txt");

        // Gemmer alle biler og deres ture i én fil
        public static void SaveCars(List<Car> cars)
        {
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
            List<Car> cars = new List<Car>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Filen findes ikke.");
                return cars;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    cars.Add(Car.FromString(line));
                }
            }

            Console.WriteLine("Alle biler er indlæst fra fil.");
            return cars;
        }
    }
}