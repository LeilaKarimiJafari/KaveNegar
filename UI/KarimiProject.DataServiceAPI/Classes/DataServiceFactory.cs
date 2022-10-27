using KarimiProject.Interfaces;
using KarimiProject.RedisService;
using KarimiProject.SqlService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarimiProject.DataServiceAPI.Classes
{
    public class DataServiceFactory:IDataServiceFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DataServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        public IDataService GetDataService(DataServiceType dataServiceType)
        {
            switch (dataServiceType)
            {
                case DataServiceType.Sql:
                    return (IDataService)serviceProvider.GetService(typeof(SqlDataService));

                default:
                case DataServiceType.Redis:
                    return (IDataService)serviceProvider.GetService(typeof(RedisDataService));
            }
        }
    }
}
