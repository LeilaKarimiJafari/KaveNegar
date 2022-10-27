using KarimiProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KarimiProject.DataServiceAPI.Classes
{
    public interface IDataServiceFactory
    {
        public IDataService GetDataService(DataServiceType databaseType);
    }
}
