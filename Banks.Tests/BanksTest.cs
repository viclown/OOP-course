using System;
using Banks.Classes;
using Banks.Classes.BankAccount;
using Banks.Services;
using Banks.Tools;
using NuGet.Frameworks;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private CentralBank _centralBank;
        private Bank _tinkoff;
        private Client _egor;
        private Client _dima;

        [SetUp]
        public void Setup()
        {
            var centralBank = new CentralBank();
            _centralBank = centralBank;
            
            Bank tinkoff = centralBank.CreateNewBank("Tinkoff", 3.65, 10, 15000, 100000);
            _tinkoff = tinkoff;
            
            var dimaBuilder = new ClientBuilder();
            dimaBuilder.SetNameAndSurname("Dmitri", "Ivanov");
            Client dima = dimaBuilder.Build();
            tinkoff.AddNewClientToBank(dima);
            _dima = dima;
            
            var egorBuilder = new ClientBuilder();
            egorBuilder.SetNameAndSurname("Egor", "Ivanov");
            Client egor = egorBuilder.Build();
            tinkoff.AddNewClientToBank(egor);
            _egor = egor;
        }

        [Test]
        public void CreateNewBank_BankWasRegistered()
        {
            Assert.AreEqual(_tinkoff, _centralBank.FindBank("Tinkoff"));
        }

        [Test]
        public void AddClientToBank_CheckForSuspicion()
        {
            var builder = new ClientBuilder();
            builder.SetNameAndSurname("Vladimir", "Sergeev");
            Client vova = builder.Build();
            Assert.AreEqual(true, vova.IsSuspicious);
            
            vova.SetAddress("Zarechnaja 17");
            Assert.AreEqual(true, vova.IsSuspicious);
            
            vova.SetPassport(1234567890);
            Assert.AreEqual(false, vova.IsSuspicious);
        }

        [Test]
        public void CreateDebitAccountForClient_CheckAfterMonth_GetMoney()
        {
            _tinkoff.AddNewClientToBank(_dima);
            DebitAccount account = _centralBank.CreateDebitAccount(_dima, _tinkoff);
            account.AddMoneyToAccount(_dima, _dima, 100000);
            
            _centralBank.RunTimeMechanism(31);
            Assert.AreEqual(100590, Math.Truncate(account.Money));
        }

        [Test]
        public void CreateCreditAccountForClient_GetCommission()
        {
            _dima.SetAddress("Zarechnaja 17");
            Assert.AreEqual(true, _dima.IsSuspicious);
            _dima.SetPassport(1234567890);
            
            _tinkoff.AddNewClientToBank(_dima);
            CreditAccount account = _centralBank.CreateCreditAccount(_dima, _tinkoff);

            account.GetMoneyFromAccount(80000);
            _centralBank.RunTimeMechanism(365);
            
            // Клиент начал пользоваться кредитным счетом, банк каждый день снимает процент от той суммы, что взял клиент,
            // с его счета, пока клиент не вернет всю сумму, которая изначально лежала на его кредитной карте.
            Assert.AreEqual(2290, Math.Truncate(account.Money));
            
            account.AddMoneyToAccount(_dima, _dima, 150000);
            _centralBank.RunTimeMechanism(365);
            
            Assert.AreEqual(152290, Math.Truncate(account.Money));
        }
        
        [Test]
        public void CreateDepositAccountForClient_TryToGetMoney()
        {
            _tinkoff.AddNewClientToBank(_dima);
            DepositAccount account = _centralBank.CreateDepositAccount(_dima, 60, _tinkoff);
            account.AddMoneyToAccount(_dima, _dima,50000);

            Assert.Catch<BanksException>(() =>
            {
                account.GetMoneyFromAccount(500);
            });
            
            _centralBank.RunTimeMechanism(65);

            account.GetMoneyFromAccount(500);
            
            // забрали 500 рублей, но еще накапала комиссия
            Assert.AreEqual(50092, Math.Truncate(account.Money));
        }
        
        [Test]
        public void CheckClientsId()
        {
            _tinkoff.AddNewClientToBank(_dima);
            _tinkoff.AddNewClientToBank(_egor);

            Assert.AreNotEqual(_egor.Id, _dima.Id); 
        }
    }
}