using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Classes.BankAccount;

namespace Banks.Services
{
    public class Bank
    {
        private int _lastBankId = 0;

        public Bank(string name, BankInterest interest, BankCommission commission, BankLimit limitForSuspiciousStudents, BankLimit limitForCreditAccount, DateTime currentDate)
        {
            Name = name;
            Id = _lastBankId++;
            Interest = interest;
            Commission = commission;
            LimitForSuspiciousClients = limitForSuspiciousStudents;
            LimitForCreditAccount = limitForCreditAccount;
            Clients = new List<Client>();
            CurrentDate = currentDate;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public BankInterest Interest { get; set; }
        public BankCommission Commission { get; set; }
        public BankLimit LimitForSuspiciousClients { get; set; }
        public BankLimit LimitForCreditAccount { get; set; }
        public List<Client> Clients { get; set; }
        public List<DebitAccount> DebitAccounts { get; set; } = new List<DebitAccount>();
        public List<DepositAccount> DepositAccounts { get; set; } = new List<DepositAccount>();
        public List<CreditAccount> CreditAccounts { get; set; } = new List<CreditAccount>();
        public DateTime CurrentDate { get; set; }

        public Client AddNewClientToBank(Client client)
        {
            Clients.Add(client);
            return client;
        }

        public void SendNotificationsToClients(string message, List<Client> clients)
        {
            foreach (Client client in clients.Where(client => client.IsSubscribedToNotifications))
            {
                client.Notifications.Add(message);
            }
        }

        public BankCommission SetNewBankCommission(BankCommission commission)
        {
            Commission = commission;
            string message = $"We have set new bank commissions for all our clients. New bank commission is {Commission.Value}";
            SendNotificationsToClients(message, Clients);
            return commission;
        }

        public BankLimit SetNewBankLimitForSuspiciousClients(BankLimit limitForSuspiciousClients)
        {
            LimitForSuspiciousClients = limitForSuspiciousClients;
            string message = $"We have set new bank limits for clients without passport or address information. New bank limit for these clients is {LimitForSuspiciousClients.Value}";
            SendNotificationsToClients(message, Clients);
            return limitForSuspiciousClients;
        }

        public BankLimit SetNewBankLimitForCreditAccount(BankLimit limitForCreditAccount)
        {
            LimitForCreditAccount = limitForCreditAccount;
            string message = $"We have set new bank limits for credit accounts. New bank limit for credit accounts is {LimitForCreditAccount.Value}";
            SendNotificationsToClients(message, Clients);
            return limitForCreditAccount;
        }

        public BankInterest SetNewBankInterest(BankInterest interest)
        {
            Interest = interest;
            string message = $"We have set new bank interests on balances. New bank interest is {Interest.Value}";
            SendNotificationsToClients(message, Clients);
            return interest;
        }

        public void CheckNewDay()
        {
            CurrentDate = CurrentDate.AddDays(1);
            foreach (Client client in Clients)
            {
                foreach (DebitAccount account in client.DebitAccounts)
                {
                    account.CheckNewDay();
                }

                foreach (CreditAccount account in client.CreditAccounts)
                {
                    account.CheckNewDay();
                }

                foreach (DepositAccount account in client.DepositAccounts)
                {
                    account.CheckNewDay();
                }
            }
        }
    }
}