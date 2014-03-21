
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
