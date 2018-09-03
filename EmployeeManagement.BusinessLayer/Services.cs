using EmployeeManagement.BusinessLayer;
using EmployeeManagement.DataAccess;
using EmployeeManagement.Entities.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagement.Business
{
    public class Services<T> : IServices<T>
    {
        private readonly AppSettings _appsettings;
        public Services(AppSettings appsettings)
        {
            _appsettings = appsettings;
        }

        public T Service
        {
            get
            {
                var serviceRequest = new ServiceCollection()
                   .AddSingleton(typeof(IDatabase<>), typeof(Database<>))
                    .AddSingleton(_appsettings)
                    .AddSingleton(typeof(T))
                    .BuildServiceProvider();
                return (T)Convert.ChangeType(serviceRequest
                    .GetService<T>(), typeof(T));
            }

        }
    }
}
