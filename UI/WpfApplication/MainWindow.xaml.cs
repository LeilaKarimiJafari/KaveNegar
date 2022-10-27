using KarimiProject.Entities;
using KarimiProject.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication.Extension;
using System.Linq;


namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly IFileReader fileReader;
        private readonly IDistributedCache distributedCache;
        private readonly IDataService dbManager;

        public MainWindow(IFileReader fileReader, IDistributedCache distributedCache,IDataService dbManager)
        {
            InitializeComponent();
            this.fileReader = fileReader;
            this.distributedCache = distributedCache;
            this.dbManager = dbManager;
        }

        private void SetRedisButton_Click(object sender, RoutedEventArgs e)
        {
            var file_Customers = ReadDataFromExcel();

            long rowCount = 0;
            foreach (var fileCustomer in file_Customers)
            {
                string fileName = fileCustomer.Key;

                long cachKeyIndex = 0;
                foreach (var customer in fileCustomer.Value)
                {
                    string key = fileName + cachKeyIndex++;
                    Task.Run(async () => await distributedCache.SetDistributedCache<Customer>(key, customer, TimeSpan.FromHours(1)));
                }
                rowCount += fileCustomer.Value.Count;
            }
            InfoLabel.Content = $"All Files have been Read successfully, row count {rowCount}";

        }

        private void InsertDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            var file_Customers = ReadDataFromExcel();

            foreach (var file_Customer in file_Customers)
            {
                dbManager.SetCustomersInDataBase(file_Customer.Key, file_Customer.Value);
            }

            var rowCount = file_Customers.Values.Sum(cList => cList.Count);
            InfoLabel.Content = $"All Files Insert in DataBase successfully, row count {rowCount}";

        }

        private Dictionary<string, List<Customer>> ReadDataFromExcel()
        {
            Dictionary<string, List<Customer>> file_Customers = new Dictionary<string, List<Customer>>(); ;

            var files = Directory.GetFiles(@"D:\Develop\Kavenegar\DataExcel");

            int rowCount = 0;
            foreach (var filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                List<Customer> customers = fileReader.ReadCustomersFromFile(filePath);
                file_Customers.Add(fileName, customers);

                rowCount += customers.Count;
            }

            return file_Customers;
        }
    }
}
