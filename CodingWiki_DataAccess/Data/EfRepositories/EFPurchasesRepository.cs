using Practice5_DataAccess.Interface;
using Practice5_Model.Models;

namespace Practice5_DataAccess.Data.EfRepositories
{
    public class EFPurchasesRepository : IRepositoryPurchases
    {
        private readonly ApplicationDbContext _db;

        public EFPurchasesRepository()
        {
            _db = new ApplicationDbContext();
        }


        public List<Purchase> GetPurchases()
        {
            List<Purchase> objList = _db.Purchases.ToList();
            return objList;
        }

        public Purchase UpdatePurchase(int? id)
        {
            Purchase obj = new();
            if (id == null || id == 0)
            {
                //create 
                return obj;
            }
            //edit 
            obj = _db.Purchases.FirstOrDefault(g => g.PurchaseId == id);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }

        public void UpdatePurchase(Purchase obj)
        {
            if (obj.PurchaseId == 0)
            {
                //create
                _db.Purchases.Add(obj);

            }
            else
            {
                //update
                _db.Purchases.Update(obj);
            }
            _db.SaveChanges();
            return;
        }
        public bool DeletePurchase(int? id)
        {
            bool isObjectFound = false;
            Purchase obj = new();
            obj = _db.Purchases.FirstOrDefault(g => g.PurchaseId == id);
            if (obj == null)
            {
                return isObjectFound;
            }
            isObjectFound = true;
            _db.Purchases.Remove(obj);
            _db.SaveChanges();
            return isObjectFound;
        }
    }
}
