using System;
using System.Collections.Generic;

// Namespace bruges til at organisere og gruppere relaterede klasser, så navnekonflikter undgås, og koden bliver mere struktureret.
namespace CarApp
{
    // Class er en skabelon, som bruges til at oprette objekter.
    // Konsolapplikationen er opdelt i class Car og class Program.
    // class Car håndterer alt, der relaterer sig til bilen og dens funktioner.
    // class Program håndterer alt, der relaterer sig til brugergrænsefladen.
    class Car
    {
        // Herunder deklareres en række variabler.
        // get; angiver, at værdien kan læses (hentes) af andre klasser (den er public).
        // private set; angiver, at værdien kun kan ændres inden for den samme klasse (private betyder, at andre klasser ikke kan ændre værdien direkte).
        // private angiver, at variablen kun kan tilgås inden for samme klasse.
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public double Odometer { get; private set; }
        public double KmPerLiter { get; private set; }
        private bool isEngineOn;

        // Konstruktør til Car-klassen
        public Car(string brand, string model, int year, double odometer, double kmPerLiter)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Odometer = odometer;
            KmPerLiter = kmPerLiter;
            isEngineOn = false; // Motor starter som slukket
        }

        private void UpdateOdometer(double distance)
        {
            Odometer += distance;
        }

        // Metode til at simulere en køretur
        // metoden tager et parameter som input: double distance
        // if (isEngineOn) tjekker om bilens motor er tændt eller slukket. Bilen kan kun køre, hvis motoren er tændt.
        // Problem: Hvis brugeren skriver en negativ værdi, trækkes denne fra odometer.
        // Potentielt fix: Indfør if-sætning (betingelse), der tjekker om værdien er mindre end 0.
        public void Drive(double distance)
        {
            if (isEngineOn)
            {
                UpdateOdometer(distance);
                Console.WriteLine($"Bilen har kørt {distance} km. Ny odometer: {Odometer}");
            }
            else
            {
                Console.WriteLine("Motoren er slukket! Tænd den først.");
            }
        }

        // Beregner prisen for en tur
        // metoden tager to parametre som input: double distance og double literPrice
        // if-sætningen returnerer en fejlmeddelelse og 0-værdi, hvis KmPerLiter <= 0.
        // metoden beregner prisen og returnerer dette beløb
        public double CalculateTripPrice(double distance, double literPrice)
        {
            if (KmPerLiter <= 0)
            {
                Console.WriteLine("Fejl: KmPerLiter er 0. Udfør korrektion.");
                return 0;
            }
            return (distance / KmPerLiter) * literPrice;
        }

        // Tjekker om odometerstanden er et palindrom
        public bool IsPalindrome()
        {
            string kmStr = Odometer.ToString(); // Konverterer Odometer (som er et double-tal) til en string
            int len = kmStr.Length; // Bestemmer længden af kmStr

            for (int i = 0; i < len / 2; i++) // for-løkke, itererer fra første tegn i strengen (i = 0) op til midten af strengen (len / 2). Sammenligner tegnene fra starten og slutningen af strengen.
            {
                if (kmStr[i] != kmStr[len - 1 - i]) // tegnet på nuværende position i sammenlignes med tegnet på den spejlvendte position (len - 1- i). Hvis de ikke er ens, returneres false og metoden afsluttes.
                    return false;
            }
            return true; // hvis løkken kører uden at finde uoverensstemmelser, returneres true. 
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

        static void AddNewCar()
        {
            Console.Write("Indtast mærke: ");
            string brand = Console.ReadLine();

            Console.Write("Indtast model: ");
            string model = Console.ReadLine();

            Console.Write("Indtast årgang: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Indtast kilometerstand: ");
            double odometer = double.Parse(Console.ReadLine());

            Console.Write("Indtast km/l: ");
            double kmPerLiter = double.Parse(Console.ReadLine());

            // **Bruger konstruktøren til at oprette objektet**
            Car newCar = new Car(brand, model, year, odometer, kmPerLiter);
            teamCars.Add(newCar);

            int carIndex = teamCars.Count - 1;
            Console.WriteLine($"Bilen er tilføjet! Bilens nummer: {carIndex}");
        }

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nVælg en handling:\n");
                Console.WriteLine("1. Tilføj ny bil");
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
                        AddNewCar();
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
                        int carIndexForTrip = int.Parse(Console.ReadLine());

                        if (carIndexForTrip < 0 || carIndexForTrip >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car tripCar = teamCars[carIndexForTrip];

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
                        int selectedCarIndexForPalindrome = int.Parse(Console.ReadLine());

                        if (selectedCarIndexForPalindrome < 0 || selectedCarIndexForPalindrome >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForPalindrome = teamCars[selectedCarIndexForPalindrome];
                        Console.WriteLine(selectedCarForPalindrome.IsPalindrome() ? "Odometer er et palindrom!" : "Odometer er ikke et palindrom.");
                        break;

                    case "5":
                        if (teamCars.Count == 0)
                        {
                            Console.WriteLine("Fejl: Ingen biler tilføjet. Tilføj en bil først.");
                            break;
                        }

                        Console.Write("Indtast bilnummer (0 for første bil, 1 for anden osv.): ");
                        int selectedCarIndexForPrint = int.Parse(Console.ReadLine());

                        if (selectedCarIndexForPrint < 0 || selectedCarIndexForPrint >= teamCars.Count)
                        {
                            Console.WriteLine("Ugyldigt bilnummer.");
                            break;
                        }

                        Car selectedCarForPrint = teamCars[selectedCarIndexForPrint];
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
