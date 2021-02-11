using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic: BaseLogic<ApplicantJobApplicationPoco>
    {
		protected IDataRepository<ApplicantJobApplicationPoco> _repository;
		public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> ApplicantJobApplicationPocos) : base(ApplicantJobApplicationPocos)
		{
			_repository = ApplicantJobApplicationPocos;
		}
		public override void Add(ApplicantJobApplicationPoco[] ApplicantJobApplicationPocos)
		{
			Verify(ApplicantJobApplicationPocos);
			base.Add(ApplicantJobApplicationPocos);
		}

		public override void Update(ApplicantJobApplicationPoco[] ApplicantJobApplicationPocos)
		{
			Verify(ApplicantJobApplicationPocos);
			base.Update(ApplicantJobApplicationPocos);
		}
		protected override void Verify(ApplicantJobApplicationPoco[] ApplicantJobApplicationPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in ApplicantJobApplicationPocos)
			{
				if (item.ApplicationDate>DateTime.Now)
				{
					errors.Add(new ValidationException(110, "ApplicationDate cannot be greater than today"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
