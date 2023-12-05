using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private DAAppointment appointment;
        public AppointmentController(MiniProject329AContext _db)
        {
            appointment = new DAAppointment(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return appointment.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse GetAllAppointment(long id)
        {
            return appointment.GetCustomerAppointment(id);
        }
        [HttpGet("[action]")]
        public VMResponse GetCusDocName(long parId,string? name)
        {
            return appointment.GetByCusDocrName(parId, name);
        }
    }
}
