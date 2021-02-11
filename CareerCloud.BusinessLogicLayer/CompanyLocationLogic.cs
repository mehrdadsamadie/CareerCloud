using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class CompanyLocationLogic:BaseLogic<CompanyLocationPoco>
    {
		protected IDataRepository<CompanyLocationPoco> _repository;
		public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> companyLocationPoco) : base(companyLocationPoco)
		{
			_repository = companyLocationPoco;
		}
		public override void Add(CompanyLocationPoco[] companyLocationPocos)
		{
			Verify(companyLocationPocos);
			base.Add(companyLocationPocos);
		}

		public override void Update(CompanyLocationPoco[] companyLocationPocos)
		{
			Verify(companyLocationPocos);
			base.Update(companyLocationPocos);
		}
		protected override void Verify(CompanyLocationPoco[] companyLocationPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in companyLocationPocos)
			{
				if (string.IsNullOrEmpty(item.CountryCode))
				{
					errors.Add(new ValidationException(500, "CountryCode cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.Province))
				{
					errors.Add(new ValidationException(501, "Province cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.Street))
				{
					errors.Add(new ValidationException(502, "Street cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.City))
				{
					errors.Add(new ValidationException(503, "City cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.PostalCode))
				{
					errors.Add(new ValidationException(504, "PostalCode cannot be empty"));
				}
			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
