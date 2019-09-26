using System.Collections.Generic;
using AutofacDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutofacDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly Services _services;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="services"></param>
        public ValuesController(Services services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _services.GetDataList().ToArray();
        }
    }
}
