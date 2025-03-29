using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarApp;
using System;
using System.IO;

namespace CarApp.test
{
    [TestClass]
    public class CarTests
    {
        private Car _car;
        private Trip _trip;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            _car = new Car("Toyota", "Corolla", 2020, FuelType.Benzin, 10000, 20);
            _trip = new Trip
            {
                Distance = 100,
                LiterPrice = 15,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(60),
                BreakTime = DateTime.Now.AddMinutes(30)
            };
        }

        [TestMethod]
        public void Drive_EngineOff_PrintsWarning()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            _car.Drive(_trip);

            // Assert
            var output = sw.ToString();
            StringAssert.Contains(output, "Motoren er slukket");
            Assert.AreEqual(10000, _car.Odometer);
            Assert.AreEqual(0, _car.Trips.Count);
        }

        [TestMethod]
        public void Drive_NullTrip_PrintsError()
        {
            // Arrange
            _car.ToggleEngine();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            _car.Drive(null);

            // Assert
            var output = sw.ToString();
            StringAssert.Contains(output, "Fejl: Turen er ugyldig");
            Assert.AreEqual(10000, _car.Odometer);
            Assert.AreEqual(0, _car.Trips.Count);
        }

        [TestMethod]
        public void Drive_ValidTrip_UpdatesOdometerAndAddsTrip()
        {
            // Arrange
            _car.ToggleEngine();

            // Act
            _car.Drive(_trip);

            // Assert
            Assert.AreEqual(10100, _car.Odometer);
            Assert.AreEqual(1, _car.Trips.Count);
            Assert.AreEqual(_trip, _car.Trips[0]);
        }

        [TestMethod]
        public void Drive_MultipleTrips_SumsOdometerCorrectly()
        {
            // Arrange
            var car = new Car("Honda", "Civic", 2022, FuelType.Diesel, 5000, 25);
            car.ToggleEngine();
            var trip1 = new Trip { Distance = 60, LiterPrice = 14, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(60), BreakTime = DateTime.Now.AddMinutes(30) };
            var trip2 = new Trip { Distance = 40, LiterPrice = 14, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(60), BreakTime = DateTime.Now.AddMinutes(30) };

            // Act
            car.Drive(trip1);
            car.Drive(trip2);

            // Assert
            Assert.AreEqual(5100, car.Odometer);
            Assert.AreEqual(2, car.Trips.Count);
        }

        [TestMethod]
        public void Drive_PalindromeCheck_TrueAfterDrive()
        {
            // Arrange
            var car = new Car("Mazda", "3", 2018, FuelType.El, 12320, 10);
            car.ToggleEngine();
            var trip = new Trip { Distance = 1, LiterPrice = 0, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(10), BreakTime = DateTime.Now.AddMinutes(5) };

            // Act
            car.Drive(trip);

            // Assert
            Assert.AreEqual(12321, car.Odometer);
            Assert.IsTrue(car.IsPalindrome());
        }
    }
}