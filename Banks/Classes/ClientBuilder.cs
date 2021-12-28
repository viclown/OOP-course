namespace Banks.Classes
{
    public class ClientBuilder
    {
        private int _id = 1;
        private Client client;

        public ClientBuilder(Client client)
        {
            this.client = client;
        }

        public Client AddNewClient(string name, string surname)
        {
            SetName(name);
            SetSurname(surname);
            client.Id = _id++;
            return client;
        }

        private Client SetName(string name)
        {
            client.Name = name;
            client.Id = _id++;
            return client;
        }

        private Client SetSurname(string surname)
        {
            client.Surname = surname;
            return client;
        }
    }
}