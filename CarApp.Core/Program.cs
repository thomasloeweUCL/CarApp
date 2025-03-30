using System;
using System.Collections.Generic;

namespace CarApp
{
    class Program
    {
        static List<Car> teamCars = new List<Car>();
        static List<CarOwner> owners = new List<CarOwner>();

        static void Main(string[] args)
        {
            while (true)
            {
                string[] menuOptions = {
                    "Tilføj ny bil",
                    "Registrer køretur",
                    "Vis ture for bil",
                    "Start motor",
                    "Stop motor",
                    "Vis alle biler",
                    "Opret ejer",
                    "Tilføj bil til ejer",
                    "Vis ture for ejer",
                    "Vis biler for ejer",
                    "Afslut"
                };

                int choice = ShowMenu(menuOptions);

                switch (choice)
                {
                    case 0: AddNewCar(); break;
                    case 1: RegisterTrip(); break;
                    case 2: ShowTrips(); break;
                    case 3: StartEngine(); break;
                    case 4: StopEngine(); break;
                    case 5: ShowAllCars(); break;
                    case 6: AddNewOwner(); break;
                    case 7: AssignCarToOwner(); break;
                    case 8: ShowOwnerTrips(); break;
                    case 9: ShowOwnerCars(); break;
                    case 10: return;
                }

                Console.WriteLine("\nTryk på en tast for at fortsætte...");
                Console.ReadKey();
            }
        }

        static int ShowMenu(string[] options)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("--- Menu ---\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                else if (key == ConsoleKey.DownArrow)
                    selectedIndex = (selectedIndex + 1) % options.Length;

            } while (key != ConsoleKey.Enter);

            return selectedIndex;
        }

        static void AddNewCar()
        {
            Console.Write("Mærke: ");
            string brand = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Årgang: ");
            int year = int.Parse(Console.ReadLine());

            FuelType[] fuels = (FuelType[])Enum.GetValues(typeof(FuelType));
            for (int i = 0; i < fuels.Length; i++)
            {
                Console.WriteLine($"{i}: {fuels[i]}");
            }

            Console.Write("Vælg brændstoftype (nummer): ");
            int fuelIndex = int.Parse(Console.ReadLine());
            FuelType fuel = fuels[fuelIndex];

            Console.Write("Kilometerstand: ");
            double odometer = double.Parse(Console.ReadLine());

            Console.Write("Km pr. liter: ");
            double kmPerLiter = double.Parse(Console.ReadLine());

            Car newCar = new Car(brand, model, year, fuel, odometer, kmPerLiter);
            teamCars.Add(newCar);

            Console.WriteLine("Bil tilføjet.");
        }

        static void RegisterTrip()
        {
            Car car = SelectCar();
            if (car == null)
            {
                Console.WriteLine("Ingen bil valgt.");
                return;
            }

            if (!car.EngineIsRunning)
            {
                Console.WriteLine("Motoren er ikke tændt! Du skal starte motoren før du kan køre.");
                return;
            }

            Console.Write("Distance (km): ");
            double distance = double.Parse(Console.ReadLine());

            Console.Write("Benzinpris pr. liter: ");
            double fuelPrice = double.Parse(Console.ReadLine());

            DateTime now = DateTime.Now;
            Trip trip = new Trip(distance, now.Date, now, now.AddMinutes(30), fuelPrice);

            car.Drive(trip);
        }

        static void ShowTrips()
        {
            Car car = SelectCar();
            if (car == null) return;

            foreach (Trip trip in car.Trips)
            {
                trip.PrintTripDetails(car.KmPerLiter);
            }
        }

        static void StartEngine()
        {
            Car car = SelectCar();
            car?.ToggleEngine();
        }

        static void StopEngine()
        {
            Car car = SelectCar();
            car?.ToggleEngine();
        }

        static void ShowAllCars()
        {
            for (int i = 0; i < teamCars.Count; i++)
            {
                Car car = teamCars[i];
                Console.WriteLine($"{i + 1}: {car.Brand} {car.Model} ({car.Year}) - {car.Odometer} km");
            }
        }

        static Car SelectCar()
        {
            if (teamCars.Count == 0) return null;

            Console.WriteLine("Vælg bil:");
            for (int i = 0; i < teamCars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teamCars[i].Brand} {teamCars[i].Model}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= teamCars.Count)
                return teamCars[index - 1];

            return null;
        }

        static void AddNewOwner()
        {
            Console.Write("Ejerens navn: ");
            string name = Console.ReadLine();
            owners.Add(new CarOwner(name));
            Console.WriteLine("Ejer tilføjet.");
        }

        static void AssignCarToOwner()
        {
            Car car = SelectCar();
            if (car == null) return;

            Console.WriteLine("Vælg ejer:");
            for (int i = 0; i < owners.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {owners[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= owners.Count)
            {
                owners[index - 1].AddCar(car);
                Console.WriteLine("Bil tilføjet til ejer.");
            }
        }

        static void ShowOwnerCars()
        {
            Console.WriteLine("Vælg ejer:");
            for (int i = 0; i < owners.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {owners[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= owners.Count)
            {
                CarOwner owner = owners[index - 1];
                Console.WriteLine($"Biler ejet af { owner.Name}:");
                if (owner.Cars.Count == 0)
                {
                    Console.WriteLine("Ingen biler registreret.");
                }
                else
                {
                    foreach (var car in owner.Cars)
                    {
                        Console.WriteLine($"- {car.Brand} {car.Model} ({car.Year}) - {car.Odometer} km");
                    }
                }
            }
        }

        static void ShowOwnerTrips()
        {
            Console.WriteLine("Vælg ejer:");
            for (int i = 0; i < owners.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {owners[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= owners.Count)
            {
                CarOwner owner = owners[index - 1];
                owner.PrintAllTrips();
                Console.WriteLine($"Samlet pris: {owner.GetTotalCost():N2} kr");
            }
        }
    }
}
