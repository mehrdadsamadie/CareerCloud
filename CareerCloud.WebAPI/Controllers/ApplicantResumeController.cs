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
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;
        public ApplicantResumeController()
        {
            EFGenericRepository<ApplicantResumePoco> repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }
        [HttpGet("resume/{applicantResumeId}")]
        public ActionResult GetApplicantResume(Guid applicantResumeId)
        {
            var _poco = _logic.Get(applicantResumeId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("resume")]
        public ActionResult PostApplicantResume([FromBody]ApplicantResumePoco[] model)
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
        [HttpGet("resume")]
        public ActionResult GetAllApplicantResume()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("resume")]
        public ActionResult PutApplicantResume([FromBody]ApplicantResumePoco[] model)
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

        [HttpDelete("resume")]
        public ActionResult DeleteApplicantResume([FromBody]ApplicantResumePoco[] model)
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