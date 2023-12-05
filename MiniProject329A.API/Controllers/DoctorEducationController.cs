using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorEducationController : Controller
    {
        private DADoctorEducation DoctorEducation;

        public DoctorEducationController(MiniProject329AContext _db) 
        {
            DoctorEducation = new DADoctorEducation(_db);
        }
        //Get All Data
        [HttpGet()]
        public VMResponse GetAll()
        {
            return DoctorEducation.GetAll();
        }
    }
}
