using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalho6.comum;

namespace trabalho6
{
    static class Sensor
    {
        public static Temperatura Leitura(TimeSpan horaDoDia)
        {
            int seconds = Convert.ToInt32(horaDoDia.TotalSeconds);
            double celsius = -10 * Math.Sin(0.01 * seconds);

            return new Temperatura(celsius);
        }
    }
}
