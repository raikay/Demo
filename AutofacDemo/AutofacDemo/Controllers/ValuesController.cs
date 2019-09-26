using System.Collections.Generic;
using AutofacDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutofacDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IServices _advertisementServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="advertisementServices"></param>
        public ValuesController(IServices advertisementServices)
        {
            _advertisementServices = advertisementServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _advertisementServices.GetDataList().ToArray();
        }
    }
}
