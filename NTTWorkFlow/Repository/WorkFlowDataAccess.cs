using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using NTTWorkFlow.Models;
using System.Data;

namespace NTTWorkFlow.Repository
{
    internal class WorkFlowDataAccess
    {
        private static string _connectionString = "";

        public WorkFlowDataAccess(String connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Step> GetByRefID(String refID) {
            List<Step> result = new List<Step>();
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                if (sqlConn.State != System.Data.ConnectionState.Open) sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;

                sqlCommand.CommandText = "select * from dbo.Task where [RefID] = @refID and complete = 0";
                sqlCommand.Parameters.Add(new SqlParameter("@refID", refID));
                using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
                    while (reader.Read())
                    {
                        result.Add(new Step()
                        {
                            ID = (int)reader["TaskID"],
                            Name = (string)reader["Name"]                           
                        });
                    }
                }
                
                if (sqlConn.State == System.Data.ConnectionState.Open) sqlConn.Close();

            }
            return result;
        }


        public bool CompleteTask(int ID, string refID) {
            bool result;
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                if (sqlConn.State != System.Data.ConnectionState.Open) sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;

                sqlCommand.CommandText = "update dbo.Task set complete = 1 where [TaskID] = @TeskID AND [RefID] = @RefID";
                sqlCommand.Parameters.Add(new SqlParameter("@TeskID", SqlDbType.Int)).Value = ID;
                sqlCommand.Parameters.Add(new SqlParameter("@RefID", SqlDbType.NVarChar, 8000)).Value = refID;
                result = sqlCommand.ExecuteNonQuery() == 1;

                if (sqlConn.State == System.Data.ConnectionState.Open) sqlConn.Close();

            }
            return result;
        }


        public bool InsertTask(Step newStep, String refID, String userID)
        {
            bool result;
            using (SqlConnection sqlConn = new SqlConnection(_connectionString))
            {
                if (sqlConn.State != System.Data.ConnectionState.Open) sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;

                sqlCommand.CommandText = @"INSERT INTO [dbo].[Task] ([TaskID],[Name],[RefID],[Complete],[CreationDate],[AssignedTo],[DueDate],[ApproverComments])
                VALUES(@TeskID, @Name, @RefID, @Complete, @CreationDate, @AssignedTo, null, null)";

                sqlCommand.Parameters.Add(new SqlParameter("@TeskID", SqlDbType.Int)).Value = newStep.ID;
                sqlCommand.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 500)).Value = newStep.Name;
                sqlCommand.Parameters.Add(new SqlParameter("@RefID", SqlDbType.NVarChar, 8000)).Value = refID;
                sqlCommand.Parameters.Add(new SqlParameter("@Complete", SqlDbType.Bit)).Value = false;
                sqlCommand.Parameters.Add(new SqlParameter("@CreationDate", SqlDbType.DateTime)).Value = DateTime.Now;
                sqlCommand.Parameters.Add(new SqlParameter("@AssignedTo", SqlDbType.NVarChar, 500)).Value = userID;

                result = sqlCommand.ExecuteNonQuery() == 1;

                if (sqlConn.State == System.Data.ConnectionState.Open) sqlConn.Close();

            }
            return result;
        }
    }
}
