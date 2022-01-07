using System;
using System.Runtime.InteropServices;
using Banks.Tools;

namespace Banks.Classes
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private long _passport;

        public void SetNameAndSurname(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public void SetAddress(string address)
        {
            _address = address;
        }

        public void SetPassport(long passport)
        {
            if (passport < Math.Pow(10, 9) || passport >= Math.Pow(10, 10))
                throw new InvalidPassportException("Passport consists of 10 numbers, please, make sure the entered data is correct");
            _passport = passport;
        }

        public Client Build()
        {
            return new Client(_name, _surname, _address, _passport);
        }
    }
}