using System;
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
            return $"Trip: {Distance}; {TripDate:dd-MM-yyyy}; {StartTime:HH:mm}; {EndTime:HH:mm}; {FuelPrice}";
        }

        public static Trip FromFormattedString(string line)
        {
            var data = line.Replace("Trip: ", "").Split(';');

            double distance = double.Parse(data[0].Trim(), System.Globalization.CultureInfo.InvariantCulture);
            DateTime date = DateTime.ParseExact(data[1].Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime start = DateTime.ParseExact(data[2].Trim(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(data[3].Trim(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            double fuelPrice = double.Parse(data[4].Trim(), System.Globalization.CultureInfo.InvariantCulture);

            // Kombinér dato og tidspunkt
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, start.Hour, start.Minute, 0);
            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, end.Hour, end.Minute, 0);

            return new Trip(distance, date, startTime, endTime, fuelPrice);
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
