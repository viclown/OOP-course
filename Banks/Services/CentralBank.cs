using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Classes.BankAccount;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
            CurrentDate = DateTime.Today;
        }

        public List<Bank> Banks { get; set; }
        public DateTime CurrentDate { get; set; }

        public Bank CreateNewBank(string name, double interest, double commission, double limitForSuspiciousClients, double limitForCreditAccount)
        {
            var bankInterest = new BankInterest(interest);
            var bankCommission = new BankCommission(commission);
            var bankLimitForSuspiciousClients = new BankLimit(limitForSuspiciousClients);
            var bankLimitForCreditAccount = new BankLimit(limitForCreditAccount);
            var bank = new Bank(name, bankInterest, bankCommission, bankLimitForSuspiciousClients, bankLimitForCreditAccount, CurrentDate);
            Banks.Add(bank);
            return bank;
        }

        public DebitAccount CreateDebitAccount(Client client, Bank bank)
        {
            var account = new DebitAccount(client, bank);
            bank.DebitAccounts.Add(account);
            client.DebitAccounts.Add(account);
            return account;
        }

        public DepositAccount CreateDepositAccount(Client client, int durationInDays, Bank bank)
        {
            var account = new DepositAccount(client, bank, DateTime.Now.AddDays(durationInDays));
            bank.DepositAccounts.Add(account);
            client.DepositAccounts.Add(account);
            return account;
        }

        public CreditAccount CreateCreditAccount(Client client, Bank bank)
        {
            var account = new CreditAccount(client, bank);
            bank.CreditAccounts.Add(account);
            client.CreditAccounts.Add(account);
            return account;
        }

        public Bank FindBank(string name)
        {
            foreach (Bank bank in Banks)
            {
                if (bank.Name == name)
                    return bank;
            }

            throw new BankWasNotFoundException($"Bank with a name {name} does not exist");
        }

        public void RunTimeMechanism(int durationInDays)
        {
            for (int day = 0; day < durationInDays; day++)
            {
                CurrentDate = CurrentDate.AddDays(1);
                foreach (Bank bank in Banks)
                   bank.CheckNewDay();
            }
        }
    }
}