using System;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(Client receiver, Client sender, double amountOfTransaction, DateTime transactionTime)
        {
            Receiver = receiver;
            Sender = sender;
            AmountOfTransaction = amountOfTransaction;
            TransactionTime = transactionTime;
        }

        public Client Receiver { get; }
        public Client Sender { get; }
        public double AmountOfTransaction { get; }
        public DateTime TransactionTime { get; }
        public bool WasDeclined { get; set; } = false;
    }
}