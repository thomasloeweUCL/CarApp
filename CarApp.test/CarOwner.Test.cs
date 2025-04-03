using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Test
{
    [TestClass]
    public class CarOwnerTests
    {
        [TestMethod]
        // Tester at en bil korrekt tilføjes til ejerens liste over biler
        public void AddCar_ShouldAddCarToOwner()
        {
            var owner = new CarOwner("Alice");
            var car = new Car("Toyota", "Yaris", 2020, FuelType.Benzin, 12000, 18.5);

            owner.AddCar(car);

            Assert.AreEqual(1, owner.Cars.Count);
            Assert.AreSame(car, owner.Cars[0]);
        }

        [TestMethod]
        // Tester at samme bil ikke kan tilføjes flere gange til ejerens liste
        public void AddCar_ShouldNotAddDuplicateCars()
        {
            var owner = new CarOwner("Bob");
            var car = new Car("Ford", "Focus", 2019, FuelType.Diesel, 8000, 17.2);

            owner.AddCar(car);
            owner.AddCar(car); // forsøger at tilføje samme bil igen

            Assert.AreEqual(1, owner.Cars.Count, "Samme bil må ikke tilføjes to gange.");
        }
    }

}
