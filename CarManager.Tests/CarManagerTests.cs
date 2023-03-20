namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void Setup()
        {
            car = new Car("Renault", "Megan", 5.3, 45);
        }

        [TearDown]
        public void TearDown() 
        {
            car = null;
        }

        [Test]
        public void CreateCar()
        {
            car = new Car("Renault", "Megan", 5.3, 45);

            Assert.AreEqual("Renault", car.Make);
            Assert.AreEqual("Megan", car.Model);
            Assert.AreEqual(5.3, car.FuelConsumption);
            Assert.AreEqual(45, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateCarFailsIfMakeIsNullOrEmpty(string make)
        {
            ArgumentException exeption = Assert
                .Throws<ArgumentException>(() => new Car(make, "Megan", 5.3, 45));
            Assert.That(exeption.Message, Is.EqualTo("Make cannot be null or empty!"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void CreateCarFailsIfModelIsNullOrEmpty(string model)
        {
            ArgumentException exeption = Assert
                .Throws<ArgumentException>(() => new Car("Renault", model, 5.3, 45));
            Assert.That(exeption.Message, Is.EqualTo("Model cannot be null or empty!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void CreateCarFailsIfFuelConsumptionIsLessOrEqualTo0(double consumption)
        {
            ArgumentException exeption = Assert
                .Throws<ArgumentException>(() => new Car("Renault", "Megan", consumption, 45));
            Assert.That(exeption.Message, Is.EqualTo("Fuel consumption cannot be zero or negative!"));
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void CreateCarFailsIfFuelCapacityIsLessOrEqualTo0(double capacity)
        {
            ArgumentException exeption = Assert
                .Throws<ArgumentException>(() => new Car("Renault", "Megan", 5.3, capacity));
            Assert.That(exeption.Message, Is.EqualTo("Fuel capacity cannot be zero or negative!"));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-3)]
        public void RefuelShoudThrowIfLessThanOrEqualTo0(double litres)
        {
            ArgumentException exeption = Assert
                .Throws<ArgumentException>(() => car.Refuel(litres));
            Assert.That(exeption.Message, Is.EqualTo("Fuel amount cannot be zero or negative!"));
        }

        [Test]
        public void RefuelShoudBeEqualToFuelAmmount()
        {
            car.Refuel(42);

            Assert.AreEqual(42, car.FuelAmount);
        }

        [Test]
        public void RefuelShoudBeEqualToFuelCapacity()
        {
            car.Refuel(48);

            Assert.AreEqual(45, car.FuelAmount);
        }

        [Test]
        public void DriveShouldThrowIfNotEnoughFuel()
        {
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => car.Drive(1));
            Assert.That(exeption.Message, Is.EqualTo("You don't have enough fuel to drive!"));
        }

        [Test]
        public void DriveShouldLeaveFuel()
        {
            car.Refuel(10);
            car.Drive(100);

            Assert.AreEqual(4.7, car.FuelAmount);
        }
    }
}