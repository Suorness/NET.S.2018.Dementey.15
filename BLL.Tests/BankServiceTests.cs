namespace BLL.Tests
{
    using System;
    using BLL.BankSystem.ServicesImplementation;
    using BLL.Interface.Interfaces;
    using DAL.Interface.DTO;
    using DAL.Interface.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BankServiceTests
    {

        [TestCase("Vasya", "Vas","1")]
        [TestCase("Katya", "Kat","1")]
        [TestCase("Helen", "H","1")]
        [TestCase("Vlad", "V","1")]
        [TestCase("FirstName", "LastName","Number")]
        public void GeneratorCallTests(string firstName, string lastName, string number)
        {
            var storageMock = new Mock<IAccountStorage>();
            var generatorMock = new Mock<IGeneratorNumber>();

            generatorMock.Setup(generator => generator.GenerateNumber(firstName,lastName,0))
                .Returns(number);
            var bankManager = new BankService(storageMock.Object, generatorMock.Object);

            bankManager.OpenAccount(firstName, lastName, 0m);

            generatorMock.Verify(generator => generator.GenerateNumber(firstName, lastName, 0), Times.Once);
        }

        [TestCase("", "Vas", "1")]
        [TestCase("Vasya", "", "1")]
        [TestCase("", "", "1")]
        public void OpenAccountThrowsArgumentException(string firstName, string lastName, string number)
        {
            var storageMock = new Mock<IAccountStorage>();
            var generatorMock = new Mock<IGeneratorNumber>();

            generatorMock.Setup(generator => generator.GenerateNumber(firstName, lastName, 0))
                .Returns(number);
            var bankManager = new BankService(storageMock.Object, generatorMock.Object);
            Assert.Throws<ArgumentException>(() => bankManager.OpenAccount(firstName, lastName, 0m));
        }

        [TestCase(null)]
        [TestCase("")]
        public void WithdrawMoneyThrowsArgumentException(string number)
        {
            var storageMock = new Mock<IAccountStorage>();
            var generatorMock = new Mock<IGeneratorNumber>();

            generatorMock.Setup(generator => generator.GenerateNumber("f", "l", 0))
                .Returns(number);
            var bankManager = new BankService(storageMock.Object, generatorMock.Object);
            Assert.Throws<ArgumentException>(() => bankManager.WithdrawMoney(number,0m));
        }

        [TestCase(null)]
        [TestCase("")]
        public void DepositMoneyThrowsArgumentException(string number)
        {
            var storageMock = new Mock<IAccountStorage>();
            var generatorMock = new Mock<IGeneratorNumber>();

            generatorMock.Setup(generator => generator.GenerateNumber("f", "l", 0))
                .Returns(number);
            var bankManager = new BankService(storageMock.Object, generatorMock.Object);
            Assert.Throws<ArgumentException>(() => bankManager.DepositMoney(number, 0m));
        }

        [Test]
        public void AddCallTests()
        {
            var storageMock = new Mock<IAccountStorage>();
            var generatorMock = new Mock<IGeneratorNumber>();

            generatorMock.Setup(generator => generator.GenerateNumber("f", "l", 0))
                .Returns("f");
            var bankManager = new BankService(storageMock.Object, generatorMock.Object);

            bankManager.GetAccounts();
            storageMock.Verify(s => s.GetBankAccounts(), Times.Exactly(2));
        }

    }
}
