using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp
{
    // Car-klassen repræsenterer en bil og indeholder dens køreture
    public class Car
    {
        public string Brand { get; private set; } // Bilens mærke
        public string Model { get; private set; } // Bilens model
        public int Year { get; private set; } // Bilens årgang
        public FuelType Fuel { get; private set; } // Bilens brændstoftype
        public double Odometer { get; private set; } // Kilometerstand
        public double KmPerLiter { get; private set; } // Brændstofeffektivitet (km pr. liter)
        public List<Trip> Trips { get; private set; } // Liste over alle registrerede ture
        public CarOwner Owner { get; set; } // Reference til ejeren af bilen
        private bool isEngineOn; // Angiver om motoren er tændt eller slukket

        public bool EngineIsRunning => isEngineOn; // Offentlig adgang til motorstatus (kun læsning)

        // Konstruktør
        public Car(string brand, string model, int year, FuelType fuel, double odometer, double kmPerLiter)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Fuel = fuel;
            Odometer = odometer;
            KmPerLiter = kmPerLiter;
            isEngineOn = false;
            Trips = new List<Trip>();
        }

        public bool IsPalindrome()
        {
            string odometerAsString = Odometer.ToString("F0");
            int length = odometerAsString.Length;
            for (int i = 0; i < length / 2; i++)
            {
                if (odometerAsString[i] != odometerAsString[length - 1 - i])
                    return false;
            }
            return true;
        }

        public void PrintCarDetails()
        {
            Console.WriteLine($"\nBil detaljer:\nMærke: {Brand}\nModel: {Model}\nÅrgang: {Year}\nBrændstoftype: {Fuel}\nOdometer: {Odometer} km\nKm/l: {KmPerLiter}\n");
        }

        public void ToggleEngine()
        {
            isEngineOn = !isEngineOn;
            Console.WriteLine(isEngineOn ? "Motoren er nu tændt." : "Motoren er nu slukket.");
        }

        public void Drive(Trip trip)
        {
            if (!isEngineOn)
            {
                Console.WriteLine("Motoren er slukket! Du skal tænde motoren før du kan køre.");
                return;
            }

            if (trip == null)
            {
                Console.WriteLine("Fejl: Turen er ugyldig.");
                return;
            }

            double fuelUsed = trip.Distance / KmPerLiter;
            double tripCost = fuelUsed * trip.FuelPrice;

            Trips.Add(trip); // Registrer turen
            Odometer += trip.Distance; // Opdater odometer

            Console.WriteLine("\nTur gennemført:");
            Console.WriteLine($"Distance: {trip.Distance} km");
            Console.WriteLine($"Brændstofforbrug: {fuelUsed:F2} liter");
            Console.WriteLine($"Pris: {tripCost:F2} kr");
            Console.WriteLine($"Ny kilometertæller: {Odometer} km");
        }
        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Owner != null)
                builder.AppendLine($"# Owner: {Owner.Name}");

            builder.AppendLine($"# Car: {Brand}; {Model}; {Year}; {Fuel}; {Odometer.ToString(CultureInfo.InvariantCulture)}; {KmPerLiter.ToString(CultureInfo.InvariantCulture)}");

            foreach (var trip in Trips)
                builder.AppendLine(trip.ToFormattedString());

            return builder.ToString();
        }
        public static Car FromFormattedString(string line, CarOwner owner)
        {
            var data = line.Replace("# Car: ", "").Split(';');
            string brand = data[0].Trim();
            string model = data[1].Trim();
            int year = int.Parse(data[2].Trim());
            FuelType fuel = Enum.Parse<FuelType>(data[3].Trim());
            double odometer = double.Parse(data[4].Trim(), CultureInfo.InvariantCulture);
            double kmPerLiter = double.Parse(data[5].Trim(), CultureInfo.InvariantCulture);

            var car = new Car(brand, model, year, fuel, odometer, kmPerLiter);
            car.Owner = owner;
            return car;
        }
    }
}
