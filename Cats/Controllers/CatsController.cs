using Cats.Data;
using Cats.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cats.Controllers
{
    public class CatsController : ApiController
    {

        public IHttpActionResult Get()
        {

            (bool success, List<Cat> cats) = DataAccess.GetCats();

            if (success) return Ok(new { records = cats, total = cats.Count });

            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to retrieve cats. Please check logs for details."));

        }

        public IHttpActionResult Post(Cat record)
        {

            if (DataAccess.AddCat(record)) return Ok();

            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Unable to add cat with name {record.Name}. Please check logs for details."));

        }

        public IHttpActionResult Put(Cat record)
        {
          
            if(DataAccess.UpdateCat(record)) return Ok();
            
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Unable to update cat with name {record.Name}. Please check logs for details."));

        }

        public IHttpActionResult Delete(int id)
        {
            
           if(DataAccess.DeleteCat(id)) return Ok();
           
           return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Unable to delete cat with Id: {id}. Please check logs for details."));

        }
    }
}
