using Practice5_DataAccess.Interface;
using Practice5_Model.Models;

namespace Practice5_DataAccess.Data.EfRepositories
{
    public class EFProductsInventoryRepository : IRepositoryProductsInventory
    {
        private readonly ApplicationDbContext _db;

        public EFProductsInventoryRepository()
        {
            _db = new ApplicationDbContext();
        }


        public List<ProductInventory> GetProductsInventory()
        {
            List<ProductInventory> objList = _db.ProductsInventory.ToList();
            return objList;
        }

        public ProductInventory UpdateProductInventory(int? id)
        {
            ProductInventory obj = new();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            obj = _db.ProductsInventory.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public void UpdateProductInventory(ProductInventory obj)
        {
            if (obj.ProductId == 0)
            {
                //create
                _db.ProductsInventory.Add(obj);

            }
            else
            {
                //update
                _db.ProductsInventory.Update(obj);
            }
            _db.SaveChanges();
            return;
        }
        public bool DeleteProductInventory(int? id)
        {
            bool isObjectFound = false;
            ProductInventory obj = new();
            obj = _db.ProductsInventory.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return isObjectFound;
            }
            isObjectFound = true;
            _db.ProductsInventory.Remove(obj);
            _db.SaveChanges();
            return isObjectFound;
        }
    }
}
