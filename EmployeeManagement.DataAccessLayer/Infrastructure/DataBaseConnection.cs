using EmployeeManagement.Entities.AppSettings;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement.DataAccessLayer.Infrastructure
{
    public class DataBaseConnection : IDataBaseConnection
    {
        public DataBaseConnection(AppSettings appsettings)
        {
            Connection = new SqlConnection(appsettings.ConnectionInfo.TransactionDatabase);
        }
        public IDbConnection Connection { get; }
    }
}
