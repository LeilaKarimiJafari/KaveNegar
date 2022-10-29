using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KarimiProject.DataServiceAPI.Classes;
using KarimiProject.Entities;
using KarimiProject.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarimiProject.DataServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataServiceController : ControllerBase
    {
        private readonly IFileReader fileReader;
        private readonly IDataServiceFactory dataServiceFactory;
        private readonly IWebHostEnvironment hostEnvironment;

        public DataServiceController(IFileReader fileReader, IDataServiceFactory dataServiceFactory, IWebHostEnvironment hostEnvironment)
        {
            this.fileReader = fileReader;
            this.dataServiceFactory = dataServiceFactory;
            this.hostEnvironment = hostEnvironment;

        }
        [HttpPost()]
        public async Task<ActionResult<string>> SetCustomersInDataBase(string folderName, DataServiceType serviceType)
        {
            try
            {
                var file_Customers = ReadDataFromExcel(folderName);

                foreach (var file_Customer in file_Customers)
                {
                    await dataServiceFactory.GetDataService(serviceType).SetCustomersInDataBase(file_Customer.Key, file_Customer.Value);
                }

                return Ok("All data submit successfully");
            }
            catch (Exception)
            {
                return BadRequest("Data submition failed");
            }
        }

        private Dictionary<string, List<Customer>> ReadDataFromExcel(string folderName)
        {
            Dictionary<string, List<Customer>> file_Customers = new Dictionary<string, List<Customer>>();
            var directoryPath = Path.Combine(hostEnvironment.ContentRootPath, "Files", folderName);
            var files = Directory.GetFiles(directoryPath);

            foreach (var filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                List<Customer> customers = fileReader.ReadCustomersFromFile(filePath);
                file_Customers.Add(fileName, customers);
            }

            return file_Customers;
        }
    }
}