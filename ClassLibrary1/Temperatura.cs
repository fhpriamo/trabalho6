using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho6.comum
{
    public class Temperatura
    {
        private double temperatura;

        public Temperatura(double temperatura)
        {
            this.temperatura = temperatura;
        }

        public double Celsius
        {
            get
            {
                return temperatura;
            }
        }

        public double Fahrenheit
        {
            get
            {
                return (((9 / 5) * temperatura) + 32.0);
            }
        }

        public  double Kelvin
        {
            get
            {
                return temperatura + 273.15;
            }
        }

        public static string KelvinUnit
        {
            get { return "°K"; }
        }

        public static string FahrenheitUnit
        {
            get { return "°F"; }
        }

        public static string CelsiusUnit
        {
            get { return "°C";  }
        }
    }
}
