using CareerCloud.ADODataAccessLayer;
using CareerCloud.Pocos;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var _rep = new ApplicantEducationRepository();
            var tt = new ApplicantEducationPoco()
            {
                Id= Guid.NewGuid(),
                Applicant= Guid.Parse("27c49e93-83c0-1d5e-534b-4020f1af3dd0"),
                CertificateDiploma = "sdsd",
                CompletionDate = DateTime.Now,
                Major = "IT",
                StartDate = DateTime.Now,
                CompletionPercent = 23
            };
            var temp = new ApplicantEducationPoco[20];
            temp[0] = tt;
            _rep.Add(temp);
            Console.WriteLine("Hello World!");
        }
    }
}
