using System.Data;

namespace EmployeeManagement.DataAccessLayer.Infrastructure
{
    public interface IDataBaseConnection
    {
        IDbConnection Connection { get; }
    }
}
