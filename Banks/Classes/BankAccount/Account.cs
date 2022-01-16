using System;
using System.Collections.Generic;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes.BankAccount
{
    public class Account
    {
        private double _interestCollectedInMonth = 0;
        private int _lastId = 0;
        private List<Transaction> _historyOfTransactions = new List<Transaction>();

        public Account(Client client, double money, Bank bank, DateTime openingDate)
        {
            Id = _lastId++;
            Client = client;
            Money = money;
            Bank = bank;
            OpeningDate = openingDate;
            DaysInCurrentMonth = DateTime.DaysInMonth(openingDate.Year, openingDate.Month);
        }

        public int Id { get; }
        public Client Client { get; }
        public double Money { get; set; }
        public Bank Bank { get; }
        public DateTime OpeningDate { get; }
        public int DaysInCurrentMonth { get; set; }

        public virtual Transaction AddMoneyToAccount(Account sender, double amountOfTransaction)
        {
            if (amountOfTransaction <= 0)
            {
                throw new InvalidAmountOfTransactionException("Amount of money must be positive. Please, try again");
            }

            var transaction = new Transaction(this, sender, amountOfTransaction, Bank.CurrentDate);
            Money += amountOfTransaction;
            _historyOfTransactions.Add(transaction);
            return transaction;
        }

        public virtual Transaction GetMoneyFromAccount(double amountOfTransaction)
        {
            if (Client.IsSuspicious && Bank.LimitForSuspiciousClients.Value < amountOfTransaction)
                throw new SuspiciousAccountException($"Value of your transaction cannot exceed {Bank.LimitForSuspiciousClients.Value}. Please, add information about your passport and address to remove the limit");
            var transaction = new Transaction(this, this, amountOfTransaction, DateTime.Now);
            Money -= amountOfTransaction;
            _historyOfTransactions.Add(transaction);
            return transaction;
        }

        public Transaction TransferMoney(Account receiver, Account sender, float money)
        {
            GetMoneyFromAccount(money);
            receiver.AddMoneyToAccount(sender, money);

            return new Transaction(receiver, sender, money, Bank.CurrentDate);
        }

        public virtual void RunNewDay()
        {
            Console.WriteLine(Bank.CurrentDate.Subtract(OpeningDate).Days);
            Console.WriteLine(Money);
            if (Bank.CurrentDate.Subtract(OpeningDate).Days % DaysInCurrentMonth == 0)
            {
                AddMoneyToAccount(this, _interestCollectedInMonth);
                _interestCollectedInMonth = 0;
                DaysInCurrentMonth = DateTime.DaysInMonth(Bank.CurrentDate.Year, Bank.CurrentDate.Month);
            }
            else
            {
                _interestCollectedInMonth += Money * (Bank.Interest.Value / 36500);
            }
        }
    }
}