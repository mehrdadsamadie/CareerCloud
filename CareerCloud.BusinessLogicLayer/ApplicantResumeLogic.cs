using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic:BaseLogic<ApplicantResumePoco>
    {
        protected IDataRepository<ApplicantResumePoco> _repository;
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> applicantResumePoco):base(applicantResumePoco)
        {
            _repository = applicantResumePoco;
        }
		public override void Add(ApplicantResumePoco[] applicantResumePocos)
		{
			Verify(applicantResumePocos);
			base.Add(applicantResumePocos);
		}

		public override void Update(ApplicantResumePoco[] applicantResumePocos)
		{
			Verify(applicantResumePocos);
			base.Update(applicantResumePocos);
		}
		protected override void Verify(ApplicantResumePoco[] applicantResumePocos)
		{
			var errors = new List<ValidationException>();

			foreach (var item in applicantResumePocos)
			{

				if (string.IsNullOrEmpty(item.Resume))
				{
					errors.Add(new ValidationException(113, "Resume cannot be empty"));
				}

			}
			if (errors.Count > 0)
			{
				throw new AggregateException(errors);
			}
		}
	}
}
