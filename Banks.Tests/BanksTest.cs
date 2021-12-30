using System;
using Banks.Classes;
using Banks.Classes.BankAccount;
using Banks.Services;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            var centralBank = new CentralBank();
            _centralBank = centralBank;
        }

        [Test]
        public void CreateNewBank_BankWasRegistered()
        {
            Bank tinkoff = _centralBank.CreateNewBank("Tinkoff", 3.65, 10, 15000, 100000);
            Assert.AreEqual(tinkoff, _centralBank.FindBank("Tinkoff"));
        }

        [Test]
        public void AddClientToBank_CheckForSuspicion()
        {
            var builder = new ClientBuilder();
            Client dima = builder.AddNewClient("Dmitri", "Ivanov");
            Assert.AreEqual(true, dima.IsSuspicious);
            builder.SetAddress("Nevskii prospekt 10");
            Assert.AreEqual(true, dima.IsSuspicious);
            builder.SetPassport(1234567890);
            Assert.AreEqual(false, dima.IsSuspicious);
        }

        [Test]
        public void CreateDebitAccountForClient_CheckAfterMonth_GetMoney()
        {
            Bank tinkoff = _centralBank.CreateNewBank("Tinkoff", 3.65, 10, 15000, 100000);
            
            var builder = new ClientBuilder();
            Client dima = builder.AddNewClient("Dmitri", "Ivanov");
            builder.SetAddress("Nevskii prospekt 10");
            builder.SetPassport(1234567890);

            tinkoff.AddNewClientToBank(dima);
            DebitAccount account = _centralBank.CreateDebitAccount(dima, tinkoff);
            account.AddMoneyToAccount(dima, dima, 100000);
            
            _centralBank.RunTimeMechanism(31);
            Assert.AreEqual(100300, account.Money);
            
            _centralBank.RunTimeMechanism(31);
            Assert.AreEqual(100600.9, account.Money);
        }

        [Test]
        public void CreateCreditAccountForClient_GetCommission()
        {
            Bank tinkoff = _centralBank.CreateNewBank("Tinkoff", 3.65, 30, 15000, 100000);
            
            var builder = new ClientBuilder();
            Client dima = builder.AddNewClient("Dmitri", "Ivanov");
            builder.SetAddress("Nevskii prospekt 10");
            builder.SetPassport(1234567890);

            tinkoff.AddNewClientToBank(dima);
            CreditAccount account = _centralBank.CreateCreditAccount(dima, tinkoff);

            account.GetMoneyFromAccount(80000);
            _centralBank.RunTimeMechanism(365);
            
            // Клиент начал пользоваться кредитным счетом, банк каждый день снимает процент от той суммы, что взял клиент,
            // с его счета, пока клиент не вернет всю сумму, которая изначально лежала на его кредитной карте.
            Assert.AreEqual(-7975, Math.Truncate(account.Money));
            
            account.AddMoneyToAccount(dima, dima, 150000);
            _centralBank.RunTimeMechanism(365);
            
            Assert.AreEqual(142024, Math.Truncate(account.Money));
        }
        
        [Test]
        public void CreateDepositAccountForClient_TryToGetMoney()
        {
            Bank tinkoff = _centralBank.CreateNewBank("Tinkoff", 3.65, 30, 15000, 100000);

            var builder = new ClientBuilder();
            Client dima = builder.AddNewClient("Dmitri", "Ivanov");
            builder.SetAddress("Nevskii prospekt 10");
            builder.SetPassport(1234567890);

            tinkoff.AddNewClientToBank(dima);
            DepositAccount account = _centralBank.CreateDepositAccount(dima, 60, tinkoff);
            account.AddMoneyToAccount(dima, dima,50000);

            Assert.Catch<BanksException>(() =>
            {
                account.GetMoneyFromAccount(500);
            });
            
            _centralBank.RunTimeMechanism(65);

            account.GetMoneyFromAccount(500);
            
            // забрали 500 рублей, но еще накапала комиссия
            Assert.AreEqual(49800, Math.Truncate(account.Money));
        }
    }
}