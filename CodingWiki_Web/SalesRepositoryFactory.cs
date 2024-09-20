using Microsoft.AspNetCore.Authentication;
using NuGet.Configuration;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Interface;

namespace Practice5_Web
{
    public interface IRepositorySalesFactory {

        public IRepositorySales GetSalesRepository();
    }
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
