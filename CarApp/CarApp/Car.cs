using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp
{
    // Car-klassen repræsenterer en bil og indeholder dens køreture
    class Car
    {
        public string Brand { get; private set; } // Bilens mærke
        public string Model { get; private set; } // Bilens model
        public int Year { get; private set; } // Bilens årgang
        public FuelType Fuel { get; private set; } // Bilens brændstoftype
        public double Odometer { get; private set; } // Kilometerstand
        public double KmPerLiter { get; private set; } // Brændstofeffektivitet (km pr. liter)
        public List<Trip> Trips { get; private set; } // Liste over alle registrerede ture
        private bool isEngineOn; // Angiver om motoren er tændt eller slukket

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

        private void UpdateOdometer(double distance)
        {
            Odometer += distance;
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
            double tripCost = fuelUsed * trip.LiterPrice;

            Trips.Add(trip); // Registrer turen
            Odometer += trip.Distance; // Opdater odometer

            Console.WriteLine("\nTur gennemført:");
            Console.WriteLine($"Distance: {trip.Distance} km");
            Console.WriteLine($"Brændstofforbrug: {fuelUsed:F2} liter");
            Console.WriteLine($"Pris: {tripCost:F2} kr");
            Console.WriteLine($"Ny kilometertæller: {Odometer} km");
        }
    }
}
