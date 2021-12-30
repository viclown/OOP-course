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
        public Account(Client client, double money, Bank bank)
        {
            Id = _lastId++;
            Client = client;
            Money = money;
            OpeningDate = bank.CurrentDate;
            LimitForSuspiciousClients = bank.LimitForSuspiciousClients;
            Commission = bank.Commission;
            Interest = bank.Interest;
            CurrentDate = bank.CurrentDate;
            HistoryOfTransactions = new List<Transaction>();
            DaysInCurrentMonth = DateTime.DaysInMonth(bank.CurrentDate.Year, bank.CurrentDate.Month);
        }

        public int Id { get; }
        public Client Client { get; }
        public virtual double Money { get; set; }
        public DateTime OpeningDate { get; }
        public BankLimit LimitForSuspiciousClients { get; }
        public BankCommission Commission { get; }
        public BankInterest Interest { get; }
        public DateTime CurrentDate { get; set; }
        public List<Transaction> HistoryOfTransactions { get; set; }
        public int DaysInCurrentMonth { get; set; }

        public virtual Transaction AddMoneyToAccount(Client receiver, Client sender, double amountOfTransaction)
        {
            if (amountOfTransaction <= 0)
            {
                throw new InvalidAmountOfTransactionException("Amount of money must be positive. Please, try again");
            }

            var transaction = new Transaction(receiver, sender, amountOfTransaction, DateTime.Now);
            Money += amountOfTransaction;
            HistoryOfTransactions.Add(transaction);
            return transaction;
        }

        public virtual Transaction GetMoneyFromAccount(double amountOfTransaction)
        {
            if (Client.IsSuspicious && LimitForSuspiciousClients.Value < amountOfTransaction)
                throw new SuspiciousAccountException($"Value of your transaction cannot exceed {LimitForSuspiciousClients.Value}. Please, add information about your passport and address to remove the limit");
            var transaction = new Transaction(Client, Client, amountOfTransaction, DateTime.Now);
            Money -= amountOfTransaction;
            HistoryOfTransactions.Add(transaction);
            return transaction;
        }

        public Transaction DeclineTransaction(Transaction transaction)
        {
            if (transaction.WasDeclined)
            {
                throw new DeclinedTransactionException($"Transaction of {transaction.TransactionTime} from {transaction.Sender.Name + transaction.Sender.Surname} to {transaction.Receiver.Name + transaction.Receiver.Surname} was already declined");
            }

            transaction.WasDeclined = true;
            Money += transaction.AmountOfTransaction;
            return transaction;
        }

        public void TransferMoney(DebitAccount receiverAccount, float money)
        {
            GetMoneyFromAccount(money);
            receiverAccount.AddMoneyToAccount(receiverAccount.Client, Client, money);
        }

        public virtual void CheckNewDay()
        {
            CurrentDate = CurrentDate.AddDays(1);
            if (CurrentDate.Subtract(OpeningDate).Days % DaysInCurrentMonth == 0)
            {
                AddMoneyToAccount(Client, Client, _interestCollectedInMonth);
                _interestCollectedInMonth = 0;
                DaysInCurrentMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            }
            else
            {
                _interestCollectedInMonth += Money * (Interest.Value / 36500);
            }
        }
    }
}