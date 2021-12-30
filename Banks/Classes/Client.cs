using System.Collections.Generic;
using Banks.Classes.BankAccount;

namespace Banks.Classes
{
    public class Client
    {
        private int _id = 1;

        public Client(string name, string surname)
        {
            Name = name;
            Surname = surname;
            Id = _id++;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public long Passport { get; set; }
        public int Id { get; set; }
        public bool IsSuspicious { get; set; } = true;
        public bool IsSubscribedToNotifications { get; set; } = false;
        public List<DebitAccount> DebitAccounts { get; set; } = new List<DebitAccount>();
        public List<DepositAccount> DepositAccounts { get; set; } = new List<DepositAccount>();
        public List<CreditAccount> CreditAccounts { get; set; } = new List<CreditAccount>();
        public List<string> Notifications { get; set; }

        public void SubscribeToNotifications()
        {
            IsSubscribedToNotifications = true;
        }
    }
}