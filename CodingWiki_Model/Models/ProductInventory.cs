using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_Model.Models
{
    public class ProductInventory
    {
        [Key]
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
