using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_DataAccess.Data
{
    public class AccessType
    {
        //static properties to pass the current choice to all controllers (sales, purchases, products, and productsinventory)
        public static int id = 0;
        public static DataAccessChoice choice = DataAccessChoice.EF;

        //object properties defined to show on the list that is displayed in the website
        public int obj_id;
        public DataAccessChoice obj_choice;
        public bool obj_isSet; 

        public static List<AccessType> getAvailableTypes()
        {
            List<AccessType> availableTypes = new List<AccessType> { new AccessType { obj_id = 0, obj_choice = DataAccessChoice.EF, obj_isSet = (id==0)? true:false}, new AccessType { obj_id = 1, obj_choice = DataAccessChoice.ADO, obj_isSet = (id == 1)? true : false } };
            return availableTypes; 
        }
        public static void setAsEf()
        {
            id = 0;
            choice = DataAccessChoice.EF; 

        }
        public static void setAsAdo()
        {
            id = 1;
            choice = DataAccessChoice.ADO; 
        }
        public static void setTypeOfAcess(int input)
        {
            id = input;
            choice = (DataAccessChoice)input;
        }
    }
    public enum DataAccessChoice
    {
        EF, 
        ADO
    }
    
}
