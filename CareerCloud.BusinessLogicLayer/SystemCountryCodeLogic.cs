using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemCountryCodeLogic
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repo) 
        {
            _repository = repo;
        }

		protected virtual void Verify(SystemCountryCodePoco[] pocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in pocos)
			{
				if (string.IsNullOrEmpty(item.Code))
				{
					errors.Add(new ValidationException(900, "Cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.Name))
				{
					errors.Add(new ValidationException(901, "Cannot be empty"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}

		public  SystemCountryCodePoco Get(string code)
		{
			return _repository.GetSingle(c => c.Code == code);
		}

		public  List<SystemCountryCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public virtual void Add(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Add(pocos);
		}

		public virtual void Update(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}

		public void Delete(SystemCountryCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
