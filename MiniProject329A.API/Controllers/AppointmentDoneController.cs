using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentDoneController : Controller
    {
        private DAAppointmentDone appDone;
        public AppointmentDoneController(MiniProject329AContext _db)
        {
            appDone = new DAAppointmentDone(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return appDone.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse GetById(long? id)
        {
            return appDone.GetById(id);
        }
    }
}
