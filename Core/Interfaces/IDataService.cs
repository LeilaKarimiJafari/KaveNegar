using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KarimiProject.Interfaces
{
    public interface IDataService
    {
        public Task SetCustomersInDataBase(string fileName,List<Entities.Customer> customers);
    }
}
