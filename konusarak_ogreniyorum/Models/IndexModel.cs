using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace konusarak_ogreniyorum.Models
{
    public class IndexModel
    {
        public string username { get; set; } 
        public string password { get; set; } 


        public static bool check_user(string username , string password)
        {
            string path = "Data source = db/db.db";
            SQLiteConnection con = new SQLiteConnection(path);
            con.Open();

            string query = "Select * from Users where USERNAME== '" + username + "'" + " and " + "PASSWORD =='" + password + "'";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();

            int count = 0;
            while (dr.Read())
            {
                count++;
            }
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
           


            /*DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);*/


            
            
          

        }
    }

    
}
