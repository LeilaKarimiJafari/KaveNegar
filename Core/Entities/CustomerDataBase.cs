using System.ComponentModel.DataAnnotations.Schema;


namespace KarimiProject.Entities
{
    [Table("Customer")]
    public class CustomerDbModel : Customer
    {
        public int ID { get; set; }
        public int ImportedFileID { get; set; }
        public ImportedFile ImportedFile { get; set; }
    }
}
