using dotnet_api.Data;
using Microsoft.AspNetCore.Mvc;




namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertController:ControllerBase
        
    {


        private Admin admin;
        public AdvertController(Admin admin ) {
            this.admin = admin;
        }


        [HttpGet]
        public List<Advert> GetAdverts()
        {

            return admin.adverts;

        }

        [HttpGet("edit")]
        public IActionResult GetAdvert([FromQuery] int id)
        {
            var advert = admin.GetAdvert(id);
            if (advert == null)
            {
                return NotFound();
            }
            return Ok(advert);
        }


        [HttpGet("{id}")]
        public List<Advert> GetListById(int id)
        {
            if (id==0)
            {
                return admin.getAllAdvert();
            }
            
            else
            {
                return admin.GetByID(id);
            }

        }
        // Python 
        [HttpGet("update")]
        public IActionResult UpdateData()
        {
            admin.ReloadData();     

            return Ok(new { message = "Advert ve CV listeleri güncellendi." });
        }

        [HttpPost]
        public void Create(Advert advert)
        {
            admin.Add(advert);
        }

        [HttpPut]   
        public void Update(Advert advert)
        {
            admin.Update(advert);
                
        }
        [HttpDelete("{id}")]
        public void DeleteAdvert(int id)
        {
            
            
            admin.AdvertRemove(id);


            

            

            
            

           
        }






    }
}
