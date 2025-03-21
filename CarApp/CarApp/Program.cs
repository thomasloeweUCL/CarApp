using System;
using System.Collections.Generic;

namespace CarApp
{
    class Program
    {
        static List<Car> teamCars = new List<Car>();

        static void AddNewCar()
        {
            Console.Write("Indtast mærke: ");
            string brand = Console.ReadLine();

            Console.Write("Indtast model: ");
            string model = Console.ReadLine();

            int year;
            while (true)
            {
                Console.Write("Indtast årgang: ");
                if (int.TryParse(Console.ReadLine(), out year)) break;
                Console.WriteLine("Ugyldig årgang. Prøv igen.");
            }

            // Vis brændstoftyper dynamisk
            FuelType[] fuelTypes = (FuelType[])Enum.GetValues(typeof(FuelType));
            int fuelChoice;

            while (true)
            {
                Console.WriteLine("Vælg brændstoftype:");
                for (int i = 0; i < fuelTypes.Length; i++)
                {
                    Console.WriteLine($"{i}: {fuelTypes[i]}");
                }

                Console.Write("Indtast nummer for brændstoftype: ");
                if (int.TryParse(Console.ReadLine(), out fuelChoice) && fuelChoice >= 0 && fuelChoice < fuelTypes.Length)
                    break;

                Console.WriteLine("Ugyldigt valg. Prøv igen.");
            }

            FuelType fuel = fuelTypes[fuelChoice];

            double odometer;
            while (true)
            {
                Console.Write("Indtast kilometerstand: ");
                if (double.TryParse(Console.ReadLine(), out odometer)) break;
                Console.WriteLine("Ugyldig kilometerstand. Prøv igen.");
            }

            double kmPerLiter;
            while (true)
            {
                Console.Write("Indtast km/l: ");
                if (double.TryParse(Console.ReadLine(), out kmPerLiter)) break;
                Console.WriteLine("Ugyldig km/l-værdi. Prøv igen.");
            }

            Car newCar = new Car(brand, model, year, fuel, odometer, kmPerLiter);
            teamCars.Add(newCar);
            Console.WriteLine($"Bilen er tilføjet! Bilens nummer: {teamCars.Count - 1}");
        }


        static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nVælg en handling:");
                Console.WriteLine("1. Tilføj ny bil");
                Console.WriteLine("2. Registrér køretur");
                Console.WriteLine("3. Beregn turpris");
                Console.WriteLine("4. Tjek om Odometer er palindrom");
                Console.WriteLine("5. Vis bilens detaljer");
                Console.WriteLine("6. Vis alle biler");
                Console.WriteLine("7. Afslut programmet");
                Console.WriteLine("8. Tænd/sluk motoren");
                Console.WriteLine("9. Vis alle ture for en bil");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewCar();
                        break;
                    case "2":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForTripRegistration = int.Parse(Console.ReadLine());

                        if (selectedCarIndexForTripRegistration < 0 || selectedCarIndexForTripRegistration >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForTripRegistration = teamCars[selectedCarIndexForTripRegistration];

                        Console.Write("Distance i km: ");
                        double tripDistance = double.Parse(Console.ReadLine());

                        Console.Write("Dato (dd-MM-yyyy): ");
                        DateTime tripDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);

                        Console.Write("Starttid (fx 08:00): ");
                        DateTime tripStartTime = DateTime.Parse($"{tripDate:dd-MM-yyyy} {Console.ReadLine()}");

                        Console.Write("Sluttid (fx 09:30): ");
                        DateTime tripEndTime = DateTime.Parse($"{tripDate:dd-MM-yyyy} {Console.ReadLine()}");

                        Console.Write("Indtast literpris (kr): ");
                        double tripLiterPrice = double.Parse(Console.ReadLine());

                        Trip trip = new Trip(tripDistance, tripDate, tripStartTime, tripEndTime, tripLiterPrice);
                        selectedCarForTripRegistration.Drive(trip);
                        break;

                    case "3":
                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForPriceCalculation = int.Parse(Console.ReadLine());
                        Car selectedCarForPriceCalculation = teamCars[selectedCarIndexForPriceCalculation];
                        Console.Write("Distance i km: ");
                        double priceCalculationDistance = double.Parse(Console.ReadLine());
                        Console.Write("Literpris (kr): ");
                        double priceCalculationLiterPrice = double.Parse(Console.ReadLine());
                        double fuelUsed = priceCalculationDistance / selectedCarForPriceCalculation.KmPerLiter;
                        double price = fuelUsed * priceCalculationLiterPrice;
                        Console.WriteLine($"Forbrug: {fuelUsed:F2} liter – Pris: {price:F2} kr");
                        break;

                    case "4":
                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForPalindromeCheck = int.Parse(Console.ReadLine());
                        Console.WriteLine(teamCars[selectedCarIndexForPalindromeCheck].IsPalindrome() ? "Odometer er et palindrom." : "Ikke et palindrom.");
                        break;

                    case "5":
                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForDetails = int.Parse(Console.ReadLine());
                        teamCars[selectedCarIndexForDetails].PrintCarDetails();
                        break;

                    case "6":
                        foreach (var carInTeamList in teamCars)
                            carInTeamList.PrintCarDetails();
                        break;

                    case "7":
                        running = false;
                        break;

                    case "8":
                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForEngineToggle = int.Parse(Console.ReadLine());
                        teamCars[selectedCarIndexForEngineToggle].ToggleEngine();
                        break;

                    case "9":
                        Console.Write("Bilnummer: ");
                        int selectedCarIndexForTripListing = int.Parse(Console.ReadLine());

                        if (selectedCarIndexForTripListing < 0 || selectedCarIndexForTripListing >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForTripListing = teamCars[selectedCarIndexForTripListing];
                        if (selectedCarForTripListing.Trips.Count == 0)
                        {
                            Console.WriteLine("Ingen ture registreret.");
                            break;
                        }

                        foreach (var tripInTripList in selectedCarForTripListing.Trips)
                        {
                            tripInTripList.PrintTripDetails(selectedCarForTripListing.KmPerLiter);
                        }
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        break;
                }
            }
        }
    }
}
