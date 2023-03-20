namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
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
            _database.Add(new Person(1, "Pesho"));
            Person result = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Pesho", result.UserName);
        }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
        {
            Person[] people = CreateFullArray();
            _database = new Database(people);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(17, "Pesho")));
            Assert.That(exeption.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }

        [Test]
        public void AddShouldThrowIfNotUniqueUsername()
        {
            _database.Add(new Person(1, "Pesho"));

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(17, "Pesho")));
            Assert.That(exeption.Message, Is.EqualTo("There is already user with this username!"));
        }

        [Test]
        public void AddShouldThrowIfNotUniqueId()
        {
            _database.Add(new Person(1, "Pesho"));

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.Add(new Person(1, "Gosho")));
            Assert.That(exeption.Message, Is.EqualTo("There is already user with this Id!"));
        }

        private Person[] CreateFullArray()
        {
            Person[] persons = new Person[16];

            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, i.ToString());
            }

            return persons;
        }

        [Test]
        public void CreateDatabaseWith2Elements()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            Person first = _database.FindById(1);
            Person second = _database.FindById(2);

            Assert.AreEqual(2, _database.Count);
            Assert.AreEqual("Pesho", first.UserName);
            Assert.AreEqual("Gosho", second.UserName);
        }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => _database.Remove());
        }

        [Test]
        public void RemoveFromDatabase()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            _database.Remove();
            Person first = _database.FindById(1);

            Assert.AreEqual(1, _database.Count);
            Assert.AreEqual("Pesho", first.UserName);
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.FindByUsername("Gosho"));
            Assert.That(exeption.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test]
        public void FindByUsernameShouldThrowIfUsernameNullOrEmpty()
        {
            ArgumentNullException exeption = Assert
                .Throws<ArgumentNullException>(() => _database.FindByUsername(null));
            Assert.That(exeption.ParamName, Is.EqualTo("Username parameter is null!"));

            ArgumentNullException emptyEx = Assert
                .Throws<ArgumentNullException>(() => _database.FindByUsername(string.Empty));
            Assert.That(emptyEx.ParamName, Is.EqualTo("Username parameter is null!"));
        }

        [Test]
        public void FindByUsernameShouldThrowIfUsernameDoesNotExist()
        {
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.FindByUsername("Gosho"));
            Assert.That(exeption.Message, Is.EqualTo("No user is present by this username!"));
        }

        [Test]
        public void FindByUsernameReturnsCorrectUser()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            Person person = _database.FindByUsername("Gosho");

            Assert.AreEqual("Gosho", person.UserName);
            Assert.AreEqual(2, person.Id);
        }

        [Test]
        public void FindByIdShouldThrowIfIdIsLessThan0()
        {
            ArgumentOutOfRangeException exeption = Assert
                .Throws<ArgumentOutOfRangeException>(() => _database.FindById(-2));
            Assert.That(exeption.ParamName, Is.EqualTo("Id should be a positive number!"));
        }

        [Test]
        public void FindByIdShouldThrowIfIdDoesNotExist()
        {
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => _database.FindById(8));
            Assert.That(exeption.Message, Is.EqualTo("No user is present by this ID!"));
        }

        [Test]
        public void FindByIdReturnsCorrectUser()
        {
            _database = new Database(new Person(1, "Pesho"), new Person(2, "Gosho"));
            Person person = _database.FindById(2);

            Assert.AreEqual("Gosho", person.UserName);
            Assert.AreEqual(2, person.Id);
        }
    }
}