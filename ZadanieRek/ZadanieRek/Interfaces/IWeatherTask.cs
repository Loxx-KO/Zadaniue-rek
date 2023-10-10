using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieRek.Data_access;

namespace ZadanieRek.Interfaces
{
    public interface IWeatherTask
    {
        public bool CheckTask();
        public void RunTask();
    }
}
