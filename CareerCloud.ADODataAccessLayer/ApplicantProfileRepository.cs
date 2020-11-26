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
    public class ApplicantProfileRepository :BaseAdo, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Profiles]
           ([Id]
           ,[Login]
           ,[Current_Salary]
           ,[Current_Rate]
           ,[Currency]
           ,[Country_Code]
           ,[State_Province_Code]
           ,[Street_Address]
           ,[City_Town]
           ,[Zip_Postal_Code])
     VALUES
           (@Id
           ,@Login
           ,@Current_Salary
           ,@Current_Rate
           ,@Currency
           ,@Country_Code
           ,@State_Province_Code
           ,@Street_Address
           ,@City_Town
           ,@Zip_Postal_Code)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary==null? DBNull.Value : (object)item.CurrentSalary);
                    sqlCommand.Parameters.AddWithValue("@Current_Rate", item.CurrentRate == null ? DBNull.Value : (object)item.CurrentRate);
                    sqlCommand.Parameters.AddWithValue("@Currency", (String.IsNullOrEmpty(item.Currency) ? DBNull.Value : (object)item.Currency));
                    sqlCommand.Parameters.AddWithValue("@Country_Code", (String.IsNullOrEmpty(item.Country) ? DBNull.Value : (object)item.Country));
                    sqlCommand.Parameters.AddWithValue("@State_Province_Code", (String.IsNullOrEmpty(item.Province) ? DBNull.Value : (object)item.Province));
                    sqlCommand.Parameters.AddWithValue("@Street_Address", (String.IsNullOrEmpty(item.Street) ? DBNull.Value : (object)item.Street));
                    sqlCommand.Parameters.AddWithValue("@City_Town", (String.IsNullOrEmpty(item.City) ? DBNull.Value : (object)item.City));
                    sqlCommand.Parameters.AddWithValue("@Zip_Postal_Code", (String.IsNullOrEmpty(item.PostalCode) ? DBNull.Value : (object)item.PostalCode));


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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Profiles]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                      ,[Login]
                      ,[Current_Salary]
                      ,[Current_Rate]
                      ,[Currency]
                      ,[Country_Code]
                      ,[State_Province_Code]
                      ,[Street_Address]
                      ,[City_Town]
                      ,[Zip_Postal_Code]
                      ,[Time_Stamp]
                  FROM [dbo].[Applicant_Profiles]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ApplicantProfilePoco[] applicantProfilePocos = new ApplicantProfilePoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newApplicantProfilePoco = new ApplicantProfilePoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Login = sqlDataReader.GetGuid(1),
                        CurrentSalary  = sqlDataReader.IsDBNull(2) ? (decimal?)null : sqlDataReader.GetDecimal(2),
                        CurrentRate = sqlDataReader.IsDBNull(3) ? (decimal?)null : sqlDataReader.GetDecimal(3),
                        Currency= sqlDataReader.IsDBNull(4) ? (string)null : sqlDataReader.GetString(4),
                        Country = sqlDataReader.IsDBNull(5) ? (string)null : sqlDataReader.GetString(5),
                        Province = sqlDataReader.IsDBNull(6) ? (string)null : sqlDataReader.GetString(6),
                        Street = sqlDataReader.IsDBNull(7) ? (string)null : sqlDataReader.GetString(7),
                        City = sqlDataReader.IsDBNull(8) ? (string)null : sqlDataReader.GetString(8),
                        PostalCode= sqlDataReader.IsDBNull(9) ? (string)null : sqlDataReader.GetString(9),
                        TimeStamp = (byte[])sqlDataReader[10],
                    };
                    applicantProfilePocos[i++] = newApplicantProfilePoco;

                }
                sqlConnection.Close();
                return applicantProfilePocos;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Profiles]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Profiles]
                     SET 
                         [Login] = @Login
                         ,[Current_Salary] = @Current_Salary
                         ,[Current_Rate] =@Current_Rate
                         ,[Currency] = @Currency
                         ,[Country_Code] =@Country_Code
                         ,[State_Province_Code] =@State_Province_Code 
                         ,[Street_Address] = @Street_Address
                         ,[City_Town] = @City_Town
                         ,[Zip_Postal_Code] =@Zip_Postal_Code 
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Login", item.Login);
                    sqlCommand.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary == null ? DBNull.Value : (object)item.CurrentSalary);
                    sqlCommand.Parameters.AddWithValue("@Current_Rate", item.CurrentRate == null ? DBNull.Value : (object)item.CurrentRate);
                    sqlCommand.Parameters.AddWithValue("@Currency", (String.IsNullOrEmpty(item.Currency) ? DBNull.Value : (object)item.Currency));
                    sqlCommand.Parameters.AddWithValue("@Country_Code", (String.IsNullOrEmpty(item.Country) ? DBNull.Value : (object)item.Country));
                    sqlCommand.Parameters.AddWithValue("@State_Province_Code", (String.IsNullOrEmpty(item.Province) ? DBNull.Value : (object)item.Province));
                    sqlCommand.Parameters.AddWithValue("@Street_Address", (String.IsNullOrEmpty(item.Street) ? DBNull.Value : (object)item.Street));
                    sqlCommand.Parameters.AddWithValue("@City_Town", (String.IsNullOrEmpty(item.City) ? DBNull.Value : (object)item.City));
                    sqlCommand.Parameters.AddWithValue("@Zip_Postal_Code", (String.IsNullOrEmpty(item.PostalCode) ? DBNull.Value : (object)item.PostalCode));



                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
