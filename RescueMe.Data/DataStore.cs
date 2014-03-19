using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Data
{
    public static class DataStore
    {
        public static IDataStore GetInstance()
        {
            return new RescueMeDb();
        }
    }
}
