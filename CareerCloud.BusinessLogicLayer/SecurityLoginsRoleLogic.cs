using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class SecurityLoginsRoleLogic:BaseLogic<SecurityLoginsRolePoco>
    {
        protected IDataRepository<SecurityLoginsRolePoco> _repository;
        public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> securityLoginsRolePoco) : base(securityLoginsRolePoco)
        {
            _repository = securityLoginsRolePoco;
        }
    }
}
