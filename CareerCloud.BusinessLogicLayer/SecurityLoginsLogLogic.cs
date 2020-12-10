using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityLoginsLogLogic:BaseLogic<SecurityLoginsLogPoco>
    {
        protected IDataRepository<SecurityLoginsLogPoco> _repository;
        public SecurityLoginsLogLogic(IDataRepository<SecurityLoginsLogPoco> companyJobPoco) : base(companyJobPoco)
        {
            _repository = companyJobPoco;
        }
    }
}
