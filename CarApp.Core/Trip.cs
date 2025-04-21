using System;
using System.Collections.Generic;
using System.Globalization;
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
        public double FuelPrice { get; private set; } // Literpris

        public Trip(double distance, DateTime tripDate, DateTime startTime, DateTime endTime, double fuelPrice)
        {
            Distance = distance;
            TripDate = tripDate;
            StartTime = startTime;
            EndTime = endTime;
            FuelPrice = fuelPrice;
        }
        public string ToFormattedString()
        {
            // Format: Trip: Distance; dd-MM-yyyy; StartTime; EndTime; FuelPrice
            return $"Trip: {Distance}; {TripDate:dd-MM-yyyy}; {StartTime:dd-MM-yyyy HH:mm}; {EndTime:dd-MM-yyyy HH:mm}; {FuelPrice.ToString("F2", CultureInfo.InvariantCulture)}";
        }

        public static Trip FromFormattedString(string line)
        {
            var parts = line.Substring(6).Split(';');

            double distance = double.Parse(parts[0].Trim());
            DateTime tripDate = DateTime.ParseExact(parts[1].Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            DateTime start = DateTime.ParseExact(parts[2].Trim(), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(parts[3].Trim(), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            double fuelPrice = double.Parse(parts[4].Trim().Replace(",", "."), CultureInfo.InvariantCulture);

            return new Trip(distance, tripDate, start, end, fuelPrice);
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

        public double CalculateTripPrice(double kmPerLiter, double literPrice)
        {
            return CalculateFuelUsed(kmPerLiter) * literPrice;
        }

        // Hjælpemetoder til nem adgang
        public double GetCost(double kmPerLiter, double fuelPrice)
        {
            return CalculateTripPrice(kmPerLiter, fuelPrice);
        }

        public void PrintTripDetails(double kmPerLiter, double fuelPrice)
        {
            Console.WriteLine("\n--- Turdetaljer ---");
            Console.WriteLine($"Dato: {TripDate:dd-MM-yyyy}");
            Console.WriteLine($"Starttid: {StartTime}");
            Console.WriteLine($"Sluttid: {EndTime}");
            Console.WriteLine($"Varighed: {CalculateDuration()}");
            Console.WriteLine($"Distance: {Distance} km");
            Console.WriteLine($"Brændstofforbrug: {CalculateFuelUsed(kmPerLiter):F2} liter");
            Console.WriteLine($"Literpris: {fuelPrice:F2} kr");
            Console.WriteLine($"Pris: {CalculateTripPrice(kmPerLiter, fuelPrice):F2} kr\n");
        }
    }
}
