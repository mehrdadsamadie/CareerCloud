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
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;
        public CompanyJobSkillController()
        {
            EFGenericRepository<CompanyJobSkillPoco> repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }
        [HttpGet("jobskill/{companyJobSkillId}")]
        public ActionResult GetCompanyJobSkill(Guid companyJobSkillId)
        {
            var _poco = _logic.Get(companyJobSkillId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("jobskill")]
        public ActionResult PostCompanyJobSkill([FromBody]CompanyJobSkillPoco[] model)
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
        [HttpGet("jobskill")]
        public ActionResult GetAllCompanyJobSkill()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("jobskill")]
        public ActionResult PutCompanyJobSkill([FromBody]CompanyJobSkillPoco[] model)
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

        [HttpDelete("jobskill")]
        public ActionResult DeleteCompanyJobSkill([FromBody]CompanyJobSkillPoco[] model)
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