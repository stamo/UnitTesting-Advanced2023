namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database _database;

        [SetUp]
        public void Setup()
        {
            _database = new Database();
        }

        [TearDown]
        public void TearDown() 
        {
            _database = null;
        }

        [Test]
        public void AddMethodTest()
        {
            _database.Add(5);
            int[] result = _database.Fetch();

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(5, result[0]);
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
        {
            _database = new Database(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.Add(8));
            Assert.That(exeption.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        [Test]
        public void CreateDatabaseWith10Elements()
        {
            _database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Assert.AreEqual(10, _database.Count);
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
        {
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.Remove());
            Assert.That(exeption.Message, Is.EqualTo("The collection is empty!"));
        }

        [Test]
        public void RemoveFromDatabase() 
        {
            _database = new Database(5, 15);
            _database.Remove();
            var result = _database.Fetch();

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(5, result[0]);
        }

        [Test]
        public void FetchDataFromDatabase()
        {
            _database = new Database(1,2,3);
            var result = _database.Fetch();

            Assert.That(new int[] { 1, 2, 3 }, Is.EquivalentTo(result));
        }
    }
}
