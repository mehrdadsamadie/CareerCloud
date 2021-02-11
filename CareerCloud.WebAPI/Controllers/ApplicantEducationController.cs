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
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }
        [HttpGet("education/{applicantEducationId}")]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            var _poco = _logic.Get(applicantEducationId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("education")]
        public ActionResult PostApplicantEducation([FromBody]ApplicantEducationPoco[] model)
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
        [HttpGet("education")]
        public ActionResult GetAllApplicantEducation()
        {

                return Ok(_logic.GetAll());
            
        }
        [HttpPut("education")]
        public ActionResult PutApplicantEducation([FromBody]ApplicantEducationPoco[] model)
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

        [HttpDelete("education")]
        public ActionResult DeleteApplicantEducation([FromBody]ApplicantEducationPoco[] model)
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