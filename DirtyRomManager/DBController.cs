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
            string currDir = Directory.GetCurrentDirectory();
            string str;
            SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");
            str = "CREATE DATABASE Catalog ON PRIMARY " +
                  "FILENAME = '" + currDir + "\\MyCatalog.mdf', " +
                  "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%" +
                  "LOG ON (NAME = db_Log, " +
                  "SIZE = 1MB, " +
                  "MAXSIZE = 5MB, " +
                  "FILEGROWTH = 10%)";

            SqlCommand comm = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                comm.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }
    }
}
