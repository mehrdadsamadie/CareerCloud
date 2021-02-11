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
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;
        public SystemLanguageCodeController()
        {
            EFGenericRepository<SystemLanguageCodePoco> repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }
        [HttpGet("languagecode/{systemLanguageCodeId}")]
        public ActionResult GetSystemLanguageCode(string systemLanguageCodeId)
        {
            var _poco = _logic.Get(systemLanguageCodeId);
            if (_poco == null)
            {
                return NotFound();
            }
            else { return Ok(_poco); }
        }
        [HttpPost("languagecode")]
        public ActionResult PostSystemLanguageCode([FromBody]SystemLanguageCodePoco[] model)
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
        [HttpGet("languagecode")]
        public ActionResult GetAllSystemLanguageCode()
        {

            return Ok(_logic.GetAll());

        }
        [HttpPut("languagecode")]
        public ActionResult PutSystemLanguageCode([FromBody]SystemLanguageCodePoco[] model)
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

        [HttpDelete("languagecode")]
        public ActionResult DeleteSystemLanguageCode([FromBody]SystemLanguageCodePoco[] model)
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