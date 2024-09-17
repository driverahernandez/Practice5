using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice5_DataAccess.Data.AdoRepositories
{
    public class AdoRepository
    {
        public const string connectionString = "Server=USQRODRIVERAHE1;Database=Practice5;" +
            "TrustServerCertificate=True;Trusted_Connection=True;";

        public string queryString;
    }
}
