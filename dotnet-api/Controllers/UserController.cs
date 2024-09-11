using dotnet_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private Admin admin;
        public UserController(Admin admin)
        {
            this.admin = admin;
        }

        [HttpGet]
        public List<User> GetAllUser()
        {
            return admin.GetUsers();
        }


        
        [HttpGet("{id}")]
        public User GetUser(User user)
        {
            if (user.Id >= 0)
            {
                return admin.UserGetById(user.Id    );
            }
            else
            {
                throw new Exception("id cannot lower than 0");
            }
        }




        [HttpPost]
        public void AddUser(User user)
        { 
            admin.AddUser(user);
            
            
            
        }
        [HttpPost("authenticate")]
        public ActionResult<User> AuthenticateUser([FromBody] UserCredentials credentials)
        {
            var user = admin.GetUsers().FirstOrDefault(u => u.Email == credentials.Email && u.Password == credentials.Password);

            if (user != null)
            {
                return user; // Kullanıcı türünü döndürür
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        [HttpPut]
        public void UpdateUser(User user)
        {
            admin.UserUpdate(user);
        }
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {

            var user = admin.UserGetById(id);
            


            admin.UserRemove(user);
        }
        

    }
}
