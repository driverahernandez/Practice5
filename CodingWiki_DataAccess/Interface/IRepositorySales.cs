using Practice5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_DataAccess.Interface
{
    public interface IRepositorySales
    {
        public List<Sale> GetSales();
        public Sale UpdateSale(int? id);
        public void UpdateSale(Sale sale);
        public bool DeleteSale(int? id);
        
    }
}
