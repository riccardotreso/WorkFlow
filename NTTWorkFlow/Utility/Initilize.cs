using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace NTTWorkFlow.Utility
{
    public class Initilize
    {

        /// <summary>
        /// Create the DB structure
        /// </summary>
        /// <param name="connectionString">The connection string of the database</param>
        public void CreateDataStructure(string connectionString) {

            string scriptDataBase,
                striptTables;
            using (StreamReader sr = new StreamReader("SQL\\database.sql"))
            {
                scriptDataBase = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("SQL\\tables.sql"))
            {
                striptTables = sr.ReadToEnd();
            }


            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                if (sqlConn.State != System.Data.ConnectionState.Open) sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;

                sqlCommand.CommandText = scriptDataBase;
                sqlCommand.ExecuteNonQuery();

                sqlCommand.CommandText = striptTables;
                sqlCommand.ExecuteNonQuery();

                if (sqlConn.State == System.Data.ConnectionState.Open) sqlConn.Close();
                

            }

        }
    }
}
