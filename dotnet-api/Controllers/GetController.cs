using dotnet_api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotnet_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GetController:ControllerBase

    {
        private Admin admin;
        public GetController(Admin admin)
        {
            this.admin = admin;
        }
        [HttpGet("{id}")]
        public Advert GetAdvert(int id)
        {
            return admin.GetAdvert(id);
        }

        [HttpGet("GetUsers")]
        public List<User> GetAllUsers()
        {
            return admin.GetUsers();

        }

        [HttpGet("GetEmail")]
        public IActionResult GetUser([FromQuery]int id)
        {
            var advert=admin.adverts.Find(a=> a.Id==id);
            var user = admin.Users.Find(a => a.Id == advert.ActiveUser);
            

            return Ok(user);
        }
        [HttpGet("GetAdverts")]
        public List<Advert> GetAllAdvert()
        {
            return admin.getAllAdvert();

        }
        [HttpGet("GetCvs")]
        public List<CV> GetAllCv()
        {
            return admin.GetCV();

        }


    }
    
}
