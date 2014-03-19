using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueMe.Data
{
    public sealed class RescueMeDb : DbContext, IDataStore
    {
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Rescue> Rescues { get; set; }

        IQueryable<Registration> IDataStore.Registrations
        {
            get { return Registrations; }
        }

        IQueryable<Rescue> IDataStore.Rescues
        {
            get { return Rescues; }
        }

        void IDataStore.CancelAccount(string phoneNumer)
        {
            foreach(var reg in Registrations.Where(r => r.Number == phoneNumer).ToList())
            {
                foreach(var rescue in Rescues.Where(r => r.Who == reg).ToList())
                {
                    Rescues.Remove(rescue);
                }

                Registrations.Remove(reg);
            }

            SaveChanges();
        }
    }
}
