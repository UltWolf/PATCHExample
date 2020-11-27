using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PATCH.Models;
using System.Collections.Generic;

namespace PATCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patchDoc"></param>
        /// <remarks> 
        /// [
        ///  {
        ///    "OperationType": 0,
        ///    "path": "customerName",
        ///    "op": "replace",
        ///    "value":"Hi"
        ///  }
        ///  for collection 
        ///  [ { "path": "/orders", "op": "add", "value":[{"OrderName":"Hi" } ]} ] 
        ///  Больше информации, как и что отправлять
        ///  https://sookocheff.com/post/api/understanding-json-patch/
        ///]
        ///</remarks>
        /// <returns></returns>
        [HttpPatch]
    public IActionResult JsonPatchWithModelState(
      [FromBody] JsonPatchDocument<Customer> patchDoc)
    {
        if (patchDoc != null)
        {
            var customer = new Customer() { CustomerName = "blah  blah", Orders = new List<Order>() { new Order() { OrderName = "filed" } } };

            patchDoc.ApplyTo(customer, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new ObjectResult(customer);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }
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
}
}
