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
    public class CompanyJobDescriptionRepository :BaseAdo, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                           ([Id]
                           ,[Job]
                           ,[Job_Name]
                           ,[Job_Descriptions])
                     VALUES
                           (@Id
                           ,@Job
                           ,@Job_Name
                           ,@Job_Descriptions)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Job_Name", item.JobName);
                    sqlCommand.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Jobs_Descriptions]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
      ,[Job]
      ,[Job_Name]
      ,[Job_Descriptions]
      ,[Time_Stamp]
  FROM [dbo].[Company_Jobs_Descriptions]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyJobDescriptionPoco[] CompanyJobDescriptionPocos = new CompanyJobDescriptionPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyJobDescriptionPoco = new CompanyJobDescriptionPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Job = sqlDataReader.GetGuid(1),
                        JobName = sqlDataReader.GetString(2),
                        JobDescriptions = sqlDataReader.GetString(3),
                        
                        TimeStamp = (byte[])sqlDataReader[4],
                    };
                    CompanyJobDescriptionPocos[i++] = newCompanyJobDescriptionPoco;

                }
                sqlConnection.Close();
                return CompanyJobDescriptionPocos;
            }

        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Jobs_Descriptions]
                     SET 
                        [Job] = @Job
                        ,[Job_Name] = @Job_Name
                        ,[Job_Descriptions] = @Job_Descriptions
                     WHERE [Id] = @Id");


                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Job_Name", item.JobName);
                    sqlCommand.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
