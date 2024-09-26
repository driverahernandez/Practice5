using Practice5_DataAccess.Interface;
using Practice5_Model.Models;

namespace Practice5_DataAccess.Data.EfRepositories
{
    public class EFSalesRepository : IRepositorySales
    {
        private readonly ApplicationDbContext _db;

        public EFSalesRepository()
        {
            _db = new ApplicationDbContext();
        }
        public EFSalesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Sale> GetSales()
        {
            List<Sale> objList = _db.Sales.ToList();
            return objList;
        }
        public Sale? GetSaleById(int id)
        {
            var obj = _db.Sales.FirstOrDefault(s => s.SaleId==id);

            return obj;
        }
        public void CreateSale(Sale sale)
        {
            Sale obj = new Sale();
            obj.SaleId = 0;
            obj.ProductId = sale.ProductId;
            obj.Total = sale.Total;
            obj.SaleDate = sale.SaleDate;

            _db.Sales.Add(obj);
            _db.SaveChanges();
        }
        public Sale UpdateSale(int? id)
        {
            Sale obj = new();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            obj = _db.Sales.FirstOrDefault(g => g.SaleId == id);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public void UpdateSale(Sale obj)
        {
            if (obj.SaleId == 0)
            {
                //create
                _db.Sales.Add(obj);

            }
            else
            {
                //update
                _db.Sales.Update(obj);
            }
            _db.SaveChanges();
            return;
        }
        public bool DeleteSale(int? id)
        {
            bool isObjectFound = false;
            Sale obj = new();
            obj = _db.Sales.FirstOrDefault(g => g.SaleId == id);
            if (obj == null)
            {
                return isObjectFound;
            }
            isObjectFound = true;
            _db.Sales.Remove(obj);
            _db.SaveChanges();
            return isObjectFound;
        }
    }
}
