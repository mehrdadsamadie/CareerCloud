using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
	{
		protected IDataRepository<ApplicantProfilePoco> _repository;
		public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> ApplicantProfilePoco) : base(ApplicantProfilePoco)
		{
			_repository = ApplicantProfilePoco;
		}
		public override void Add(ApplicantProfilePoco[] applicantProfilePocos)
		{
			Verify(applicantProfilePocos);
			base.Add(applicantProfilePocos);
		}

		public override void Update(ApplicantProfilePoco[] applicantProfilePocos)
		{
			Verify(applicantProfilePocos);
			base.Update(applicantProfilePocos);
		}
		protected override void Verify(ApplicantProfilePoco[] applicantProfilePocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in applicantProfilePocos)
			{

				if (item.CurrentRate<0 )
				{
					errors.Add(new ValidationException(112, "CurrentRate cannot be negative"));
				}
				if (item.CurrentSalary < 0)
				{
					errors.Add(new ValidationException(111, "CurrentSalary cannot be negative"));
				}
			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
