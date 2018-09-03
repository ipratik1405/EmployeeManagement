using EmployeeManagement.DataAccessLayer.Infrastructure;
using EmployeeManagement.Entities.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagement.DataAccess
{
    public class Database<T> : IDatabase<T> where T : IDisposable
    {
        private readonly AppSettings _appsettings;
        public Database(AppSettings appsettings)
        {
            _appsettings = appsettings;
        }

        public T Repository
        {
            get
            {
                var serviceProvider = new ServiceCollection()
                     .AddSingleton(typeof(T))
                     .AddSingleton(typeof(IDataBaseConnection), typeof(DataBaseConnection))
                     .AddSingleton(_appsettings)
                     .BuildServiceProvider();
                return serviceProvider.GetService<T>();
            }
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
