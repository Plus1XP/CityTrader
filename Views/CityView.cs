using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    public class CityView
    {
        public string CurrentCity { get; set; } = "London";

        public void Display(string display)
        {
            Console.WriteLine(display);
        }
    }
}
