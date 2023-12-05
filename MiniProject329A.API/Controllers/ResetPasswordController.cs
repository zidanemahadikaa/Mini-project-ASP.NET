using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResetPasswordController : Controller
    {
        private DAResetPassword resetPassword;

        public ResetPasswordController(MiniProject329AContext _db)
        {
            resetPassword = new DAResetPassword(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return resetPassword.GetAll();
        }
        [HttpPost("[action]")]
        public VMResponse ResetOldPassword(VMTblTResetPassword data)
        {
            return resetPassword.ResetOldPassword(data);
        }

    }
}
