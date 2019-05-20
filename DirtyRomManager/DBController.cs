using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirtyRomManager
{
    class DBController : DBInterface
    {
        public void checkDatabase()
        {
            string connStr = "Data Source = Collection.sdf; Password = pass";
            if (!File.Exists("Collection.sdf"))
            {
                try
                {
                    
                }
            }
        }
    }
}
