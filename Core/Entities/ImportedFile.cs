using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KarimiProject.Entities
{
    [Table("ImportedFile")]
    public class ImportedFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime ImportedTime { get; set; }
        public List<CustomerDbModel> Customers { get; set; }
    }
}
