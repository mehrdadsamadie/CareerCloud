using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic: BaseLogic<ApplicantEducationPoco>
    {
        protected IDataRepository<ApplicantEducationPoco> _repository;
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> applicantEducationPocos) :base(applicantEducationPocos)
        {
            _repository = applicantEducationPocos;
        }
		public override void Add(ApplicantEducationPoco[] applicantEducationPocos)
		{
			Verify(applicantEducationPocos);
			base.Add(applicantEducationPocos);
		}

		public override void Update(ApplicantEducationPoco[] applicantEducationPocos)
		{
			Verify(applicantEducationPocos);
			base.Add(applicantEducationPocos);
		}
		protected override void Verify(ApplicantEducationPoco[] applicantEducationPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in applicantEducationPocos) 
			{
				if (string.IsNullOrEmpty(item.Major) || item.Major.Length < 3) 
				{
					errors.Add(new ValidationException(107, "Cannot be empty or less than 3 characters"));
				}
				if (item.StartDate > DateTime.Now) 
				{
					errors.Add(new ValidationException(108, "Cannot be greater than today"));
				}
				if(item.CompletionDate< item.StartDate) 
				{
					errors.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate"));
				}
			}
			if (errors.Count > 0) 
			{
				throw new AggregateException(errors);
			}
		}
	}
}
