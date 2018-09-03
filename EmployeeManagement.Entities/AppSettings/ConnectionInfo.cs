namespace EmployeeManagement.Entities.AppSettings
{
    public class ConnectionInfo
    {
        public string TransactionDatabase { get; set; }
        public string LoggingDatabase { get; set; }
        public string RedisDatabase { get; set; }

    }
}
