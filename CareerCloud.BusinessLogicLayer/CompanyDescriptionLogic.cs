using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic:BaseLogic<CompanyDescriptionPoco>
    {
		protected IDataRepository<CompanyDescriptionPoco> _repository;
		public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> companyDescriptionPoco) : base(companyDescriptionPoco)
		{
			_repository = companyDescriptionPoco;
		}
		public override void Add(CompanyDescriptionPoco[] companyDescriptionPocos)
		{
			Verify(companyDescriptionPocos);
			base.Add(companyDescriptionPocos);
		}

		public override void Update(CompanyDescriptionPoco[] companyDescriptionPocos)
		{
			Verify(companyDescriptionPocos);
			base.Add(companyDescriptionPocos);
		}
		protected override void Verify(CompanyDescriptionPoco[] companyDescriptionPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in companyDescriptionPocos)
			{
				if ( string.IsNullOrEmpty(item.CompanyName) || item.CompanyName.Length<2 )
				{
					errors.Add(new ValidationException(107, "CompanyName must be greater than 2 characters"));
				}
				if (string.IsNullOrEmpty(item.CompanyDescription) || item.CompanyDescription.Length <2)
				{
					errors.Add(new ValidationException(106, "CompanyDescription must be greater than 2 characters"));
				}


			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
