﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp
{
    // Trip-klassen repræsenterer en køretur
    public class Trip
    {
        public double Distance { get; private set; } // Turens længde i km
        public DateTime TripDate { get; private set; } // Dato for turen
        public DateTime StartTime { get; private set; } // Starttidspunkt
        public DateTime EndTime { get; private set; } // Sluttidspunkt
        public double LiterPrice { get; private set; } // Literpris for brændstof

        public Trip(double distance, DateTime tripDate, DateTime startTime, DateTime endTime, double literPrice)
        {
            Distance = distance;
            TripDate = tripDate;
            StartTime = startTime;
            EndTime = endTime;
            LiterPrice = literPrice;

            if (distance < 0) throw new ArgumentException("Distance må ikke være negativ");
            if (endTime < startTime) throw new ArgumentException("Sluttid må ikke være før starttid.");
            if (literPrice < 0) throw new ArgumentException("Literpris kan ikke være negativ.");
        }
        public override string ToString()
        {
            return $"{Distance};{TripDate:O};{StartTime:O};{EndTime:O};{LiterPrice}";
        }

        public static Trip FromString(string data)
        {
            string[] parts = data.Split(';');

            double distance = double.Parse(parts[0]);
            DateTime tripDate = DateTime.Parse(parts[1]);
            DateTime startTime = DateTime.Parse(parts[2]);
            DateTime endTime = DateTime.Parse(parts[3]);
            double literPrice = double.Parse(parts[4]);

            return new Trip(distance, tripDate, startTime, endTime, literPrice);
        }

        public TimeSpan CalculateDuration()
        {
            return EndTime - StartTime;
        }

        public double CalculateFuelUsed(double kmPerLiter)
        {
            if (kmPerLiter <= 0)
                throw new ArgumentException("KmPerLiter skal være større end 0.");
            return Distance / kmPerLiter;
        }

        public double CalculateTripPrice(double kmPerLiter)
        {
            return CalculateFuelUsed(kmPerLiter) * LiterPrice;
        }

        // Hjælpemetoder til nem adgang
        public double GetCost(double kmPerLiter)
        {
            return CalculateTripPrice(kmPerLiter);
        }

        public double GetFuelUsed(double kmPerLiter)
        {
            return CalculateFuelUsed(kmPerLiter);
        }

        public void PrintTripDetails(double kmPerLiter)
        {
            Console.WriteLine("\n--- Turdetaljer ---");
            Console.WriteLine($"Dato: {TripDate:dd-MM-yyyy}");
            Console.WriteLine($"Starttid: {StartTime}");
            Console.WriteLine($"Sluttid: {EndTime}");
            Console.WriteLine($"Varighed: {CalculateDuration()}");
            Console.WriteLine($"Distance: {Distance} km");
            Console.WriteLine($"Brændstofforbrug: {CalculateFuelUsed(kmPerLiter):F2} liter");
            Console.WriteLine($"Literpris: {LiterPrice:F2} kr");
            Console.WriteLine($"Pris: {CalculateTripPrice(kmPerLiter):F2} kr\n");
        }
    }
}
