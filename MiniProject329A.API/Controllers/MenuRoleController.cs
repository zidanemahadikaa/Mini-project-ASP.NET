using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuRoleController : Controller
    {
        private DAMenuRole menuRole;

        public MenuRoleController(MiniProject329AContext _db)
        {
            menuRole = new DAMenuRole(_db);
        }

        [HttpGet("{Id?}")]
        public VMResponse GetByRoleId(int Id)
        {
            return menuRole.GetByRoleId(Id);
        }
    }
}
