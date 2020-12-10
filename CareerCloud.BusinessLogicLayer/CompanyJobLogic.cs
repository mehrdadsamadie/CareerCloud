using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class CompanyJobLogic:BaseLogic<CompanyJobPoco>
    {
        protected IDataRepository<CompanyJobPoco> _repository;
        public CompanyJobLogic(IDataRepository<CompanyJobPoco> companyJobPoco) : base(companyJobPoco)
        {
            _repository = companyJobPoco;
        }
    }
}
