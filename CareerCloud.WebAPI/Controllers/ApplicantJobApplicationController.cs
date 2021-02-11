using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;
        public ApplicantJobApplicationController()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }
        [HttpGet("jobapplication/{applicantJobApplicationId}")]
        public ActionResult GetApplicantJobApplication(Guid applicantJobApplicationId)
        {
            var _poco = _logic.Get(applicantJobApplicationId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("jobapplication")]
        public ActionResult PostApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] model)
        {

            if (model == null)
            {
                return BadRequest("400");
            }

            else
            {
                _logic.Add(model);
                return Ok();
            }
        }
        [HttpGet("jobapplication")]
        public ActionResult GetAllApplicantJobApplication()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("jobapplication")]
        public ActionResult PutApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] model)
        {
            if (model == null)
            {
                return BadRequest("400");
            }

            else
            {
                _logic.Update(model);
                return Ok();
            }


        }

        [HttpDelete("jobapplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody]ApplicantJobApplicationPoco[] model)
        {
            if (model == null)
            {
                return BadRequest("400");
            }

            else
            {
                _logic.Delete(model);
                return Ok();
            }


        }
    }
}