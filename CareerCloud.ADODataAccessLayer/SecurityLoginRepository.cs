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
    public class SecurityLoginRepository :BaseAdo, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Security_Logins]
                           ([Id]
                           ,[Login]
                           ,[Password]
                           ,[Created_Date]
                           ,[Password_Update_Date]
                           ,[Agreement_Accepted_Date]
                           ,[Is_Locked]
                           ,[Is_Inactive]
                           ,[Email_Address]
                           ,[Phone_Number]
                           ,[Full_Name]
                           ,[Force_Change_Password]
                           ,[Prefferred_Language])
                     VALUES
                           (@Id
                           ,@Login
                           ,@Password
                           ,@Created_Date
                           ,@Password_Update_Date
                           ,@Agreement_Accepted_Date
                           ,@Is_Locked
                           ,@Is_Inactive
                           ,@Email_Address
                           ,@Phone_Number
                           ,@Full_Name
                           ,@Force_Change_Password
                           ,@Prefferred_Language)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Password", item.Password);
                    sqlCommand.Parameters.AddWithValue("@Created_Date", item.Created);
                    sqlCommand.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate==null?DBNull.Value:(object)item.PasswordUpdate);
                    sqlCommand.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted == null ? DBNull.Value : (object)item.AgreementAccepted);
                    sqlCommand.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    sqlCommand.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    sqlCommand.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Full_Name", item.FullName);
                    sqlCommand.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    sqlCommand.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Security_Logins]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Login]
                     ,[Password]
                     ,[Created_Date]
                     ,[Password_Update_Date]
                     ,[Agreement_Accepted_Date]
                     ,[Is_Locked]
                     ,[Is_Inactive]
                     ,[Email_Address]
                     ,[Phone_Number]
                     ,[Full_Name]
                     ,[Force_Change_Password]
                     ,[Prefferred_Language]
                     ,[Time_Stamp]
                 FROM [dbo].[Security_Logins]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                SecurityLoginPoco[] securityLoginPocos = new SecurityLoginPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newSecurityLoginPoco = new SecurityLoginPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Login = sqlDataReader.GetString(1),
                        Password = sqlDataReader.GetString(2),
                        Created = sqlDataReader.GetDateTime(3),
                        PasswordUpdate = sqlDataReader.IsDBNull(4) ? (DateTime?)null : (DateTime?)sqlDataReader[4],
                        AgreementAccepted = sqlDataReader.IsDBNull(5) ? (DateTime?)null : (DateTime?)sqlDataReader[5],
                        IsLocked = sqlDataReader.GetBoolean(6),
                        IsInactive = sqlDataReader.GetBoolean(7),
                        EmailAddress = sqlDataReader.GetString(8),
                        PhoneNumber = sqlDataReader.IsDBNull(9) ? null : (string)sqlDataReader[9],
                        FullName = sqlDataReader.IsDBNull(10) ? null : (string)sqlDataReader[10],
                        ForceChangePassword = sqlDataReader.GetBoolean(11),
                        PrefferredLanguage = sqlDataReader.IsDBNull(12) ? null : (string)sqlDataReader[12],
                        TimeStamp = (byte[])sqlDataReader[13],
                    };
                    securityLoginPocos[i++] = newSecurityLoginPoco;

                }
                sqlConnection.Close();
                return securityLoginPocos;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Security_Logins]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Security_Logins]
                     SET 
                        [Login] = @Login
                        ,[Password] = @Password
                        ,[Created_Date] = @Created_Date
                        ,[Password_Update_Date] = @Password_Update_Date
                        ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                        ,[Is_Locked] = @Is_Locked
                        ,[Is_Inactive] = @Is_Inactive
                        ,[Email_Address] = @Email_Address
                        ,[Phone_Number] = @Phone_Number
                        ,[Full_Name] = @Full_Name
                        ,[Force_Change_Password] = @Force_Change_Password
                        ,[Prefferred_Language] = @Prefferred_Language
                     WHERE [Id] = @Id");

                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Password", item.Password);
                    sqlCommand.Parameters.AddWithValue("@Created_Date", item.Created);
                    sqlCommand.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate == null ? DBNull.Value : (object)item.PasswordUpdate);
                    sqlCommand.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted == null ? DBNull.Value : (object)item.AgreementAccepted);
                    sqlCommand.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    sqlCommand.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    sqlCommand.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Full_Name", item.FullName);
                    sqlCommand.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    sqlCommand.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
