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
    public class ApplicantWorkHistoryRepository :BaseAdo, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Work_History]
                         ([Id]
                         ,[Applicant]
                         ,[Company_Name]
                         ,[Country_Code]
                         ,[Location]
                         ,[Job_Title]
                         ,[Job_Description]
                         ,[Start_Month]
                         ,[Start_Year]
                         ,[End_Month]
                         ,[End_Year])
                   VALUES
                         (@Id
                         ,@Applicant
                         ,@Company_Name
                         ,@Country_Code
                         ,@Location
                         ,@Job_Title
                         ,@Job_Description
                         ,@Start_Month
                         ,@Start_Year
                         ,@End_Month
                         ,@End_Year)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    sqlCommand.Parameters.AddWithValue("@Location", item.Location);
                    sqlCommand.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    sqlCommand.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    sqlCommand.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    sqlCommand.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    sqlCommand.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    sqlCommand.Parameters.AddWithValue("@End_Year", item.EndYear);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Work_History]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Applicant]
                     ,[Company_Name]
                     ,[Country_Code]
                     ,[Location]
                     ,[Job_Title]
                     ,[Job_Description]
                     ,[Start_Month]
                     ,[Start_Year]
                     ,[End_Month]
                     ,[End_Year]
                     ,[Time_Stamp]
                 FROM [dbo].[Applicant_Work_History]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos = new ApplicantWorkHistoryPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newApplicantWorkHistoryPoco = new ApplicantWorkHistoryPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Applicant = sqlDataReader.GetGuid(1),
                        CompanyName = sqlDataReader.GetString(2),
                        CountryCode = sqlDataReader.GetString(3),
                        Location = sqlDataReader.GetString(4),
                        JobTitle = sqlDataReader.GetString(5),
                        JobDescription = sqlDataReader.GetString(6),
                        StartMonth = Convert.ToInt16(sqlDataReader[7]),
                        StartYear = sqlDataReader.GetInt32(8),
                        EndMonth =Convert.ToInt16(sqlDataReader[9]),
                        EndYear = sqlDataReader.GetInt32(10),
                        TimeStamp = (byte[])sqlDataReader[11],
                    };
                    applicantWorkHistoryPocos[i++] = newApplicantWorkHistoryPoco;

                }
                sqlConnection.Close();
                return applicantWorkHistoryPocos;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Work_History]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Work_History]
                     SET 
                        [Applicant] = @Applicant
                        ,[Company_Name] = @Company_Name
                        ,[Country_Code] = @Country_Code
                        ,[Location] = @Location
                        ,[Job_Title] = @Job_Title
                        ,[Job_Description] = @Job_Description
                        ,[Start_Month] = @Start_Month
                        ,[Start_Year] = @Start_Year
                        ,[End_Month] = @End_Month
                        ,[End_Year] = @End_Year
                     WHERE [Id] = @Id");

                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    sqlCommand.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    sqlCommand.Parameters.AddWithValue("@Location", item.Location);
                    sqlCommand.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    sqlCommand.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    sqlCommand.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    sqlCommand.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    sqlCommand.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    sqlCommand.Parameters.AddWithValue("@End_Year", item.EndYear);

                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
