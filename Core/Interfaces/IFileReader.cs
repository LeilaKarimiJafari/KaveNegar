using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KarimiProject.Interfaces
{
    public interface IFileReader
    {
        public  List<Entities.Customer> ReadCustomersFromFile(string filePath);
    }
}
