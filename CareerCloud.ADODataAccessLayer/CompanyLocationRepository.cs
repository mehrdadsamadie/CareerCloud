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
    public class CompanyLocationRepository :BaseAdo, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Locations]
                           ([Id]
                           ,[Company]
                           ,[Country_Code]
                           ,[State_Province_Code]
                           ,[Street_Address]
                           ,[City_Town]
                           ,[Zip_Postal_Code])
                     VALUES
                           (@Id
                           ,@Company
                           ,@Country_Code
                           ,@State_Province_Code
                           ,@Street_Address
                           ,@City_Town
                           ,@Zip_Postal_Code)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    sqlCommand.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    sqlCommand.Parameters.AddWithValue("@Street_Address", item.Street);
                    sqlCommand.Parameters.AddWithValue("@City_Town", item.City);
                    sqlCommand.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);


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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Locations]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                         ,[Company]
                         ,[Country_Code]
                         ,[State_Province_Code]
                         ,[Street_Address]
                         ,[City_Town]
                         ,[Zip_Postal_Code]
                         ,[Time_Stamp]
                     FROM [dbo].[Company_Locations]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyLocationPoco[] companyLocationPocos = new CompanyLocationPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyLocationPoco = new CompanyLocationPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Company = sqlDataReader.GetGuid(1),
                         CountryCode= sqlDataReader.GetString(2),
                        Province = sqlDataReader.GetString(3),
                        Street =  sqlDataReader.IsDBNull(4) ? null : (string)sqlDataReader[4],
                        City = sqlDataReader.IsDBNull(5) ? null : (string)sqlDataReader[5],
                        PostalCode = sqlDataReader.IsDBNull(6) ? null : (string)sqlDataReader[6],
                        TimeStamp = (byte[])sqlDataReader[7],
                    };
                    companyLocationPocos[i++] = newCompanyLocationPoco;

                }
                sqlConnection.Close();
                return companyLocationPocos;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Locations]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Locations]
                      SET 
                         [Company] = @Company
                         ,[Country_Code] = @Country_Code
                         ,[State_Province_Code] = @State_Province_Code
                         ,[Street_Address] = @Street_Address
                         ,[City_Town] = @City_Town
                         ,[Zip_Postal_Code] = @Zip_Postal_Code
                     WHERE [Id] = @Id");


                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    sqlCommand.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    sqlCommand.Parameters.AddWithValue("@Street_Address", item.Street);
                    sqlCommand.Parameters.AddWithValue("@City_Town", item.City);
                    sqlCommand.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
