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
    public class CompanyDescriptionRepository :BaseAdo, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Descriptions]
                            ([Id]
                            ,[Company]
                            ,[LanguageID]
                            ,[Company_Name]
                            ,[Company_Description])
                      VALUES
                            (@Id
                            ,@Company
                            ,@LanguageID
                            ,@Company_Name
                            ,@Company_Description)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    sqlCommand.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
 

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Descriptions]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Company]
                     ,[LanguageID]
                     ,[Company_Name]
                     ,[Company_Description]
                     ,[Time_Stamp]
                 FROM [dbo].[Company_Descriptions]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyDescriptionPoco[] CompanyDescriptionPocos = new CompanyDescriptionPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyDescriptionPoco = new CompanyDescriptionPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Company = sqlDataReader.GetGuid(1),
                        LanguageId = sqlDataReader.GetString(2),
                        CompanyName = sqlDataReader.GetString(3),
                        CompanyDescription = sqlDataReader.GetString(4),

                        TimeStamp = (byte[])sqlDataReader[5],
                    };
                    CompanyDescriptionPocos[i++] = newCompanyDescriptionPoco;

                }
                sqlConnection.Close();
                return CompanyDescriptionPocos;
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Descriptions]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Descriptions]
                     SET 
                         [Company] = @Company
                        ,[LanguageID] = @LanguageID
                        ,[Company_Name] =@Company_Name
                        ,[Company_Description] =@Company_Description
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    sqlCommand.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
