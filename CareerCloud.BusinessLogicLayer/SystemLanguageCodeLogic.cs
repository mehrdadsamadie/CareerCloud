using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemLanguageCodeLogic
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;
        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) 

        { _repository = repository; }
		protected virtual void Verify(SystemLanguageCodePoco[] pocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in pocos)
			{
				if (string.IsNullOrEmpty(item.LanguageID))
				{
					errors.Add(new ValidationException(1000, "Cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.Name))
				{
					errors.Add(new ValidationException(1001, "Cannot be empty"));
				}
				if (string.IsNullOrEmpty(item.NativeName))
				{
					errors.Add(new ValidationException(1002, "Cannot be empty"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}

		public SystemLanguageCodePoco Get(string code)
		{
			return _repository.GetSingle(c => c.LanguageID == code);
		}

		public List<SystemLanguageCodePoco> GetAll()
		{
			return _repository.GetAll().ToList();
		}

		public virtual void Add(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Add(pocos);
		}

		public virtual void Update(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			_repository.Update(pocos);
		}

		public void Delete(SystemLanguageCodePoco[] pocos)
		{
			_repository.Remove(pocos);
		}
	}
}
