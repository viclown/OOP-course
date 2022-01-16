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
            
            Bank tinkoff = centralBank.CreateNewBank("Tinkoff", 3.65, 10, 100000, 100000);
            _tinkoff = tinkoff;
            
            var dimaBuilder = new ClientBuilder();
            dimaBuilder.SetNameAndSurname("Dmitri", "Ivanov");
            Client dima = dimaBuilder.BuildClient();
            tinkoff.AddNewClientToBank(dima);
            _dima = dima;
            
            var egorBuilder = new ClientBuilder();
            egorBuilder.SetNameAndSurname("Egor", "Ivanov");
            Client egor = egorBuilder.BuildClient();
            tinkoff.AddNewClientToBank(egor);
            _egor = egor;
        }

        [Test]
        public void CreateNewBank_BankWasRegistered()
        {
            Assert.AreEqual(_tinkoff, _centralBank.FindBank("Tinkoff"));
        }

        [Test]
        public void CreateDebitAccountForClient_CheckAfterMonth_GetMoney()
        {
            _tinkoff.AddNewClientToBank(_dima);
            DebitAccount account = _centralBank.CreateDebitAccount(_dima, _tinkoff);
            account.AddMoneyToAccount(account, 100000);
            
            _centralBank.RunTimeMechanism(35);
            Assert.AreEqual(100300, Math.Truncate(account.Money));
        }

        [Test]
        public void CreateCreditAccountForClient_GetCommission()
        {
            _tinkoff.AddNewClientToBank(_dima);
            CreditAccount account = _centralBank.CreateCreditAccount(_dima, _tinkoff);

            account.GetMoneyFromAccount(80000);
            _centralBank.RunTimeMechanism(365);
            
            // Клиент начал пользоваться кредитным счетом, банк каждый день снимает процент от той суммы, что взял клиент,
            // с его счета, пока клиент не вернет всю сумму, которая изначально лежала на его кредитной карте.
            Assert.AreEqual(11587, Math.Truncate(account.Money));
            
            account.AddMoneyToAccount(account, 150000);
            _centralBank.RunTimeMechanism(365);
            
            Assert.AreEqual(161587, Math.Truncate(account.Money));
        }
        
        [Test]
        public void CreateDepositAccountForClient_TryToGetMoney()
        {
            _tinkoff.AddNewClientToBank(_dima);
            DepositAccount account = _centralBank.CreateDepositAccount(_dima, 60, _tinkoff);
            account.AddMoneyToAccount(account, 50000);
            
            Assert.Catch<BanksException>(() =>
            {
                account.GetMoneyFromAccount(500);
            });
            
            _centralBank.RunTimeMechanism(90);

            account.GetMoneyFromAccount(500);
            
            // забрали 500 рублей, но еще накапала комиссия
            Assert.AreEqual(49795, Math.Truncate(account.Money));
        }
        
        [Test]
        public void CheckClientsId()
        {
            _tinkoff.AddNewClientToBank(_dima);
            _tinkoff.AddNewClientToBank(_egor);

            Assert.AreNotEqual(_egor.Id, _dima.Id); 
        }
        
        [Test]
        public void DeclineTransaction()
        {
            _tinkoff.AddNewClientToBank(_egor);
            DebitAccount accountEgor = _centralBank.CreateDebitAccount(_egor, _tinkoff);
            
            _tinkoff.AddNewClientToBank(_dima);
            DebitAccount accountDima = _centralBank.CreateDebitAccount(_dima, _tinkoff);
            accountDima.AddMoneyToAccount(accountDima, 100000);

            Transaction transaction = accountDima.TransferMoney(accountEgor, accountDima, 50);
            Assert.AreEqual(99950, accountDima.Money);
            
            transaction.DeclineTransaction();
            Assert.AreEqual(100000, accountDima.Money);
        }
    }
}