using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarApp;

namespace CarApp.Test
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            var car = new Car("Toyota", "Corolla", 2020, FuelType.Benzin, 10000, 20);

            Assert.AreEqual("Toyota", car.Brand);
            Assert.AreEqual("Corolla", car.Model);
            Assert.AreEqual(2020, car.Year);
            Assert.AreEqual(FuelType.Benzin, car.Fuel);
            Assert.AreEqual(10000, car.Odometer);
            Assert.AreEqual(20, car.KmPerLiter);
            Assert.IsFalse(car.EngineIsRunning);
            Assert.IsNotNull(car.Trips);
        }

        [TestMethod]
        public void ToggleEngine_ShouldToggleEngineStatus()
        {
            var car = new Car("Ford", "Focus", 2018, FuelType.Diesel, 5000, 18);
            car.ToggleEngine();
            Assert.IsTrue(car.EngineIsRunning);
            car.ToggleEngine();
            Assert.IsFalse(car.EngineIsRunning);
        }

        [TestMethod]
        public void Drive_ShouldUpdateOdometerAndAddTrip()
        {
            var car = new Car("Nissan", "Leaf", 2021, FuelType.Elektrisk, 3000, 15);
            car.ToggleEngine();

            var now = DateTime.Now;
            var trip = new Trip(
                distance: 100,
                tripDate: now.Date,
                startTime: now,
                endTime: now.AddMinutes(90),
                literPrice: 12.5
            );

            car.Drive(trip);

            Assert.AreEqual(3100, car.Odometer);
            Assert.AreEqual(1, car.Trips.Count);
            Assert.AreSame(trip, car.Trips[0]);
        }

        [TestMethod]
        public void IsPalindrome_ShouldReturnTrueForPalindromeOdometer()
        {
            var car = new Car("VW", "Golf", 2015, FuelType.Benzin, 12321, 17);
            Assert.IsTrue(car.IsPalindrome());
        }

        [TestMethod]
        public void IsPalindrome_ShouldReturnFalseForNonPalindromeOdometer()
        {
            var car = new Car("VW", "Golf", 2015, FuelType.Benzin, 12345, 17);
            Assert.IsFalse(car.IsPalindrome());
        }

        [TestMethod]
        public void ToStringAndFromFormattedString_ShouldRecreateSameCar()
        {
            var owner = new CarOwner("TestOwner");
            var original = new Car("Tesla", "Model 3", 2022, FuelType.Elektrisk, 2500, 18.5);
            original.Owner = owner;

            string[] lines = original.ToString().Split(Environment.NewLine);
            string carLine = Array.Find(lines, l => l.StartsWith("# Car:"));

            var recreated = Car.FromFormattedString(carLine, owner);

            Assert.AreEqual(original.Brand, recreated.Brand);
            Assert.AreEqual(original.Model, recreated.Model);
            Assert.AreEqual(original.Year, recreated.Year);
            Assert.AreEqual(original.Fuel, recreated.Fuel);
            Assert.AreEqual(original.Odometer, recreated.Odometer);
            Assert.AreEqual(original.KmPerLiter, recreated.KmPerLiter);
        }
    }
}