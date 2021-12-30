using System;
using System.Runtime.InteropServices;
using Banks.Tools;

namespace Banks.Classes
{
    public class ClientBuilder
    {
        public Client Client { get; private set; }

        public Client AddNewClient(string name, string surname)
        {
            Client = new Client(name, surname);
            return Client;
        }

        public void SetAddress(string address)
        {
            Client.Address = address;
            if (Client.Passport != 0)
                Client.IsSuspicious = false;
        }

        public void SetPassport(int passport)
        {
            if (passport < Math.Pow(10, 9) || passport >= Math.Pow(10, 10))
                throw new InvalidPassportException("Passport consists of 10 numbers, please, make sure the entered data is correct");
            Client.Passport = passport;

            if (Client.Address != null)
                Client.IsSuspicious = false;
        }
    }
}