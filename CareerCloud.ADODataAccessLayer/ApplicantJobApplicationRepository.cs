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
    public class ApplicantJobApplicationRepository : BaseAdo, IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Job_Applications]
                             ([Id]
                             ,[Applicant]
                             ,[Job]
                             ,[Application_Date])
                       VALUES
                             (@Id
                             ,@Applicant
                             ,@Job
                             ,@ApplicationDate)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);
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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Job_Applications]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Applicant]
                     ,[Job]
                     ,[Application_Date]
                     ,[Time_Stamp]
                 FROM [dbo].[Applicant_Job_Applications]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ApplicantJobApplicationPoco[] applicantJobApplicationPocos = new ApplicantJobApplicationPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newApplicantJobApplicationPoco = new ApplicantJobApplicationPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Applicant = sqlDataReader.GetGuid(1),
                        Job = sqlDataReader.GetGuid(2),
                        ApplicationDate = sqlDataReader.GetDateTime(3),
                        
                        TimeStamp = (byte[])sqlDataReader[4],
                    };
                    applicantJobApplicationPocos[i++] = newApplicantJobApplicationPoco;

                }
                sqlConnection.Close();
                return applicantJobApplicationPocos;
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Job_Applications]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Job_Applications]
                      SET 
                         [Applicant] = @Applicant
                         ,[Job] = @Job
                         ,[Application_Date] = @ApplicationDate
                      WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
