namespace Models
{
    using System.Collections.Generic;

    public class City
    {
        private List<City> cities = new List<City>();

        public City()
        {
            this.AddCity(new City(1, "London", "Welcome to London"));
            this.AddCity(new City(2, "Paris", "Welcome to Paris"));
            this.AddCity(new City(3, "Berlin", "Welcome to Berlin"));
            this.AddCity(new City(4, "Madrid", "Welcome to Madrid"));
            this.AddCity(new City(5, "Milan", "Welcome to Milan"));
            this.AddCity(new City(6, "New York", "Welcome to New York"));
            this.AddCity(new City(7, "Tokyo", "Welcome to Tokyo"));
            this.AddCity(new City(8, "Hong Kong", "Welcome to Hong Kong"));
        }

        private City(int id, string name, string welcomeMessage)
        {
            this.ID = id;
            this.Name = name;
            this.WelcomeMessage = welcomeMessage;
        }

        public int ID { get; private set; }

        public string Name { get; private set; }

        public string WelcomeMessage { get; private set; }

        public IEnumerable<City> GetAllCities()
        {
            return this.cities;
        }

        private void AddCity(City city)
        {
            this.cities.Add(city);
        }
    }
}
