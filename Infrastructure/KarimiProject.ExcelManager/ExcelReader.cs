using ExcelDataReader;
using KarimiProject.Entities;
using KarimiProject.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KarimiProject.ExcelManager
{
    public class ExcelReader : IFileReader
    {
        public List<Customer> ReadCustomersFromFile(string filePath)
         {
            List<Customer> customers = new List<Customer>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //todo: اگر سطر اول نام بود رد بشه
                    reader.Read();
                    while (reader.Read()) 
                    {
                        customers.Add(new Customer
                        {
                            Name = reader.GetValue(0).ToString(),
                            InputPhoneNumber = reader.GetValue(1).ToString(),
                        });
                    }
                }
            }

            return customers;
        }

    }
}
