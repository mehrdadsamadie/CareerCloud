using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        protected IDataRepository<CompanyJobDescriptionPoco> _repository;
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> companyJobDescriptionPoco) : base(companyJobDescriptionPoco)
        {
            _repository = companyJobDescriptionPoco;
        }
        public override void Add(CompanyJobDescriptionPoco[] companyJobDescriptionLogics)
        {
            Verify(companyJobDescriptionLogics);
            base.Add(companyJobDescriptionLogics);
        }

        public override void Update(CompanyJobDescriptionPoco[] companyJobDescriptionLogics)
        {
            Verify(companyJobDescriptionLogics);
            base.Add(companyJobDescriptionLogics);
        }
        protected override void Verify(CompanyJobDescriptionPoco[] companyJobDescriptionLogics)
        {
            var errors = new List<ValidationException>();

            foreach (var item in companyJobDescriptionLogics)
            {
                if (string.IsNullOrEmpty(item.JobName))
                {
                    errors.Add(new ValidationException(300, "JobName cannot be empty"));
                }
                if (string.IsNullOrEmpty(item.JobDescriptions))
                {
                    errors.Add(new ValidationException(301, "JobDescriptions cannot be empty"));
                }

            }
            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }
        }
    }
}
