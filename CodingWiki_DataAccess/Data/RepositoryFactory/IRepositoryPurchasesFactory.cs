using Practice5_DataAccess.Interface;

namespace Practice5_DataAccess.Data.RepositoryFactory
{
    public interface IRepositoryPurchasesFactory
    {
        IRepositoryPurchases GetPurchasesRepository();
    }
}