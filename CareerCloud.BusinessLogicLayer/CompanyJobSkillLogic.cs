using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic:BaseLogic<CompanyJobSkillPoco>
    {
		protected IDataRepository<CompanyJobSkillPoco> _repository;
		public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> companyJobSkillPoco) : base(companyJobSkillPoco)
		{
			_repository = companyJobSkillPoco;
		}
		public override void Add(CompanyJobSkillPoco[] companyJobSkillPocos)
		{
			Verify(companyJobSkillPocos);
			base.Add(companyJobSkillPocos);
		}

		public override void Update(CompanyJobSkillPoco[] companyJobSkillPocos)
		{
			Verify(companyJobSkillPocos);
			base.Update(companyJobSkillPocos);
		}
		protected override void Verify(CompanyJobSkillPoco[] companyJobSkillPocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in companyJobSkillPocos)
			{

				if (item.Importance<0)
				{
					errors.Add(new ValidationException(400, "Importance cannot be less than 0"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
