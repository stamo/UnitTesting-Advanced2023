namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void Setup()
        {
           arena  = new Arena();
        }

        [TearDown]
        public void TearDown() 
        {
            arena = null;
        }

        [Test]
        public void ArenaShoudBeVoidOnCreate()
        {
            arena = new Arena();

            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollShouldAddWarrior()
        {
            arena.Enroll(new Warrior("Pesho", 5, 12));

            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void EnrollShouldThrowIfWarriorNameIsNotUnique()
        {
            arena.Enroll(new Warrior("Pesho", 5, 12));

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Pesho", 5, 12)));
            Assert.That(exeption.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        [Test]
        public void FightShouldThrowIfDefenderIsMissing()
        {
            arena.Enroll(new Warrior("Pesho", 5, 12));

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => arena.Fight("Pesho", "Gosho"));
            Assert.That(exeption.Message, Is.EqualTo("There is no fighter with name Gosho enrolled for the fights!"));
        }

        [Test]
        public void FightShouldThrowIfAttackerIsMissing()
        {
            arena.Enroll(new Warrior("Pesho", 5, 12));

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => arena.Fight("Misho", "Pesho"));
            Assert.That(exeption.Message, Is.EqualTo("There is no fighter with name Misho enrolled for the fights!"));
        }

        [Test]
        public void TestFigth()
        {
            var attacker = new Warrior("Pesho", 15, 35);
            var defender = new Warrior("Gosho", 15, 45);
            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(20, attacker.HP);
            Assert.AreEqual(30, defender.HP);
        }
    }
}
