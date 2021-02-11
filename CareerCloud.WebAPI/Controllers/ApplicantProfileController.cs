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
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _logic;
        public ApplicantProfileController()
        {
            EFGenericRepository<ApplicantProfilePoco> repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repo);
        }
        [HttpGet("profile/{applicantProfileId}")]
        public ActionResult GetApplicantProfile(Guid applicantProfileId)
        {
            var _poco = _logic.Get(applicantProfileId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("profile")]
        public ActionResult PostApplicantProfile([FromBody]ApplicantProfilePoco[] model)
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
        [HttpGet("profile")]
        public ActionResult GetAllApplicantProfile()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("profile")]
        public ActionResult PutApplicantProfile([FromBody]ApplicantProfilePoco[] model)
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

        [HttpDelete("profile")]
        public ActionResult DeleteApplicantProfile([FromBody]ApplicantProfilePoco[] model)
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