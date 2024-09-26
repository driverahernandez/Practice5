using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;

namespace Practice5_DataAccess.Data
{
    public class SalesRepositoryFactory : IRepositorySalesFactory
    {
        private readonly EFSalesRepository _efSalesRepository;
        private readonly ADOSalesRepository _adoSalesRepository;
        public SalesRepositoryFactory(EFSalesRepository efSalesRepository, ADOSalesRepository adoSalesRepository)
        {
            _adoSalesRepository = adoSalesRepository;
            _efSalesRepository = efSalesRepository;
        }
        public IRepositorySales GetSalesRepository()
        {
            if (AccessType.id == 0)
            {
                return _efSalesRepository;
            }
            else
            {
                return _adoSalesRepository;
            }
        }
    }
}
