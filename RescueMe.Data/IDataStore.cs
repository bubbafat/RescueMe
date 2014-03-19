using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Data
{
    public interface IDataStore : IDisposable
    {
        IQueryable<Registration> Registrations { get; }
        IQueryable<Rescue> Rescues { get; }

        void CancelAccount(string phoneNumer);
    }
}
