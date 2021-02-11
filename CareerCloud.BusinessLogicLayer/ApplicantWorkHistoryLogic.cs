using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic:BaseLogic<ApplicantWorkHistoryPoco>
    {
		protected IDataRepository<ApplicantWorkHistoryPoco> _repository;
		public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> applicantWorkHistoryPoco) : base(applicantWorkHistoryPoco)
		{
			_repository = applicantWorkHistoryPoco;
		}
		public override void Add(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
		{
			Verify(applicantWorkHistoryPocos);
			base.Add(applicantWorkHistoryPocos);
		}

		public override void Update(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
		{
			Verify(applicantWorkHistoryPocos);
			base.Update(applicantWorkHistoryPocos);
		}
		protected override void Verify(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in applicantWorkHistoryPocos)
			{
				if (string.IsNullOrEmpty(item.CompanyName) ||item.CompanyName.Length<3)
				{
					errors.Add(new ValidationException(105, "Must be greater then 2 characters"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
