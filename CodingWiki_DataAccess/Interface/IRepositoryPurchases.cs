using Practice5_Model.Models;

namespace Practice5_DataAccess.Interface
{
    public interface IRepositoryPurchases
    {

        public List<Purchase> GetPurchases();
        public Purchase UpdatePurchase(int? id);
        public void UpdatePurchase(Purchase purchase);  
        public bool DeletePurchase(int? id);
    }
}