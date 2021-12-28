using System;
using Banks.Tools;

namespace Banks.Classes
{
    public class ConfirmedClientBuilder
    {
        private Client client;

        public ConfirmedClientBuilder(Client client)
        {
            this.client = client;
        }

        public Client SetAddress(string address)
        {
            client.Address = address;
            if (client.Passport != 0)
                client.IsSuspicious = false;
            return client;
        }

        public Client SetPassport(long passport)
        {
            if (passport < Math.Pow(10, 9) || passport >= Math.Pow(10, 10))
                throw new InvalidPassportException("Passport consists of 10 numbers, please, make sure the entered data is correct");
            client.Passport = passport;

            if (client.Address != null)
                client.IsSuspicious = false;
            return client;
        }
    }
}