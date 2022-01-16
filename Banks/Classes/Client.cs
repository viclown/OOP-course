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

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public long Passport { get; }
        public int Id { get; set; }
        public bool IsSuspicious => Passport == 0 || Address == null;
        public List<Account> Accounts { get; } = new List<Account>();
        public List<string> Notifications { get; } = new List<string>();
    }
}