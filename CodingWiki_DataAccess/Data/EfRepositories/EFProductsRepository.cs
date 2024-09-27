using Practice5_DataAccess.Interface;
using Practice5_Model.Models;

namespace Practice5_DataAccess.Data.EfRepositories
{
    public class EFProductsRepository : IRepositoryProducts
    {
        private readonly ApplicationDbContext _db;

        public EFProductsRepository()
        {
            _db = new ApplicationDbContext();
        }


        public List<Product> GetProducts()
        {
            List<Product> objList = _db.Products.ToList();
            return objList;
        }

        public Product? GetProductById(int id)
        {
            var obj = _db.Products.FirstOrDefault(s => s.ProductId == id);

            return obj;
        }
        public void CreateProduct(Product product)
        {
            Product obj = new Product();
            obj.ProductId = product.ProductId;
            obj.ProductName = product.ProductName;
            obj.Price = product.Price;

            _db.Products.Add(obj);
            _db.SaveChanges();
        }
        public Product UpdateProduct(int? id)
        {
            Product obj = new();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            obj = _db.Products.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public void UpdateProduct(Product obj)
        {
            if (obj.ProductId == 0)
            {
                //create
                _db.Products.Add(obj);

            }
            else
            {
                //update
                _db.Products.Update(obj);
            }
            _db.SaveChanges();
            return;
        }
        public bool DeleteProduct(int? id)
        {
            bool isObjectFound = false;
            Product obj = new();
            obj = _db.Products.FirstOrDefault(g => g.ProductId == id);
            if (obj == null)
            {
                return isObjectFound;
            }
            isObjectFound = true;
            _db.Products.Remove(obj);
            _db.SaveChanges();
            return isObjectFound;
        }
    }
}
