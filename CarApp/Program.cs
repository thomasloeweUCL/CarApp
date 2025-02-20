using System;
using System.Collections.Generic;

namespace CarApp
{
    class Car
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public double Odometer { get; private set; }
        public double KmPerLiter { get; private set; }
        private bool isEngineOn;

        // Metode til at indlæse bilens oplysninger
        public void ReadCarDetails()
        {
            Console.Write("Indtast mærke: ");
            Brand = Console.ReadLine();
            Console.Write("Indtast model: ");
            Model = Console.ReadLine();
            Console.Write("Indtast årgang: ");
            Year = int.Parse(Console.ReadLine());
            Console.Write("Indtast km-tilstand: ");
            Odometer = double.Parse(Console.ReadLine());
            Console.Write("Indtast km per liter: ");
            KmPerLiter = double.Parse(Console.ReadLine());
        }

        // Metode til at simulere en køretur
        public void Drive(double distance)
        {
            if (isEngineOn)
            {
                Odometer += distance;
                Console.WriteLine($"Bilen har kørt {distance} km. Ny odometer: {Odometer}");
            }
            else
            {
                Console.WriteLine("Motoren er slukket! Tænd den først.");
            }
        }

        // Beregner prisen for en tur
        public double CalculateTripPrice(double distance, double literPrice)
        {
            if (KmPerLiter == 0)
            {
                Console.WriteLine("Fejl: KmPerLiter er 0. Udfør korrektion.");
                return 0;
            }
            return (distance / KmPerLiter) * literPrice;
        }

        // Tjekker om odometerstanden er et palindrom
        public bool IsPalindrome()
        {
            string kmStr = Odometer.ToString();
            int len = kmStr.Length;

            for (int i = 0; i < len / 2; i++)
            {
                if (kmStr[i] != kmStr[len - 1 - i])
                    return false;
            }
            return true;
        }

        // Udskriver bilens oplysninger
        public void PrintCarDetails()
        {
            Console.WriteLine($"\nBil detaljer:\nMærke: {Brand}\nModel: {Model}\nÅrgang: {Year}\nOdometer: {Odometer} km\nKm/l: {KmPerLiter}\n");
        }

        // Skifter motorens tilstand
        public void ToggleEngine()
        {
            isEngineOn = !isEngineOn;
            Console.WriteLine(isEngineOn ? "Motoren er nu tændt." : "Motoren er nu slukket.");
        }
    }

    class Program
    {
        static List<Car> teamCars = new List<Car>();

        static void Main()
        {
            Car myCar = new Car();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nVælg en handling:\n");
                Console.WriteLine("1. Read Car Details");
                Console.WriteLine("2. Drive");
                Console.WriteLine("3. Calculate Trip Price");
                Console.WriteLine("4. IsPalindrome");
                Console.WriteLine("5. Print Car Details");
                Console.WriteLine("6. Print All Team Cars");
                Console.WriteLine("7. Afslut programmet");
                Console.WriteLine("8. Tænd/sluk motoren\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Car newCar = new Car(); // Opret en ny bil
                        newCar.ReadCarDetails();
                        teamCars.Add(newCar); // Tilføj den nye bil til listen
                        int carIndex = teamCars.Count - 1; // Hent bilens index i listen
                        Console.WriteLine($"Bilen er tilføjet! Bilens nummer: {carIndex}");
                        break;

                    case "2":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int selectedCarIndexForDrive = int.Parse(Console.ReadLine()); // Brugeren vælger bil

                        if (selectedCarIndexForDrive < 0 || selectedCarIndexForDrive >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForDrive = teamCars[selectedCarIndexForDrive]; // Vælg den bil, der er valgt af brugeren

                        Console.Write("Indtast distance i km: ");
                        double distanceToDrive = double.Parse(Console.ReadLine());

                        // Opdater bilens odometer
                        selectedCarForDrive.Drive(distanceToDrive);

                        break;

                    case "3":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int carIndexForTrip = int.Parse(Console.ReadLine()); // Ændret variabelnavn her

                        if (carIndexForTrip < 0 || carIndexForTrip >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car tripCar = teamCars[carIndexForTrip]; // Brug den nye variabel her

                        Console.Write("Indtast distance i km: ");
                        double tripDistance = double.Parse(Console.ReadLine());

                        Console.Write("Indtast literpris: ");
                        double literPrice = double.Parse(Console.ReadLine());

                        double price = tripCar.CalculateTripPrice(tripDistance, literPrice);

                        if (price > 0)
                            Console.WriteLine($"Turen koster: {price:F2} kr.");
                        break;

                    case "4":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int selectedCarIndexForPalindrome = int.Parse(Console.ReadLine()); // Ændret variabelnavn her

                        if (selectedCarIndexForPalindrome < 0 || selectedCarIndexForPalindrome >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForPalindrome = teamCars[selectedCarIndexForPalindrome]; // Brug den nye variabel her
                        Console.WriteLine(selectedCarForPalindrome.IsPalindrome() ? "Odometer er et palindrom!" : "Odometer er ikke et palindrom.");
                        break;

                    case "5":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int selectedCarIndexForPrint = int.Parse(Console.ReadLine()); // Ændret variabelnavn her

                        if (selectedCarIndexForPrint < 0 || selectedCarIndexForPrint >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForPrint = teamCars[selectedCarIndexForPrint]; // Brug den nye variabel her
                        selectedCarForPrint.PrintCarDetails();
                        break;

                    case "6":
                        PrintAllTeamCars();
                        break;

                    case "7":
                        running = false;
                        Console.WriteLine("Programmet afsluttes...");
                        break;

                    case "8":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int selectedCarIndexForEngineToggle = int.Parse(Console.ReadLine()); // Brugeren vælger bil

                        if (selectedCarIndexForEngineToggle < 0 || selectedCarIndexForEngineToggle >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForEngineToggle = teamCars[selectedCarIndexForEngineToggle]; // Vælg den bil, der er valgt af brugeren

                        // Tænd eller sluk motoren på den valgte bil
                        selectedCarForEngineToggle.ToggleEngine();
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen.");
                        break;
                }
            }
        }

        // Udskriver alle teamets biler
        static void PrintAllTeamCars()
        {
            if (teamCars.Count == 0)
            {
                Console.WriteLine("Der er ingen biler i teamet.");
                return;
            }

            Console.WriteLine("\nAlle teamets biler:");
            foreach (var car in teamCars)
            {
                car.PrintCarDetails();
            }
        }
    }
}
