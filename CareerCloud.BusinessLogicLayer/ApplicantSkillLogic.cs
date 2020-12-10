using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic:BaseLogic<ApplicantSkillPoco>
    {
		protected IDataRepository<ApplicantSkillPoco> _repository;
		public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> applicantSkillPoco) : base(applicantSkillPoco)
		{
			_repository = applicantSkillPoco;
		}
		public override void Add(ApplicantSkillPoco[] applicantSkillPocos)
		{
			Verify(applicantSkillPocos);
			base.Add(applicantSkillPocos);
		}

		public override void Update(ApplicantSkillPoco[] applicantSkillPocos)
		{
			Verify(applicantSkillPocos);
			base.Add(applicantSkillPocos);
		}
		protected override void Verify(ApplicantSkillPoco[] applicantSkillPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in applicantSkillPocos)
			{
				if (item.StartMonth > 12 || item.StartMonth < 0)
				{
					errors.Add(new ValidationException(101, "Cannot be greater than 12"));
				}
				if (item.EndMonth > 12 || item.EndMonth < 0)
				{
					errors.Add(new ValidationException(102, "Cannot be greater than 12"));
				}
				if (item.EndYear <1900)
				{
					errors.Add(new ValidationException(103, "Cannot be less then 1900"));
				}
				if (item.EndYear < item.StartYear)
				{
					errors.Add(new ValidationException(104, "Cannot be less then StartYear"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
