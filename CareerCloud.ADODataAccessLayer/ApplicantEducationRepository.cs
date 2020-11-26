using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseAdo, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Educations]
                          ([Id]
                          ,[Applicant]
                          ,[Major]
                          ,[Certificate_Diploma]
                          ,[Start_Date]
                          ,[Completion_Date]
                          ,[Completion_Percent]
                          )
                    VALUES
                          (@Id
                          ,@Applicant
                          ,@Major
                          ,@CertificateDiploma
                          ,@StartDate
                          ,@CompletionDate
                          ,@CompletionPercent)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Major", item.Major);
                    sqlCommand.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                    sqlCommand.Parameters.AddWithValue("@StartDate", item.StartDate);
                    sqlCommand.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                    sqlCommand.Parameters.AddWithValue("@CompletionPercent", (item.CompletionPercent==null? DBNull.Value : (object)item.CompletionPercent));


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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Educations]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                         ,[Applicant]
                         ,[Major]
                         ,[Certificate_Diploma]
                         ,[Start_Date]
                         ,[Completion_Date]
                         ,[Completion_Percent]
                         ,[Time_Stamp]
                     FROM [dbo].[Applicant_Educations]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ApplicantEducationPoco[] ApplicantEducationPocos = new ApplicantEducationPoco[count];
                int i = 0;
                while (sqlDataReader.Read()) 
                {
                    var newApplicantEducationPoco = new ApplicantEducationPoco() 
                    {
                        Id=sqlDataReader.GetGuid(0),
                        Applicant = sqlDataReader.GetGuid(1),
                        Major = sqlDataReader.GetString(2),
                        CertificateDiploma =sqlDataReader.GetString(3),
                        StartDate= sqlDataReader.IsDBNull(4)?(DateTime?)null: sqlDataReader.GetDateTime(4),
                        CompletionDate = sqlDataReader.IsDBNull(5) ? (DateTime?)null : sqlDataReader.GetDateTime(5),
                        CompletionPercent= sqlDataReader.IsDBNull(6) ? (byte?)null: (byte?)sqlDataReader[6],
                        TimeStamp= (byte[])sqlDataReader[7],
                    };
                    ApplicantEducationPocos[i++] = newApplicantEducationPoco;

                }
                sqlConnection.Close();
                return ApplicantEducationPocos;
                }
            }
        
        

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Educations]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Educations]
                     SET 
                          [Applicant] = @Applicant
                          ,[Major] =@Major
                          ,[Certificate_Diploma] = @CertificateDiploma
                          ,[Start_Date] =@StartDate
                          ,[Completion_Date] = @CompletionDate
                          ,[Completion_Percent] = @CompletionPercent
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Major", item.Major);
                    sqlCommand.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                    sqlCommand.Parameters.AddWithValue("@StartDate", item.StartDate);
                    sqlCommand.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                    sqlCommand.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);


                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
