using Practice5_Model.Models;

namespace Practice5_DataAccess.Interface
{
    public interface IRepositoryProductsInventory
    {
        public List<ProductInventory> GetProductsInventory();
        public ProductInventory UpdateProductInventory(int? id);
        public void UpdateProductInventory(ProductInventory productInventory);
        public bool DeleteProductInventory(int? id);
        void CreateProductInventory(ProductInventory product);
        ProductInventory GetProductInventoryById(int id);
    }
}