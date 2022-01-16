using System;
using Banks.Services;

namespace Banks.Classes.BankAccount
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank, DateTime openingDate)
            : base(client, 0, bank, openingDate)
        {
        }
    }
}