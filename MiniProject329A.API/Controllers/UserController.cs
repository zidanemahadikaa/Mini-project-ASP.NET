using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private DAUser user;

        public UserController(MiniProject329AContext _db)
        {
            user = new DAUser(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return user.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse Login(string email, string password)
        {
            return user.Login(email, password);
        }

        [HttpGet("{id?}")]
        public VMResponse GetById(int id)
        {
            return user.GetById(id);
        }
        [HttpGet("[action]")]
        public VMResponse EmailCheck(string email)
        {
            return user.EmailCheck(email);
        }
        [HttpPost]
        public VMResponse Register(VMTblMUser data)
        {
            return user.Register(data);
        }
        [HttpPut("[action]")]
        public VMResponse LoginAttemp(VMTblMUser data)
        {
            return user.LoginAttemp(data);
        }
        [HttpPut("[action]")]
        public VMResponse ResetLoginAttemp(VMTblMUser data)
        {
            return user.ResetLoginAttemp(data);
        }
        [HttpPut("[action]")]
        public VMResponse UpdatePassword(VMTblMUser data)
        {
            return user.UpdatePassword(data);
        }
    }
        
}
