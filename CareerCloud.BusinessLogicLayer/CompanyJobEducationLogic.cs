using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic:BaseLogic<CompanyJobEducationPoco>
	{
		protected IDataRepository<CompanyJobEducationPoco> _repository;
		public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> companyJobEducationPoco) : base(companyJobEducationPoco)
		{
			_repository = companyJobEducationPoco;
		}
		public override void Add(CompanyJobEducationPoco[] companyJobEducationPocos)
		{
			Verify(companyJobEducationPocos);
			base.Add(companyJobEducationPocos);
		}

		public override void Update(CompanyJobEducationPoco[] companyJobEducationPocos)
		{
			Verify(companyJobEducationPocos);
			base.Add(companyJobEducationPocos);
		}
		protected override void Verify(CompanyJobEducationPoco[] companyJobEducationPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in companyJobEducationPocos)
			{

				if ( string.IsNullOrEmpty(item.Major) || item.Major.Length < 2)
				{
					errors.Add(new ValidationException(200, "Major must be at least 2 characters"));
				}
				if (item.Importance<0)
				{
					errors.Add(new ValidationException(201, "Importance cannot be less than 0"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
