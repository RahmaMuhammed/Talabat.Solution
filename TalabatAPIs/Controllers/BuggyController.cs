using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;
using TalabatAPIs.Controllers;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dpcontext)
        {
            _dbContext = dpcontext;
        }

        [HttpGet("notfound")]  //GET : api/buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.products.Find(100);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("servererror")]  //GET : api/buggy/servererror
        public ActionResult GetServerError()
        {
            var product = _dbContext.products.Find(100);
            var productToReturn = product.ToString(); // Will Throw Exception [NullReferenceException]
            return Ok(productToReturn);
        }

        [HttpGet("badrequest")]  // GET : api/buggy/badrequest
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")] // GET : api/buggy/badrequest/five
        public ActionResult GetBadRequest(int id) // Validation Error
        {
            return Ok();
        }
    }
}
