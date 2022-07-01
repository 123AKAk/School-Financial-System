using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adolphus
{
    class dtba
    {
        public static bool connectiontest;
        public static MySqlConnection connectToDb()
        {

            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=localhost;database=eyosms;uid=root;pwd=;";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();

                return cnn;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
