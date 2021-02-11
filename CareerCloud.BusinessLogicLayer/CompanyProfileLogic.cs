using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
		protected IDataRepository<CompanyProfilePoco> _repository;
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> companyProfilePoco) : base(companyProfilePoco)
		{
			_repository = companyProfilePoco;
		}
		public override void Add(CompanyProfilePoco[] CompanyProfilePocos)
		{
			Verify(CompanyProfilePocos);
			base.Add(CompanyProfilePocos);
		}

		public override void Update(CompanyProfilePoco[] CompanyProfilePocos)
		{
			Verify(CompanyProfilePocos);
			base.Update(CompanyProfilePocos);
		}
		protected override void Verify(CompanyProfilePoco[] CompanyProfilePocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in CompanyProfilePocos)
			{
				if (string.IsNullOrEmpty(item.ContactPhone))
				{
					errors.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {item.Id} is not in the required format."));
				}
				else
				{
					string[] phoneComponents = item.ContactPhone.Split('-');
					if (phoneComponents.Length != 3)
					{
						errors.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {item.Id} is not in the required format."));
					}
					else
					{
						if (phoneComponents[0].Length != 3)
						{
							errors.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {item.Id} is not in the required format."));
						}
						else if (phoneComponents[1].Length != 3)
						{
							errors.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {item.Id} is not in the required format."));
						}
						else if (phoneComponents[2].Length != 4)
						{
							errors.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {item.Id} is not in the required format."));
						}
					}
				}
					if (string.IsNullOrEmpty(item.CompanyWebsite))
					{
						errors.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
					}
					else
					{
						var arraystr = item.CompanyWebsite.Split(".");
					if (arraystr.Length < 2) 
					{
						errors.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
					}
					else	if (!((arraystr[(arraystr.Length - 1)] == "ca") || (arraystr[(arraystr.Length - 1)] == "com") || (arraystr[(arraystr.Length - 1)] == "biz")))
							errors.Add(new ValidationException(600, "Valid websites must end with the following extensions – .ca, .com, .biz"));

					}
				
			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
