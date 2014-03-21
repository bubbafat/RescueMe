using System;
using System.Data.Entity;
using System.Linq;

namespace RescueMe.Data
{
    public sealed class RescueMeDb : DbContext, IDataStore
    {
        public RescueMeDb()
            : base("DefaultConnection")
        {

        }

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
                foreach(var rescue in Rescues.Where(r => r.Who.Id == reg.Id).ToList())
                {
                    Rescues.Remove(rescue);
                }

                Registrations.Remove(reg);
            }

            SaveChanges();
        }

        void IDataStore.Register(string phoneNumer)
        {
            Registrations.Add(new Registration
                {
                    Number = phoneNumer,
                    Registered = DateTimeOffset.UtcNow,
                });

            SaveChanges();
        }
    }
}
