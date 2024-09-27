using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;

namespace Practice5_DataAccess.Data.RepositoryFactory
{
    public class ProductsRepositoryFactory : IRepositoryProductsFactory
    {
        private readonly EFProductsRepository _efProductsRepository;
        private readonly ADOProductsRepository _adoProductsRepository;
        public ProductsRepositoryFactory(EFProductsRepository efProductsRepository, ADOProductsRepository adoProductsRepository)
        {
            _adoProductsRepository = adoProductsRepository;
            _efProductsRepository = efProductsRepository;
        }
        public IRepositoryProducts GetProductsRepository()
        {
            if (AccessType.id == 0)
            {
                return _efProductsRepository;
            }
            else
            {
                return _adoProductsRepository;
            }
        }
    }
}
