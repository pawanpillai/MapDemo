using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWebApp.DAL;
using MyWebApp.DAL.DataHandlers;

namespace MyWebApp.API
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration configuration;


        public ValuesController(IConfiguration config)
        {
            configuration = config;
        }

        // GET api/values
        [HttpGet]
        public JsonResult GetAllData()
        {
            ValuesDataHandler objValuesDataHander = new ValuesDataHandler(configuration);
            return Json(objValuesDataHander.GetValues());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string GetDataById(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void PostData([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [Route("admin/[controller]")]
    public class MyClsController : Controller
    {

    }
}
