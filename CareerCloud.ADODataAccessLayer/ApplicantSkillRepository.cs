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
    public class ApplicantSkillRepository:BaseAdo , IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Applicant_Skills]
                         ([Id]
                         ,[Applicant]
                         ,[Skill]
                         ,[Skill_Level]
                         ,[Start_Month]
                         ,[Start_Year]
                         ,[End_Month]
                         ,[End_Year])
                   VALUES
                         (@Id
                         ,@Applicant
                         ,@Skill
                         ,@Skill_Level
                         ,@Start_Month
                         ,@Start_Year
                         ,@End_Month
                         ,@End_Year)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Skill", (String.IsNullOrEmpty(item.Skill) ? DBNull.Value : (object)item.Skill));
                    sqlCommand.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Applicant_Skills]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                      ,[Applicant]
                      ,[Skill]
                      ,[Skill_Level]
                      ,[Start_Month]
                      ,[Start_Year]
                      ,[End_Month]
                      ,[End_Year]
                      ,[Time_Stamp]
                  FROM [dbo].[Applicant_Skills]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                var applicantSkillPocos = new ApplicantSkillPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newApplicantSkillPoco = new ApplicantSkillPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Applicant = sqlDataReader.GetGuid(1),
                        Skill = sqlDataReader.GetString(2),
                        SkillLevel = sqlDataReader.GetString(3),
                        StartMonth = sqlDataReader.GetByte(4),
                        StartYear = sqlDataReader.GetInt32(5),
                        EndMonth = sqlDataReader.GetByte(6),
                        EndYear = sqlDataReader.GetInt32(7),
                        TimeStamp = (byte[])sqlDataReader[8],
                    };
                    applicantSkillPocos[i++] = newApplicantSkillPoco;

                }
                sqlConnection.Close();
                return applicantSkillPocos;
            }
            
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Applicant_Skills]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Applicant_Skills]
                      SET 
                         [Applicant] =@Applicant
                         ,[Skill] =@Skill
                         ,[Skill_Level] =@Skill_Level
                         ,[Start_Month] =@Start_Month
                         ,[Start_Year] = @Start_Year
                         ,[End_Month] =@End_Month 
                         ,[End_Year] = @End_Year
                     WHERE [Id] = @Id");

                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Applicant", item.Applicant);
                    sqlCommand.Parameters.AddWithValue("@Skill", (String.IsNullOrEmpty(item.Skill) ? DBNull.Value : (object)item.Skill));
                    sqlCommand.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
