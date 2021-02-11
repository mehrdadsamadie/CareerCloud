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
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryController()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }
        [HttpGet("workhistory/{applicantWorkHistoryId}")]
        public ActionResult GetApplicantWorkHistory(Guid applicantWorkHistoryId)
        {
            var _poco = _logic.Get(applicantWorkHistoryId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("workhistory")]
        public ActionResult PostApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] model)
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
        [HttpGet("workhistory")]
        public ActionResult GetAllApplicantWorkHistory()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("workhistory")]
        public ActionResult PutApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] model)
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

        [HttpDelete("workhistory")]
        public ActionResult DeleteApplicantWorkHistory([FromBody]ApplicantWorkHistoryPoco[] model)
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