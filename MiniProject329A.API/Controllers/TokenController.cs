using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private DAToken token;

        public TokenController(MiniProject329AContext _db)
        {
            token = new DAToken(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return token.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse GetByEmail(string email, string otp)
        {
            return token.GetByEmailToken(email, otp);
        }
        [HttpGet("[action]")]
        public VMResponse SameEmail(string email)
        {
            return token.EmailUser(email);
        }
        [HttpPost("[action]")]
        public VMResponse AddToken(VMTblTToken data)
        {
            return token.AddToken(data);
        }
        [HttpPut("[action]")]
        public VMResponse UpdateByOtpCode(VMTblTToken data)
        {
            return token.UpdateByOtpCode(data);
        }
        [HttpPut("[action]")]
        public VMResponse UpdateOtpEx(VMTblTToken data)
        {
            return token.UpdateOtpEx(data);
        }
        [HttpPut("[action]")]
        public VMResponse UpdateUserID(VMTblTToken data)
        {
            return token.UpdateUserID(data);
        }
    }
}
