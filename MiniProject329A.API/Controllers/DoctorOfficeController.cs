using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorOfficeController : Controller
    {
        private DADoctorOffice doctorOffice;
        public DoctorOfficeController(MiniProject329AContext _db)
        {
            doctorOffice = new DADoctorOffice(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return doctorOffice.GetAll();
        }
    }
}
