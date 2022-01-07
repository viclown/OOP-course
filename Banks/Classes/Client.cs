using System;
using System.Collections.Generic;
using Banks.Classes.BankAccount;
using Banks.Tools;

namespace Banks.Classes
{
    public class Client
    {
        public Client(string name, string surname, string address, long passport)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
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

        public void SetAddress(string address)
        {
            Address = address;
            if (Passport != 0)
                IsSuspicious = false;
        }

        public void SetPassport(long passport)
        {
            if (passport < Math.Pow(10, 9) || passport >= Math.Pow(10, 10))
                throw new InvalidPassportException("Passport consists of 10 numbers, please, make sure the entered data is correct");
            Passport = passport;

            if (Address != null)
                IsSuspicious = false;
        }
    }
}