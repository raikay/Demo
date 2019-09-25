using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        
        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 参数 说明文档 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetDemo))]
        public Param GetDemo(Param param)
        {
            return new Param();
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class Param
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { set; get; }

            /// <summary>
            ///  编号
            /// </summary>
            public int Code { set; get; }

            /// <summary>
            ///  网址
            /// </summary>
            public string Urn { set; get; }
        }
    }
}
