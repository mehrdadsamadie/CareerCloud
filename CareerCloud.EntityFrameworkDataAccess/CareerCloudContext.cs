
using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder
optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Job;Persist Security Info=True;User ID=sa;Password=Samad!123");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }

        public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }

        public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }

        public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }

        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }

        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }

        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }

        public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }

        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }

        public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }

        public DbSet<SecurityRolePoco> SecurityRole { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(d => d.ApplicantProfile)
                .WithMany(p => p.ApplicantEducations)
                .HasForeignKey(d => d.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(d => d.ApplicantProfile)
                .WithMany(p => p.ApplicantJobApplications)
                .HasForeignKey(d => d.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(d => d.CompanyJob)
                .WithMany(p => p.ApplicantJobApplications)
                .HasForeignKey(d => d.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(d => d.SecurityLogin)
                .WithMany(p => p.ApplicantProfiles)
                .HasForeignKey(d => d.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(d => d.SystemCountryCode)
                .WithMany(p => p.ApplicantProfiles)
                .HasForeignKey(d => d.Country);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(d => d.ApplicantProfile)
                .WithMany(p => p.ApplicantResumes)
                .HasForeignKey(d => d.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(d => d.ApplicantProfile)
                .WithMany(p => p.ApplicantSkills)
                .HasForeignKey(d => d.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(d => d.ApplicantProfile)
                .WithMany(p => p.ApplicantWorkHistorys)
                .HasForeignKey(d => d.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(d => d.SystemCountryCode)
                .WithMany(p => p.ApplicantWorkHistorys)
                .HasForeignKey(d => d.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(d => d.CompanyProfile)
                .WithMany(p => p.CompanyDescriptions)
                .HasForeignKey(d => d.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
               .HasOne(d => d.SystemLanguageCode)
               .WithMany(p => p.CompanyDescriptions)
               .HasForeignKey(d => d.LanguageId);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
               .HasOne(d => d.CompanyJob)
               .WithMany(p => p.CompanyJobDescriptions)
               .HasForeignKey(d => d.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
               .HasOne(d => d.CompanyJob)
               .WithMany(p => p.CompanyJobEducations)
               .HasForeignKey(d => d.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(d => d.CompanyProfile)
                .WithMany(p => p.CompanyJobs)
                .HasForeignKey(d => d.Company);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(d => d.CompanyJob)
                .WithMany(p => p.CompanyJobSkills)
                .HasForeignKey(d => d.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(d => d.CompanyProfile)
                .WithMany(p => p.CompanyLocations)
                .HasForeignKey(d => d.Company);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(d => d.SecurityLogin)
                .WithMany(p => p.SecurityLoginsLogs)
                .HasForeignKey(d => d.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
               .HasOne(d => d.SecurityLogin)
               .WithMany(p => p.SecurityLoginsRoles)
               .HasForeignKey(d => d.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(d => d.SecurityRole)
                .WithMany(p => p.SecurityLoginsRoles)
                .HasForeignKey(d => d.Role);


            //modelBuilder.Entity<ApplicantEducationPoco>();
            //modelBuilder.Entity<ApplicantJobApplicationPoco>();
            //modelBuilder.Entity<ApplicantResumePoco>();
            //modelBuilder.Entity<ApplicantSkillPoco>();
            //modelBuilder.Entity<ApplicantWorkHistoryPoco>();
            //modelBuilder.Entity<CompanyJobDescriptionPoco>();
            //modelBuilder.Entity<CompanyJobEducationPoco>();
            //modelBuilder.Entity<CompanyJobPoco>();
            //modelBuilder.Entity<CompanyJobSkillPoco>();
            //modelBuilder.Entity<CompanyLocationPoco>();
            //modelBuilder.Entity<CompanyProfilePoco>();
            //modelBuilder.Entity<SecurityLoginPoco>();
            //modelBuilder.Entity<SecurityLoginsLogPoco>();
            //modelBuilder.Entity<SecurityRolePoco>();
            //modelBuilder.Entity<SystemCountryCodePoco>();
            //modelBuilder.Entity<SystemLanguageCodePoco>();

        }
    }
}
