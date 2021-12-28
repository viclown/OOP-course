using Banks.Services;
using Banks.Tools;

namespace Banks.Classes.BankAccount
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank)
            : base(client, 0, bank)
        {
        }
    }
}