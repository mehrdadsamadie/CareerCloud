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
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;
        public ApplicantSkillController()
        {
            EFGenericRepository<ApplicantSkillPoco> repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }
        [HttpGet("skill/{applicantEducationId}")]
        public ActionResult GetApplicantSkill(Guid applicantSkillPocoId)
        {
            var _poco = _logic.Get(applicantSkillPocoId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("skill")]
        public ActionResult PostApplicantSkill([FromBody]ApplicantSkillPoco[] model)
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
        [HttpGet("skill")]
        public ActionResult GetAllApplicantSkill()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("skill")]
        public ActionResult PutApplicantSkill([FromBody]ApplicantSkillPoco[] model)
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

        [HttpDelete("skill")]
        public ActionResult DeleteApplicantSkill([FromBody]ApplicantSkillPoco[] model)
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