using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Classes.BankAccount;

namespace Banks.Services
{
    public class Bank
    {
        private int _lastClientId = 0;
        private NotificationSystem _notificationSystem = new NotificationSystem();
        private List<Client> _subscribedClients = new List<Client>();

        public Bank(string name, BankInterest interest, BankCommission commission, BankLimit limitForSuspiciousStudents, BankLimit limitForCreditAccount, int id)
        {
            Name = name;
            Id = id;
            Interest = interest;
            Commission = commission;
            LimitForSuspiciousClients = limitForSuspiciousStudents;
            LimitForCreditAccount = limitForCreditAccount;
        }

        public string Name { get; set; }
        public int Id { get; }
        public BankInterest Interest { get; private set; }
        public BankCommission Commission { get; private set; }
        public BankLimit LimitForSuspiciousClients { get; private set; }
        public BankLimit LimitForCreditAccount { get; private set; }
        public List<Client> Clients { get; } = new List<Client>();
        public DateTime CurrentDate { get; set; }
        public List<Account> Accounts { get; } = new List<Account>();

        public Client AddNewClientToBank(Client client)
        {
            Clients.Add(client);
            client.Id = _lastClientId++;
            return client;
        }

        public BankCommission SetNewBankCommission(BankCommission commission)
        {
            Commission = commission;
            string message = $"We have set new bank commissions for our clients with credit cards. New bank commission is {Commission.Value}";
            _notificationSystem.SendNotification(_subscribedClients, message);
            return commission;
        }

        public BankLimit SetNewBankLimitForSuspiciousClients(BankLimit limitForSuspiciousClients)
        {
            LimitForSuspiciousClients = limitForSuspiciousClients;
            string message = $"We have set new bank limits for clients without passport or address information. New bank limit for these clients is {LimitForSuspiciousClients.Value}";
            _notificationSystem.SendNotification(_subscribedClients, message);
            return limitForSuspiciousClients;
        }

        public BankLimit SetNewBankLimitForCreditAccount(BankLimit limitForCreditAccount)
        {
            LimitForCreditAccount = limitForCreditAccount;
            string message = $"We have set new bank limits for credit accounts. New bank limit for credit accounts is {LimitForCreditAccount.Value}";
            _notificationSystem.SendNotification(_subscribedClients, message);
            return limitForCreditAccount;
        }

        public BankInterest SetNewBankInterest(BankInterest interest)
        {
            Interest = interest;
            string message = $"We have set new bank interests on balances. New bank interest is {Interest.Value}";
            _notificationSystem.SendNotification(_subscribedClients, message);
            return interest;
        }

        public void SubscribeToNotifications(Client client)
        {
            _subscribedClients.Add(client);
        }

        public void UnsubscribeFromNotifications(Client client)
        {
            _subscribedClients.Remove(client);
        }

        public void RunNewDay(DateTime currentDate)
        {
            CurrentDate = currentDate;
            foreach (Account account in Accounts)
            {
                account.RunNewDay();
            }
        }
    }
}