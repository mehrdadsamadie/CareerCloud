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
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;
        public SecurityLoginsLogController()
        {
            EFGenericRepository<SecurityLoginsLogPoco> repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }
        [HttpGet("loginslog/{securityLoginId}")]
        public ActionResult GetSecurityLoginLog(Guid securityLoginId)
        {
            var _poco = _logic.Get(securityLoginId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("loginslog")]
        public ActionResult PostSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] model)
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
        [HttpGet("loginslog")]
        public ActionResult GetAllSecurityLoginLog()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("loginslog")]
        public ActionResult PutSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] model)
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

        [HttpDelete("loginslog")]
        public ActionResult DeleteSecurityLoginLog([FromBody]SecurityLoginsLogPoco[] model)
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