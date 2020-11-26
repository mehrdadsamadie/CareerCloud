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
    public class CompanyJobEducationRepository :BaseAdo, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Job_Educations]
                        ([Id]
                        ,[Job]
                        ,[Major]
                        ,[Importance])
                  VALUES
                        (@Id
                        ,@Job
                        ,@Major
                        ,@Importance)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Major", item.Major);
                    sqlCommand.Parameters.AddWithValue("@Importance", item.Importance);
   

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Job_Educations]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                      ,[Job]
                      ,[Major]
                      ,[Importance]
                      ,[Time_Stamp]
                  FROM [dbo].[Company_Job_Educations]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyJobEducationPoco[] companyJobEducationPocos = new CompanyJobEducationPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyJobEducationPoco = new CompanyJobEducationPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Job = sqlDataReader.GetGuid(1),
                        Major = sqlDataReader.GetString(2),
                        Importance = Convert.ToInt16(sqlDataReader[3]),
                        TimeStamp = (byte[])sqlDataReader[4],
                    };
                    companyJobEducationPocos[i++] = newCompanyJobEducationPoco;

                }
                sqlConnection.Close();
                return companyJobEducationPocos;
            }

        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Job_Educations]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Job_Educations]
                     SET 
                        [Job] = @Job
                        ,[Major] = @Major
                        ,[Importance] = @Importance
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Major", item.Major);
                    sqlCommand.Parameters.AddWithValue("@Importance", item.Importance);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
