using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CareerCloud.DataAccessLayer;
using System.Linq;
using System.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository :BaseAdo, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Profiles]
                           ([Id]
                           ,[Registration_Date]
                           ,[Company_Website]
                           ,[Contact_Phone]
                           ,[Contact_Name]
                           ,[Company_Logo])
                     VALUES
                           (@Id
                           ,@Registration_Date
                           ,@Company_Website
                           ,@Contact_Phone
                           ,@Contact_Name
                           ,@Company_Logo)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    sqlCommand.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    sqlCommand.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    sqlCommand.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);


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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Profiles]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Registration_Date]
                     ,[Company_Website]
                     ,[Contact_Phone]
                     ,[Contact_Name]
                     ,[Company_Logo]
                     ,[Time_Stamp]
                 FROM [dbo].[Company_Profiles]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyProfilePoco[] companyProfilePocos = new CompanyProfilePoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newcompanyProfilePocos = new CompanyProfilePoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        RegistrationDate = sqlDataReader.GetDateTime(1),
                        CompanyWebsite = sqlDataReader.IsDBNull(2) ? null : (string)sqlDataReader[2],
                        ContactPhone = sqlDataReader.GetString(3),
                        ContactName =  sqlDataReader.IsDBNull(4) ? null : (string)sqlDataReader[4],
                        CompanyLogo = Convert.IsDBNull(sqlDataReader[5])? null: (byte[])sqlDataReader[5],
                        TimeStamp = (byte[])sqlDataReader[6],
                    };
                    companyProfilePocos[i++] = newcompanyProfilePocos;

                }
                sqlConnection.Close();
                return companyProfilePocos;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Profiles]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Profiles]
                     SET 
                        [Registration_Date] = @Registration_Date
                        ,[Company_Website] =@Company_Website
                        ,[Contact_Phone] = @Contact_Phone
                        ,[Contact_Name] = @Contact_Name
                        ,[Company_Logo] = @Company_Logo
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    sqlCommand.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    sqlCommand.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    sqlCommand.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    sqlCommand.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
