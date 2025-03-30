using System;
using System.Collections.Generic;

namespace CarApp
{
    // Repræsenterer en bilejer, der kan eje flere biler.
    public class CarOwner
    {
        // Ejers navn.
        public string Name { get; private set; }

        // Liste over biler, som ejeren ejer.
        public List<Car> Cars { get; private set; }

        // Opretter en ny CarOwner med angivet navn.
        public CarOwner(string name)
        {
            Name = name;
            Cars = new List<Car>();
        }

        // Tilføjer en bil til ejerens liste.
        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        // Viser alle ture for alle ejerens biler.
        public void PrintAllTrips()
        {
            Console.WriteLine($"Ture for {Name}:");
            foreach (var car in Cars)
            {
                Console.WriteLine($"\n{car.Brand} {car.Model}:");

                foreach (var trip in car.Trips)
                {
                    Console.WriteLine($" - {trip.TripDate.ToShortDateString()}, {trip.Distance} km, Pris: {trip.GetCost(car.KmPerLiter):N2} kr");
                }
            }
        }

        // Udregner den samlede pris for alle ejerens ture.
        public double GetTotalCost()
        {
            double total = 0;
            foreach (var car in Cars)
            {
                foreach (var trip in car.Trips)
                {
                    total += trip.CalculateTripPrice(car.KmPerLiter);
                }
            }
            return total;
        }
    }
}
