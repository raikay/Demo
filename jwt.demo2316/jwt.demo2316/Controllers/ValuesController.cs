using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jwt.demo2316.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("GetToken")]
        public ActionResult<dynamic> GetToken()
        {
            Dictionary<string, object> payLoad = new Dictionary<string, object>();
            payLoad.Add("sub", "rober");
            payLoad.Add("jti", Guid.NewGuid().ToString());
            payLoad.Add("nbf", null);
            payLoad.Add("exp", null);
            payLoad.Add("iss", "roberIssuer");
            payLoad.Add("aud", "roberAudience");
            payLoad.Add("age", 30);

            var encodeJwt = JwtHelper.CreateToken(payLoad, 30);

            return new { token = encodeJwt, code = 200, message = "获取成功" };
        }

        [HttpGet("Checktoken")]
        public ActionResult<dynamic> Checktoken()
        {
            var encodeJwt = HttpContext.Request.Headers["Authorization"];
            var result = JwtHelper.Validate(encodeJwt, (load) =>
            {
                var success = true;
                //验证是否包含aud 并等于 roberAudience
                success = success && load["aud"]?.ToString() == "roberAudience";

                //验证age>20等
                int.TryParse(load["age"].ToString(), out int age);

                //其他验证 jwt的标识 jti是否加入黑名单等

                return success;
            });
            if (result)
            {

                return result;
            }
            else
            {
                
                HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return Content("Unauthorized");
                
            }
        }
    }
}
