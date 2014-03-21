using System;
using System.Linq;

namespace RescueMe.Data
{
    public interface IDataStore : IDisposable
    {
        IQueryable<Registration> Registrations { get; }
        IQueryable<Rescue> Rescues { get; }

        void CancelAccount(string phoneNumer);

        void Register(string phoneNumber);
    }
}
