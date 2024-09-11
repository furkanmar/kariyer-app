using dotnet_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CVController : ControllerBase
    {



        private Admin admin;
        public CVController(Admin admin)
        {
            this.admin = admin;
        }



        [HttpGet]
        public List<CV> GetCV()
        {
            return admin.GetCV();

        }

        [HttpGet("{id}")]
        public CV GetCVById(int id)
            {
            if (id >= 0)
            {
                return admin.GetCVByID(id);
            }
            else
            {
                throw new Exception("id cannot lower than 0");
            }

        }
        
        [HttpGet("getRelatedCv")]
        public IActionResult GetCVs([FromQuery] int id)
        {
            var cvs = admin.GetCvsByID(id);
            
            return Ok(cvs);
        }

        [HttpPost]
        public IActionResult CVCreate([FromBody] CV cv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                admin.CVAdd(cv);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut]
        public void CVUpdate(CV cv)
        {
            admin.CVUpdate(cv);

        }
        [HttpDelete("{id}")]
        public void DeleteCV(int id)
        {
            var cv = admin.GetCVByID(id);


            admin.CVRemove(cv);




            }
    }
}
