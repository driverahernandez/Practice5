using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_Model.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public double Total { get; set; }
        public DateOnly PurchaseDate { get; set; }
    }
}
