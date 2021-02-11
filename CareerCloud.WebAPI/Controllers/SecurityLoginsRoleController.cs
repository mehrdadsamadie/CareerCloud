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
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;
        public SecurityLoginsRoleController()
        {
            EFGenericRepository<SecurityLoginsRolePoco> repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }
        [HttpGet("loginsRole/{securityLoginsRoleId}")]
        public ActionResult GetSecurityLoginsRole(Guid securityLoginsRoleId)
        {
            var _poco = _logic.Get(securityLoginsRoleId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("loginsRole")]
        public ActionResult PostSecurityLoginRole([FromBody]SecurityLoginsRolePoco[] model)
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
        [HttpGet("loginsRole")]
        public ActionResult GetAllSecurityLoginRole()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("loginsRole")]
        public ActionResult PutSecurityLoginsRole([FromBody]SecurityLoginsRolePoco[] model)
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

        [HttpDelete("loginsRole")]
        public ActionResult DeleteSecurityLoginRole([FromBody]SecurityLoginsRolePoco[] model)
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