using System;

namespace EmployeeManagement.DataAccess
{
    public interface IDatabase<out T> : IDisposable
    {
        T Repository { get; }
    }
}
