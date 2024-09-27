using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;

namespace Practice5_DataAccess.Data.RepositoryFactory
{
    public class ProductsInventoryRepositoryFactory : IRepositoryProductsInventoryFactory
    {
        private readonly EFProductsInventoryRepository _efProductsInventoryRepository;
        private readonly ADOProductsInventoryRepository _adoProductsInventoryRepository;
        public ProductsInventoryRepositoryFactory(EFProductsInventoryRepository efProductsInventoryRepository, ADOProductsInventoryRepository adoProductsInventoryRepository)
        {
            _adoProductsInventoryRepository = adoProductsInventoryRepository;
            _efProductsInventoryRepository = efProductsInventoryRepository;
        }
        public IRepositoryProductsInventory GetProductsInventoryRepository()
        {
            if (AccessType.id == 0)
            {
                return _efProductsInventoryRepository;
            }
            else
            {
                return _adoProductsInventoryRepository;
            }
        }
    }
}
