using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;

namespace Practice5_DataAccess.Data.RepositoryFactory
{
    public class PurchasesRepositoryFactory : IRepositoryPurchasesFactory
    {
        private readonly EFPurchasesRepository _efPurchasesRepository;
        private readonly ADOPurchasesRepository _adoPurchasesRepository;
        public PurchasesRepositoryFactory(EFPurchasesRepository efPurchasesRepository, ADOPurchasesRepository adoPurchasesRepository)
        {
            _adoPurchasesRepository = adoPurchasesRepository;
            _efPurchasesRepository = efPurchasesRepository;
        }
        public IRepositoryPurchases GetPurchasesRepository()
        {
            if (AccessType.id == 0)
            {
                return _efPurchasesRepository;
            }
            else
            {
                return _adoPurchasesRepository;
            }
        }
    }
}
