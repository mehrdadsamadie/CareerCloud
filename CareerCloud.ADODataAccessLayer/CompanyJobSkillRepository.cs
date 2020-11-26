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
    public class CompanyJobSkillRepository :BaseAdo, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"INSERT INTO [dbo].[Company_Job_Skills]
                          ([Id]
                          ,[Job]
                          ,[Skill]
                          ,[Skill_Level]
                          ,[Importance])
                    VALUES
                          (@Id
                          ,@Job
                          ,@Skill
                          ,@Skill_Level
                          ,@Importance)");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Skill", item.Skill);
                    sqlCommand.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Company_Job_Skills]";
                sqlConnection.Open();
                Int32 count = (Int32)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
                sqlCommand.CommandText = (@"SELECT [Id]
                      ,[Job]
                      ,[Skill]
                      ,[Skill_Level]
                      ,[Importance]
                      ,[Time_Stamp]
                  FROM [dbo].[Company_Job_Skills]");
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CompanyJobSkillPoco[] companyJobSkillPocos = new CompanyJobSkillPoco[count];
                int i = 0;
                while (sqlDataReader.Read())
                {
                    var newCompanyJobSkillPoco = new CompanyJobSkillPoco()
                    {
                        Id = sqlDataReader.GetGuid(0),
                        Job = sqlDataReader.GetGuid(1),
                        Skill = sqlDataReader.GetString(2),
                        SkillLevel = sqlDataReader.GetString(3),
                        Importance = sqlDataReader.GetInt32(4),
                        TimeStamp = (byte[])sqlDataReader[5],
                    };
                    companyJobSkillPocos[i++] = newCompanyJobSkillPoco;

                }
                sqlConnection.Close();
                return companyJobSkillPocos;
            }

        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> lists = GetAll().AsQueryable();
            return lists.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"DELETE FROM [dbo].[Company_Job_Skills]
                        WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                foreach (var item in items)
                {
                    sqlCommand.CommandText = (@"UPDATE [dbo].[Company_Job_Skills]
                     SET 
                         [Job] = @Job
                        ,[Skill] = @Skill
                        ,[Skill_Level] = @Skill_Level
                        ,[Importance] = @Importance
                     WHERE [Id] = @Id");
                    sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                    sqlCommand.Parameters.AddWithValue("@Job", item.Job);
                    sqlCommand.Parameters.AddWithValue("@Skill", item.Skill);
                    sqlCommand.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    sqlCommand.Parameters.AddWithValue("@Importance", item.Importance);

                    sqlConnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }

        }
    }
}
