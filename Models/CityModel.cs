using System.Collections.Generic;

namespace Models
{
    public class CityModel
    {
        public List<CityModel> cities = new List<CityModel>();

        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityMessage { get; set; }

        public CityModel(int id, string name, string message)
        {
            this.CityID = id;
            this.CityName = name;
            this.CityMessage = message;
        }

        public CityModel()
        {
            AddCity(new CityModel(1, "London", "Welcome to London"));
            AddCity(new CityModel(2, "Paris", "Welcome to Paris"));
            AddCity(new CityModel(3, "Berlin", "Welcome to Berlin"));
            AddCity(new CityModel(4, "Madrid", "Welcome to Madrid"));
            AddCity(new CityModel(5, "Milan", "Welcome to Milan"));
            AddCity(new CityModel(6, "New York", "Welcome to New York"));
            AddCity(new CityModel(7, "Tokyo", "Welcome to Tokyo"));
            AddCity(new CityModel(8, "Hong Kong", "Welcome to Hong Kong"));
        }

        public void AddCity(CityModel City)
        {
            cities.Add(City);
        }

        public IEnumerable<CityModel> GetAllCities()
        {
            return cities;
        }
    }
}
