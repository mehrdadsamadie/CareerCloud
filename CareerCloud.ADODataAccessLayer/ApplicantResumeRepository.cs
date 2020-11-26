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
    public class ApplicantResumeRepository :BaseAdo,IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Resumes]
                          ([Id]
                          ,[Applicant]
                          ,[Resume]
                          ,[Last_Updated])
                    VALUES
                          (@Id
                          ,@Applicant
                          ,@Resume
                          ,@Last_Updated)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Resume", item.Resume);
                    sqlCommand.Parameters.AddWithValue("@Last_Updated", (item.LastUpdated == null ? DBNull.Value : (object)item.LastUpdated));
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Resumes]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                     ,[Applicant]
                     ,[Resume]
                     ,[Last_Updated]
                 FROM [dbo].[Applicant_Resumes]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                var applicantResumePocos = new ApplicantResumePoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newApplicantResumePoco = new ApplicantResumePoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Applicant = sqlDataReader.GetGuid(1),
                        Resume = sqlDataReader.GetString(2),
                        LastUpdated = sqlDataReader.IsDBNull(3) ? (DateTime?)null : sqlDataReader.GetDateTime(3),

                    };
                    applicantResumePocos[i++] = newApplicantResumePoco;

                }
                sqlConnection.Close();
                return applicantResumePocos;
            }

        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Resumes]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Resumes]
                     SET
                        [Applicant] = @Applicant
                        ,[Resume] = @Resume
                        ,[Last_Updated] = @Last_Updated
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Resume", item.Resume);
                    sqlCommand.Parameters.AddWithValue("@Last_Updated", (item.LastUpdated == null ? DBNull.Value : (object)item.LastUpdated));
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }
    }
}
