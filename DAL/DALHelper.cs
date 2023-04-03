namespace CarInfo.DAL
{
    public class DALHelper
    {
        public static string connectionStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnectionString");

    }
}
