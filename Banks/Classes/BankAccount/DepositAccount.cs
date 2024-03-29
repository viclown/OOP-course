﻿using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes.BankAccount
{
    public class DepositAccount : Account
    {
        public DepositAccount(Client client, Bank bank, DateTime closingDate, DateTime openingDate)
            : base(client, 0, bank, openingDate)
        {
            ClosingDate = closingDate;
        }

        public DateTime ClosingDate { get; }

        public override Transaction GetMoneyFromAccount(double money)
        {
            if (DateTime.Compare(Bank.CurrentDate, ClosingDate) < 0)
            {
                throw new DepositAccountClosedException($"You cannot get money from deposit account. Please, wait for {ClosingDate}");
            }

            return base.GetMoneyFromAccount(money);
        }
    }
}