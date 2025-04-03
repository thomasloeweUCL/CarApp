using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using CarApp;

namespace CarApp.Test
{
    [TestClass]
    public class DataHandlerTests
    {
        private static string testFilePath;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            testFilePath = Path.Combine(Path.GetTempPath(), "cars_test.txt");
            DataHandler.SetFilePath(testFilePath);
        }

        [TestInitialize]
        public void Setup()
        {
            // Slet testfilen hvis den findes
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void TestSaveAndLoadCar()
        {
            // Arrange
            var car = new Car("Toyota", "Corolla", 2020, FuelType.Benzin, 10000.0, 18.5);
            car.Owner = new CarOwner("TestEjer");

            var trip = new Trip(
                120.0,
                DateTime.Today,
                DateTime.Today.AddHours(8),
                DateTime.Today.AddHours(10),
                14.0
            );
            car.Trips.Add(trip);
            var carsToSave = new List<Car> { car };

            // Act
            DataHandler.SaveCars(carsToSave);
            var loadedCars = DataHandler.LoadCars();

            // Assert
            Assert.AreEqual(1, loadedCars.Count);
            Assert.AreEqual(car.Brand, loadedCars[0].Brand);
            Assert.AreEqual(car.Model, loadedCars[0].Model);
            Assert.AreEqual(car.Year, loadedCars[0].Year);
            Assert.AreEqual(car.Fuel, loadedCars[0].Fuel);
            Assert.AreEqual(car.Odometer, loadedCars[0].Odometer);
            Assert.AreEqual(car.KmPerLiter, loadedCars[0].KmPerLiter);
            Assert.AreEqual(car.Trips.Count, loadedCars[0].Trips.Count);
        }

        [TestMethod]
        public void TestFileNotFound()
        {
            // Arrange
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }

            // Act
            var loadedCars = DataHandler.LoadCars();

            // Assert
            Assert.IsNotNull(loadedCars);
            Assert.AreEqual(0, loadedCars.Count);
        }
    }
}