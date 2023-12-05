using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private DARole role;
        public RoleController(MiniProject329AContext _db)
        {
            role = new DARole(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return role.GetAll();
        }
        [HttpPost("[action]")]
        public VMResponse CreateRole(VMTblMRole formData)
        {
            return role.CreateRole(formData);
        }
    }
}
