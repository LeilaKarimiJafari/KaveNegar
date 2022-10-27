using KarimiProject.Entities;
using KarimiProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace KarimiProject.SqlService
{
    public class SqlDataService : IDataService
    {
        private readonly MainDbContext dbContext;

        public SqlDataService(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SetCustomersInDataBase(string fileName, List<Customer> customers)
        {
            var dbImportedFile = dbContext.ImportedFiles.FirstOrDefault(f=>f.Name==fileName);
            if (dbImportedFile != null)
                return;

            ImportedFile importedFile = new ImportedFile() { ImportedTime = DateTime.Now, Name = fileName };
            dbContext.ImportedFiles.Add(importedFile);

            var dbCustomers = customers.Select(c => new CustomerDbModel()
            {
                ImportedFile = importedFile,
                InputPhoneNumber = c.InputPhoneNumber,
                Name = c.Name
            }); ;

            await dbContext.Customers.AddRangeAsync(dbCustomers);

            await dbContext.SaveChangesAsync();
        }
    }
}
