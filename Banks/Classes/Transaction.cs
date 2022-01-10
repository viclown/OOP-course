using System;
using Banks.Classes.BankAccount;
using Banks.Tools;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(Account receiver, Account sender, double amountOfTransaction, DateTime transactionTime)
        {
            Receiver = receiver;
            Sender = sender;
            AmountOfTransaction = amountOfTransaction;
            TransactionTime = transactionTime;
        }

        public Account Receiver { get; }
        public Account Sender { get; }
        public double AmountOfTransaction { get; }
        public DateTime TransactionTime { get; }
        public bool WasDeclined { get; set; } = false;

        public void DeclineTransaction()
        {
            if (WasDeclined)
            {
                throw new DeclinedTransactionException($"Transaction of {TransactionTime} from {Sender.Client.Name + Sender.Client.Surname} to {Receiver.Client.Name + Receiver.Client.Surname} was already declined");
            }

            WasDeclined = true;
            Sender.Money += AmountOfTransaction;
        }
    }
}