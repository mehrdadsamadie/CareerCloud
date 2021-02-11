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
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;
        public CompanyJobEducationController()
        {
            EFGenericRepository<CompanyJobEducationPoco> repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }
        [HttpGet("JobEducation/{companyDescriptionId}")]
        public ActionResult GetCompanyJobEducation(Guid companyJobEducationId)
        {
            var _poco = _logic.Get(companyJobEducationId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("jobeducation")]
        public ActionResult PostCompanyJobEducation([FromBody]CompanyJobEducationPoco[] model)
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
        [HttpGet("jobeducation")]
        public ActionResult GetAllCompanyJobEducation()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("jobeducation")]
        public ActionResult PutCompanyJobEducation([FromBody]CompanyJobEducationPoco[] model)
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

        [HttpDelete("jobeducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody]CompanyJobEducationPoco[] model)
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