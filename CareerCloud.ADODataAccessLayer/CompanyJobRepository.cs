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
    public class CompanyJobRepository :BaseAdo, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Jobs]
           ([Id]
           ,[Company]
           ,[Profile_Created]
           ,[Is_Inactive]
           ,[Is_Company_Hidden])
     VALUES
           (@Id
           ,@Company
           ,@Profile_Created
           ,@Is_Inactive
           ,@Is_Company_Hidden)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    sqlCommand.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    sqlCommand.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Jobs]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                      ,[Company]
                      ,[Profile_Created]
                      ,[Is_Inactive]
                      ,[Is_Company_Hidden]
                      ,[Time_Stamp]
                  FROM [dbo].[Company_Jobs]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyJobPoco[] companyJobPocos = new CompanyJobPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyJobPoco = new CompanyJobPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Company = sqlDataReader.GetGuid(1),
                        ProfileCreated = sqlDataReader.GetDateTime(2),
                        IsInactive = sqlDataReader.GetBoolean(3),
                        IsCompanyHidden =  sqlDataReader.GetBoolean(4),
                        TimeStamp = (byte[])sqlDataReader[5],
                    };
                    companyJobPocos[i++] = newCompanyJobPoco;

                }
                sqlConnection.Close();
                return companyJobPocos;
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Jobs]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Jobs]
                    SET 
                        [Company] = @Company
                       ,[Profile_Created] = @Profile_Created
                       ,[Is_Inactive] = @Is_Inactive
                       ,[Is_Company_Hidden] = @Is_Company_Hidden
                     WHERE [Id] = @Id");

                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Company", item.Company);
                    sqlCommand.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    sqlCommand.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    sqlCommand.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
