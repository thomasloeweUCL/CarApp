using System;
using System.Collections.Generic;
using System.Linq;

namespace CarApp
{
    class Program
    {
        static List<Car> teamCars = new List<Car>();
        static List<CarOwner> owners = new List<CarOwner>();

        static void Main(string[] args)
        {
            // Filsti: Går op fra bin/Debug og ind i Data
            var filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data", "cars.txt"));
            DataHandler.SetFilePath(filePath);

            // Load data from cars.txt automatically
            teamCars = DataHandler.LoadCars();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Hovedmenu ===");
                Console.WriteLine("0. Tilføj ny bil");
                Console.WriteLine("1. Registrer køretur");
                Console.WriteLine("2. Vis ture for bil");
                Console.WriteLine("3. Start motor");
                Console.WriteLine("4. Stop motor");
                Console.WriteLine("5. Vis alle biler");
                Console.WriteLine("6. Opret ejer");
                Console.WriteLine("7. Tilføj bil til ejer");
                Console.WriteLine("8. Vis ture for ejer");
                Console.WriteLine("9. Vis biler for ejer");
                Console.WriteLine("10. Afslut");
                Console.Write("Vælg en mulighed (0-12): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ugyldigt input. Tryk på en tast for at fortsætte...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 0: RunWithPause(AddNewCar); break;
                    case 1: RunWithPause(RegisterTrip); break;
                    case 2: RunWithPause(ShowTrips); break;
                    case 3: RunWithPause(StartEngine); break;
                    case 4: RunWithPause(StopEngine); break;
                    case 5: RunWithPause(ShowAllCars); break;
                    case 6: RunWithPause(AddNewOwner); break;
                    case 7: RunWithPause(AssignCarToOwner); break;
                    case 8: RunWithPause(ShowOwnerTrips); break;
                    case 9: RunWithPause(ShowOwnerCars); break;
                    case 10: return;

                    default:
                        Console.WriteLine("Ugyldigt valg. Tryk på en tast for at fortsætte...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void RunWithPause(Action action)
        {
            Console.Clear();
            action();
            Console.WriteLine("\nTryk på en tast for at vende tilbage til menuen...");
            Console.ReadKey();
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

            DataHandler.SaveCars(teamCars);

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

            DataHandler.SaveCars(teamCars);
        }

        static void ShowTrips()
        {
            Car car = SelectCar();
            if (car == null) return;

            foreach (Trip trip in car.Trips)
            {
                trip.PrintTripDetails(car.KmPerLiter, trip.FuelPrice);
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

                DataHandler.SaveCars(teamCars);

                Console.WriteLine("Bil tilføjet til ejer.");
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
    }
}
