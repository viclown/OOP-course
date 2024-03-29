﻿using System;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes.BankAccount
{
    public class CreditAccount : Account
    {
        public CreditAccount(Client client, Bank bank, DateTime openingDate)
            : base(client, bank.LimitForCreditAccount.Value, bank, openingDate)
        {
            LimitForCreditAccount = bank.LimitForCreditAccount;
        }

        public BankLimit LimitForCreditAccount { get; }

        public override Transaction GetMoneyFromAccount(double money)
        {
            if (Money - money <= 0)
                throw new ReachedCreditLimitException("All money on credit account was already used.");
            return base.GetMoneyFromAccount(money);
        }

        public override void RunNewDay()
        {
            if (Money < LimitForCreditAccount.Value)
                base.GetMoneyFromAccount((LimitForCreditAccount.Value - Money) * (Bank.Commission.Value / 36500));
        }
    }
}