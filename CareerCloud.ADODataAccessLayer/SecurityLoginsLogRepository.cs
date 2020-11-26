using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CareerCloud.DataAccessLayer;
using System.Data.SqlClient;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository : BaseAdo, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Security_Logins_Log]
                          ([Id]
                          ,[Login]
                          ,[Source_IP]
                          ,[Logon_Date]
                          ,[Is_Succesful])
                    VALUES
                          (@Id
                          ,@Login
                          ,@Source_IP
                          ,@Logon_Date
                          ,@Is_Succesful)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    sqlCommand.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    sqlCommand.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Security_Logins_Log]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Login]
                     ,[Source_IP]
                     ,[Logon_Date]
                     ,[Is_Succesful]
                 FROM [dbo].[Security_Logins_Log]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                SecurityLoginsLogPoco[] securityLoginsLogPocos = new SecurityLoginsLogPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newSecurityLoginsLogPoco = new SecurityLoginsLogPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Login = sqlDataReader.GetGuid(1),
                        SourceIP = sqlDataReader.GetString(2),
                        LogonDate=sqlDataReader.GetDateTime(3),
                        IsSuccesful = sqlDataReader.GetBoolean(4),
                    };
                    securityLoginsLogPocos[i++] = newSecurityLoginsLogPoco;

                }
                sqlConnection.Close();
                return securityLoginsLogPocos;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Security_Logins_Log]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Security_Logins_Log]
   SET 
      [Login] = @Login
      ,[Source_IP] = @Source_IP
      ,[Logon_Date] = @Logon_Date
      ,[Is_Succesful] = @Is_Succesful
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    sqlCommand.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    sqlCommand.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
