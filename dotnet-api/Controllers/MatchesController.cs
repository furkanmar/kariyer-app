using dotnet_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchesController : ControllerBase
    {
        private Admin admin;
        public MatchesController(Admin admin)
        {
            this.admin = admin;
        }
        [HttpGet("{id}")]
        public List<Matches> GetMatchesCv(int id)
        {
            return admin.getMatchesCvById(id);
        }
        [HttpGet("cv")]
        public IActionResult getMatchesAdvet([FromQuery] int id)
        {
            var cv=admin.Cvs.Find(a=> a.Id==id);
            List<Matches> matches=admin.getMatchesAdvertById(cv.Id);
            
            return Ok(matches);
        }
        [HttpGet]
        public List<Matches> getMatches()
        {
            return admin.Matches;
        }

    }
}
