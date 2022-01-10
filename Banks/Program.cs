using System;
using Banks.Classes;
using Banks.Classes.BankAccount;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var centralBank = new CentralBank();
            Bank tinkoff = centralBank.CreateNewBank("Tinkoff", 3.65, 30, 15000, 100000);

            var builder = new ClientBuilder();
            builder.SetNameAndSurname("Dmitri", "Ivanov");
            builder.SetAddress("Nevskii prospekt 10");
            builder.SetPassport(1234567890);
            Client dima = builder.BuildClient();

            tinkoff.AddNewClientToBank(dima);
            CreditAccount account = centralBank.CreateCreditAccount(dima, tinkoff);

            account.GetMoneyFromAccount(80000);
            centralBank.RunTimeMechanism(365);
            Console.WriteLine(DateTime.Today.Day);
        }
    }
}
