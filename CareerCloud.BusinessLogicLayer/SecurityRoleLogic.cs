using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic:BaseLogic<SecurityRolePoco>
    {
		protected IDataRepository<SecurityRolePoco> _repository;
		public SecurityRoleLogic(IDataRepository<SecurityRolePoco> securityRolePoco) : base(securityRolePoco)
		{
			_repository = securityRolePoco;
		}
		public override void Add(SecurityRolePoco[] securityRolePocos)
		{
			Verify(securityRolePocos);
			base.Add(securityRolePocos);
		}

		public override void Update(SecurityRolePoco[] securityRolePocos)
		{
			Verify(securityRolePocos);
			base.Update(securityRolePocos);
		}
		protected override void Verify(SecurityRolePoco[] securityRolePocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in securityRolePocos)
			{
				if (string.IsNullOrEmpty(item.Role))
				{
					errors.Add(new ValidationException(800, "Cannot be empty"));
				}


			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
