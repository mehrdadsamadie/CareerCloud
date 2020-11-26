using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository :BaseAdo, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[System_Language_Codes]
                           ([LanguageID]
                           ,[Name]
                           ,[Native_Name])
                     VALUES
                           (@LanguageID
                           ,@Name
                           ,@Native_Name)");
                    sqlCommand.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    sqlCommand.Parameters.AddWithValue("@Name", item.Name);
                    sqlCommand.Parameters.AddWithValue("@Native_Name", item.NativeName);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[System_Language_Codes]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [LanguageID]
                     ,[Name]
                     ,[Native_Name]
                 FROM [dbo].[System_Language_Codes]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                SystemLanguageCodePoco[] systemLanguageCodePocos = new SystemLanguageCodePoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newSystemLanguageCodePoco = new SystemLanguageCodePoco()
                    {
                        LanguageID = sqlDataReader.GetString(0),
                        Name = sqlDataReader.GetString(1),
                        NativeName = sqlDataReader.GetString(2),
                  
                    };
                    systemLanguageCodePocos[i++] = newSystemLanguageCodePoco;

                }
                sqlConnection.Close();
                return systemLanguageCodePocos;
            }

        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[System_Language_Codes]
                        WHERE [LanguageID] = @LanguageID");
                    sqlCommand.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[System_Language_Codes]
                    SET 
                       [Name] = @Name
                       ,[Native_Name] = @Native_Name
                     WHERE [LanguageID] = @LanguageID");
                    sqlCommand.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    sqlCommand.Parameters.AddWithValue("@Name", item.Name);
                    sqlCommand.Parameters.AddWithValue("@Native_Name", item.NativeName);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
