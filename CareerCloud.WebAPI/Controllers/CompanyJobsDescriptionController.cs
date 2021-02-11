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
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;
        public CompanyJobsDescriptionController()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }
        [HttpGet("jobsdescription/{companyJobsDescriptionId}")]
        public ActionResult GetCompanyJobsDescription(Guid companyJobsDescriptionId)
        {
            var _poco = _logic.Get(companyJobsDescriptionId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("jobsdescription")]
        public ActionResult PostCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] model)
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
        [HttpGet("jobsdescription")]
        public ActionResult GetAllCompanyJobsDescription()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("jobsdescription")]
        public ActionResult PutCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] model)
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

        [HttpDelete("jobsdescription")]
        public ActionResult DeleteCompanyJobsDescription([FromBody]CompanyJobDescriptionPoco[] model)
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