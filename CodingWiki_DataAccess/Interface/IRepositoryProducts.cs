using Practice5_Model.Models;

namespace Practice5_DataAccess.Interface
{
    public interface IRepositoryProducts
    {
        public List<Product> GetProducts();
        public Product UpdateProduct(int? id);
        public void UpdateProduct(Product product);
        public bool DeleteProduct(int? id);
    }
}