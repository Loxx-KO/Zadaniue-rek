using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;

namespace ZadanieRek.Interfaces
{
    public interface IWeatherData
    {
        public IWeatherData ReadFile(string filePath);
        public void SaveFile(long dt);

    }
}
